using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Nintex.K2.CustomServiceBrokers.RPA
{
    public class KryonGraphQLClient
    {
        private string _host;
        private string _port;
        private string _scheme;
        private string _realm;
        private string _username;
        private string _password;
        private GraphQLHttpClient _graphClient;
        public string Host
        {
            get { return _host; }
        }
        public string Port
        {
            get { return _port; }
        }
        public string Scheme
        {
            get { return _scheme; }
        }
        public string Realm
        {
            get { return _realm; }
        }

        public KryonGraphQLClient(string host, string scheme, string port, string realm, string username, string password)
        {
            _host = host;
            _scheme = scheme;
            _port = port;
            _realm = realm;
            _username = username;
            _password = password;
            _graphClient = new GraphQLHttpClient(_scheme + "://" + _host + "/public-api/graphql", new NewtonsoftJsonSerializer());
        }

        private async Task<TokenResponse> GetToken()
        {
            string url = _scheme + "://" + _host + "/auth/realms/" + _realm + "/protocol/openid-connect/token";
            HttpClient client = new HttpClient();
            var values = new Dictionary<string, string>
            {
                { "client_id", "kryon-console"},
                {"grant_type", "password"},
                { "username", _username },
                { "password", _password}
            };
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync(url, content);
            var token = await response.Content.ReadAsStringAsync();
            return (TokenResponse)JsonConvert.DeserializeObject(token, typeof(TokenResponse));
        }

        public async Task<Queue[]> GetQueues(string tenantId)
        {
            var token = await this.GetToken();
            var getqueues = new GraphQLHttpRequestWithAuthSupport
            {
                Query = @"
                   query{
		            getQueues: getQueues(queues: {tenantId:""" + tenantId + @"""}) {
		                id
				        name
				        tenantId
				        createdAt
				        isDeleted
				        deletedAt
				        createdBy
		            }
		        }",
                //OperationName = "getQueues",                
                Authentication = new AuthenticationHeaderValue(@"Bearer", token.Access_Token),
                AdditionalHeaders = new Dictionary<string, string>{
                    {"kryon-client-id", "kryon-public-api"},
                    {"kryon-auth-provider", "aerobase" }
                }
            };
            var queues = (await _graphClient.SendQueryAsync<JObject>(getqueues)).Data;
            return (Queue[])JsonConvert.DeserializeObject(queues["getQueues"].ToString(), typeof(Queue[]));
        }

        public async Task<TaskResponse> AddTask(NewTaskRequest task)
        {
            var token = await this.GetToken();
            var addTask = new GraphQLHttpRequestWithAuthSupport
            {
                Query = @"
                   mutation($task: AddTaskInput!){
		            addTask: addTask(task: $task) {
		                id
		                name
		                priority
		                queueId
		                tenantId
		                wizardId
		                state
		                producerId
		                producerType
		                ttl
		                operationalHours
		                retryLimit
		                retryTimes
		                avgTaskRun
		                createdAt
		                telemetry {
		                    waitTimeInQueue
		                }		     
		            }
		        }",
                Variables = new
                {
                    task = task
                },
                Authentication = new AuthenticationHeaderValue(@"Bearer", token.Access_Token),
                AdditionalHeaders = new Dictionary<string, string>{
                    {"kryon-client-id", "kryon-public-api"},
                    {"kryon-auth-provider", "aerobase" }
                }
            };

            var data = await _graphClient.SendQueryAsync<JObject>(addTask);
            var response = (TaskResponse)JsonConvert.DeserializeObject(data.Data["addTask"].ToString(), typeof(TaskResponse));
            return response;

        }


    }
}
