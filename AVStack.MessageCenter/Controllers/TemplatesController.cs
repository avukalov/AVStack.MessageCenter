using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Client.Http;
using Microsoft.AspNetCore.Mvc;

namespace AVStack.MessageCenter.Controllers
{
    [ApiController]
    [Route("api/templates")]
    public class TemplatesController : ControllerBase
    {
        private readonly GraphQLHttpClient _graphQlHttpClient;

        public TemplatesController(GraphQLHttpClient graphQlHttpClient)
        {
            _graphQlHttpClient = graphQlHttpClient;
        }

        // GET
        [HttpGet]
        public async Task<IActionResult> FindAsync()
        {
            var query = @"query AVUserQuery { 
                AVUser {
                    Email
                    FirstName
                    LastName
                    Id
                    UserName
                    TwoFactorEnabled
                    EmailConfirmed
                    PhoneNumber
                    PhoneNumberConfirmed
                  }
            }";

            var request = new GraphQLRequest(query);

            _graphQlHttpClient.HttpClient.DefaultRequestHeaders.Add("x-hasura-admin-secret", "PaSSW0rd123!");
            var response = await _graphQlHttpClient.SendQueryAsync<UserQueryResponse>(request);

            return Ok(response.Data.AVUser);
        }
    }

    public class UserQueryResponse
    {
        public IEnumerable<UserDto> AVUser { get; set; }
    }

    public class UserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
    }
}