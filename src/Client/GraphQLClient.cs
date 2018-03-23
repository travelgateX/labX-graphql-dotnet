using System;
using System.Threading.Tasks;
using GraphQL.Client;
using GraphQL.Common.Request;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace starwars.Client
{
    public class GraphQLClient
    {
        GraphQL.Client.GraphQLClient graphQLClient;

        public GraphQLClient()
        {
            graphQLClient = new GraphQL.Client.GraphQLClient("http://192.168.0.120:9002/graphql");
        }

        public async Task<T> sendRequest<T>(string request, string label){
            

            var rq = new GraphQLRequest
                {
                    Query = request,
                    //OperationName = "hotelX",
                    //Variables = null
                };

            var graphQLResponse = await graphQLClient.PostAsync(rq);

            var data = graphQLResponse.GetDataFieldAs<T>(label);

            return data;
        }
    }
}