using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace starwars
{
    public class GraphQLClientManager
    {
        private static readonly object SYNCLOCK_LOCK = new object();

        private static GraphQLClientManager instance;

        private GraphQLClientManager()
        {
            //TODO : create http

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


    }
}
