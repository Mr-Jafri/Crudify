using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Crudify.Api.ActionFilters
{
    public class TokenAuthentication(ITokenService tokenService) : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any())
            {
                return;
            }

            var unauthorizedResponse = new JsonResult(new ApiResponse { HttpStatusCode = StatusCodes.Status401Unauthorized, Message = ["Unauthorized"] }) { StatusCode = StatusCodes.Status401Unauthorized };
            var hasAuthorizationToken = context.HttpContext.Request.Headers.ContainsKey("Authorization");

            if (!hasAuthorizationToken)
            {
                context.Result = unauthorizedResponse;
                return;
            }

            string token = string.Empty;
            token = context.HttpContext.Request.Headers.First(x => x.Key == "Authorization").Value;

            token = token.Split(" ").Length > 1 ? token.Split(" ")[1] : string.Empty;

            if (string.IsNullOrEmpty(token))
            {
                context.Result = unauthorizedResponse;
            }

            try
            {
                var claimPrinciple = tokenService?.VerifyToken(token);

                if (claimPrinciple == null)
                {
                    context.Result = unauthorizedResponse;
                }
            }
            catch (Exception ex)
            {
                context.Result = unauthorizedResponse;
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                throw;
            }
        }
    }
}
