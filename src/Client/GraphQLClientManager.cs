using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace starwars.Client
{
    public class GraphQLClientManager
    {
        private static readonly object SYNCLOCK_LOCK = new object();

        private static GraphQLClientManager instance;

        private GraphQLClient _client;


        private GraphQLClientManager()
        {
            this._client = new GraphQLClient();

        }

        public static GraphQLClientManager getInstance()
        {
            if (GraphQLClientManager.instance == null)
            {
                lock (SYNCLOCK_LOCK)
                {
                    if (GraphQLClientManager.instance == null)
                    {
                        GraphQLClientManager.instance = new GraphQLClientManager();
                    }
                }
            }
            return GraphQLClientManager.instance;
        }

        public async Task<T> sendRequest<T>(string request)
        {
            return await _client.sendRequest<T>(request);

        }
    }   
}
