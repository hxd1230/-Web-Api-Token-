using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace api
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            //string clientId, clientSecret;
            //context.TryGetBasicCredentials(out clientId, out clientSecret);
            //if (clientId == "Mobile" && clientSecret == "XiaoMi")
            //{
            context.Validated();
            //}
            //else
            //{
            //    context.Response.StatusCode = 200;
            //    context.SetError("invalid_client", "无效客户端");
            //}
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            if (context.UserName.Trim() == "admin" && context.Password.Trim() == "123456")
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                context.Validated(identity);
            }
            else
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
            }
        }
    }
}