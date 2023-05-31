using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Nintex.K2.CustomServiceBrokers.RPA
{ 

    public class GraphQLHttpRequestWithAuthSupport : GraphQLHttpRequest
	{
		public AuthenticationHeaderValue Authentication { get; set; }
		public Dictionary<string,string> AdditionalHeaders { get; set; }

		public override HttpRequestMessage ToHttpRequestMessage(GraphQLHttpClientOptions options, IGraphQLJsonSerializer serializer)
		{
			var r = base.ToHttpRequestMessage(options, serializer);
			r.Headers.Authorization = Authentication;
            foreach (var header in AdditionalHeaders)
            {
				r.Headers.Add(header.Key, header.Value);
			}
			return r;
		}
	}
}
