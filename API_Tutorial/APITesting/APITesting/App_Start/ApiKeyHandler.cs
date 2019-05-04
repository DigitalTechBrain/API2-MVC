using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Net;

namespace APITesting.App_Start
{
    public class ApiKeyHandler : DelegatingHandler
    {
        private const string APIKeyToCheck = "fixdialkeyJHHJKHGFGFG";

        protected override async Task<HttpResponseMessage> SendAsync (HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken)
        {
            bool validkey = false;
            IEnumerable<string> requestHeaders;
            var checkApiKeyExists = httpRequestMessage.Headers.TryGetValues("APIKey", out requestHeaders);
            if (checkApiKeyExists)
            {
                if (requestHeaders.FirstOrDefault().Equals(APIKeyToCheck))
                {
                    validkey = true;
                }
            }
                if(!validkey)
                {
                    return httpRequestMessage.CreateResponse(HttpStatusCode.Forbidden, "Invalid Api Key");

                }

                var response = await base.SendAsync(httpRequestMessage, cancellationToken);
                return response;
            }
        }
    }
