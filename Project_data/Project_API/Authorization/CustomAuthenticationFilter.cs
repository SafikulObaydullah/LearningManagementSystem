using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace Project_API.Authorization
{
    public class CustomAuthenticationFilter : AuthorizeAttribute, IAuthenticationFilter
    {
        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            string authparameter = string.Empty;
            HttpRequestMessage requestMessage = context.Request;
            AuthenticationHeaderValue authorization = requestMessage.Headers.Authorization;
            if(authorization==null)
            {
                context.ErrorResult = new AuthenticationFailureResult("Missing authentication header",requestMessage);
                return;
            }
            if(authorization.Scheme != "Bearer")
            {
                context.ErrorResult = new AuthenticationFailureResult("Invalid authorization schema", requestMessage);
                return;
            }
            if(String.IsNullOrEmpty(authorization.Parameter))
            {
                context.ErrorResult = new AuthenticationFailureResult("Invalid token", requestMessage);
                return;
            }
            context.Principal = TokenManager.GetPrincipal(authorization.Parameter);

            
        }
        public async Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            var result = context.Result.ExecuteAsync(cancellationToken).Result;
            if(result.StatusCode==System.Net.HttpStatusCode.Unauthorized)
            {
                result.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue(scheme: "Basic", parameter: "relam:localhost"));
            }
            context.Result = new ResponseMessageResult(result);
        }
    }

    public class AuthenticationFailureResult : IHttpActionResult
    {
        public string reasonPharase;
        public HttpRequestMessage Request { get; set; }
        public AuthenticationFailureResult(string rPharase,HttpRequestMessage msg)
        {
            this.reasonPharase = rPharase;
            this.Request = msg;
        }
        //public string reasonPharase;
        //public HttpRequestMessage Request { get; set; }
        //public AuthenticationFailureResult(string rPharase, HttpRequestMessage msg)
        //{
        //    this.reasonPharase = rPharase;
        //    this.Request = msg;

        //}
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }
        private HttpResponseMessage Execute()
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            responseMessage.ReasonPhrase = reasonPharase;
            responseMessage.RequestMessage = Request;
            return responseMessage;
        }
    }
}