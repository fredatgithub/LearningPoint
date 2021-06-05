﻿using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using IsolatedFunctionApps.Dtos;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace IsolatedFunctionApps.FunctionApps
{
    public class InstanceFunctions
    {
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        public InstanceFunctions(JsonSerializerOptions jsonSerializerOptions)
        {
            _jsonSerializerOptions = jsonSerializerOptions;
        }

        [Function("SayHelloInstance")]
        public async Task<HttpResponseData> SayHello(
            // When using HttpTrigger, use HttpRequestData wrapper for reading input Http Request
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
            FunctionContext executionContext)
        {

            // Use the FunctionContext.GetLogger to fetch instance of ILogger
            var logger = executionContext.GetLogger(nameof(StaticFunctions));
            logger.LogInformation("Started execution of function");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonSerializer.Deserialize<UserDto>(requestBody, _jsonSerializerOptions);

            // Should use HttpResponseData as response
            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync<UserDto>(data);

            return response;
        }

        [Function("InvalidOperation")]
        public Task<HttpResponseData> InvalidOperation( // When using HttpTrigger, use HttpRequestData wrapper for reading input Http Request
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
            FunctionContext executionContext)
        {
            throw new System.Exception("Mock Exception");
        }
    }
}