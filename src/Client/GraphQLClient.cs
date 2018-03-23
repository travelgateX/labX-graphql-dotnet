using System;
using System.Threading.Tasks;
using GraphQL.Client;
using GraphQL.Common.Request;
using Microsoft.AspNetCore.Http;

namespace starwars.Client
{
    public class GraphQLClient
    {
        GraphQL.Client.GraphQLClient graphQLClient;

        public GraphQLClient()
        {
            graphQLClient = new GraphQL.Client.GraphQLClient("http://localhost:9002/graphql");
        }

        public async Task<T> sendRequest<T>(string request){
            

            var rq = new GraphQLRequest
                {
                    Query = request,
                    //OperationName = "hotelX",
                    //Variables = null
                };

            var graphQLResponse = await graphQLClient.PostAsync(rq);

            var data = graphQLResponse.GetDataFieldAs<T>(typeof(T).Name);

            return data;
        }
    }
}