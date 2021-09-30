using System;
using AVStack.MessageBus.Abstraction;
using AVStack.MessageBus.Extensions;
using AVStack.MessageCenter.Common.Configuration;
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
            services.AddControllers();
            //services.AddAuthorization();
            services.ConfigureOptions(configuration);
            services.ConfigureRabbitMq(configuration);

            services.AddTransient<IEmailService, EmailService>();

            services.ConfigureHosts();
        }

        private static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailOptions>(configuration.GetSection(EmailOptions.EmailOptionsSection));
        }

        private static void ConfigureHosts(this IServiceCollection services)
        {
            services.AddHostedService<EmailHost>();
        }

        private static void ConfigureRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMessageBus(options =>
            {
                options.Uri = new Uri(configuration.GetSection("RabbitMQ")["Uri"]);
            }, busFactory => busFactory.ConfigureTopology());
        }

        private static void ConfigureTopology(this IMessageBusFactory busFactory)
        {
            // Configure email, sms and viber topic
            busFactory.DeclareExchange("email.sms.viber", ExchangeType.Topic);
            busFactory.DeclareQueue("email");
            busFactory.DeclareQueue("sms");
            busFactory.DeclareQueue("newsletter");
            busFactory.BindQueue("email", "email.sms.viber", "email.*.*");
            busFactory.BindQueue("sms", "email.sms.viber", "*.sms.*");
            busFactory.BindQueue("viber", "email.sms.viber", "*.*.viber");
        }
    }
}