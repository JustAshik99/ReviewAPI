using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Venue.Api.Authentication
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAuthAttribute : Attribute, IAsyncActionFilter
    {
        private const string ApiKeyHeaderName = "apikey";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            bool ios = false;
            var error = new ReviewAPI.Controllers.ReviewController.Error
            {
                message = "Invalid Authentication"
            };
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var potentialApiKey)) //issue reading api key
            {
                context.Result = new JsonResult(error);
                return;
            }

            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var IOSapikey = configuration.GetValue<string>("Apikey"); //get value of apikey from config

            if (checkIOS(IOSapikey, potentialApiKey))
            {
                ios = true; 
            }

            if (ios == true)
            {

            }
            else
            {
                context.Result = new JsonResult(error); //incorrect api key
                return;
            }

            await next(); //continue
        }

        public bool checkIOS(string potential, string key) //checks if key is correct
        {
            if (key.Equals(potential))
            {
                return true;
            }
            return false;
        }

    }
}
