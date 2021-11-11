using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AVStack.MessageBus.Abstraction;
using AVStack.MessageBus.Extensions;
using AVStack.MessageCenter.Common.Configuration;
using AVStack.MessageCenter.Handlers;
using AVStack.MessageCenter.Hosts;
using AVStack.MessageCenter.Services;
using AVStack.MessageCenter.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace AVStack.MessageCenter.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddControllers();
            //services.AddAuthorization();

            services.AddRabbitMq(configuration);
            services.AddAutoMapper(typeof(Startup));

            services.RegisterOptions(configuration);
            services.RegisterServices();
            services.RegisterHosts();
        }

        private static void RegisterOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailConfigurationOptions>(configuration.GetSection(EmailConfigurationOptions.EmailConfigurationSection));
            services.Configure<EmailTemplatesOptions>(configuration.GetSection(EmailTemplatesOptions.EmailTemplatesSection));
        }
        private static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<ITopicService, TopicService>();
            services.RegisterAllTypes<ITopicHandler>(new []{ typeof(Startup).Assembly });
        }
        private static void RegisterHosts(this IServiceCollection services)
        {
            services.AddHostedService<MessageCenterBgService>();
        }

        private static void AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMessageBus(options =>
            {
                options.Uri = new Uri(configuration.GetSection("RabbitMQ")["Uri"]);
            }, busFactory => busFactory.ConfigureTopology());
        }
        private static void ConfigureTopology(this IMessageBusFactory busFactory)
        {
            // Infrastructure
            busFactory.DeclareExchange("monitoring", ExchangeType.Topic);

            busFactory.DeclareExchange("identity-server", ExchangeType.Topic);
            busFactory.DeclareExchange("account", ExchangeType.Topic);

            busFactory.DeclareQueue("message-center");


            // Bindings
            busFactory.BindExchange("identity-server", "monitoring", "identity-server.#");
            busFactory.BindExchange("account", "monitoring", "account.#");

            busFactory.BindQueue("message-center", "identity-server", "#");
            busFactory.BindQueue("message-center", "account", "#");
        }

        private static void RegisterAllTypes<T>(this IServiceCollection services, IEnumerable<Assembly> assemblies,
            ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes.Where(x => x.GetInterfaces().Contains(typeof(T))));
            foreach (var type in typesFromAssemblies)
            {
                if (type.IsAbstract) continue;
                services.Add(new ServiceDescriptor(typeof(T), type, lifetime));
            }
        }
    }
}