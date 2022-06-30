using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;


namespace UsernamePasswordTokenFunction
{
    public static class UsernamePasswordTokenFunction
    {

        private static HttpClient httpClient = new HttpClient();

        [FunctionName("UsernamePasswordTokenFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            var scopes = new string[] { "User.Read" };
            var scopesString = Environment.GetEnvironmentVariable("scopes");
            if (!string.IsNullOrEmpty(scopesString))
            {
                scopes = scopesString.Split(',');
            }
        
            var token = await Utility.GetAccessTokenAsync(scopes, req.Query["username"][0].UrlDecode(), req.Query["password"][0].UrlDecode());

            return string.IsNullOrEmpty(token)
                ? new EmptyResult()
                : new OkObjectResult(token);

        }
    }
}
