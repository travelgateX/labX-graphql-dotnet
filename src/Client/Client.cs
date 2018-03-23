using System;
using System.Threading.Tasks;
using GraphQL.Client;
using GraphQL.Common.Request;
using Microsoft.AspNetCore.Http;

namespace Client
{
    public class Client
    {


        public async Task<T> sendRequest<T>(string url, string request){
            
            var graphQLClient = new GraphQLClient(url);


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