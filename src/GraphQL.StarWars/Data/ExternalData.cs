using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.StarWars.Enum;
using GraphQL.StarWars.Types;
using starwars.Client;

namespace GraphQL.StarWars.Data
{
    public class ExternalData
    {
        public ExternalData()
        {

        }

        public Task<Specie> GetSpecie(string id)
        {

            string query = @"
                    query{";

                        if (!string.IsNullOrEmpty(id)){
                            query = query + "species(id:" + id +@") {";
                        }else{
                            query = query + "species {";
                        }
                        query = query +@"
                            designation
                            language
                            subEspecies {
                                designation
                                language
                                subEspecies {
                                    designation
                                    homeworld {
                                        name
                                        species {
                                            designation
                                        }
                                    }
                                } 
                            }
                        }                        
                    }";
            return GraphQLClientManager.getInstance().sendRequest<Specie>(query, "species");
        }

        public Task<Homeworld> GetHomeWorld(string id)
        {
            string query = @"
                    query{";

                    if (!string.IsNullOrEmpty(id)){
                            query = query + "homeworld(id:" + id +@") {";
                    }else{
                        query = query + "homeworld {";
                    }
                        query = query +@"
                            name
                            species {
                                designation
                                language
                                subEspecies {
                                    designation
                                    language
                                    subEspecies {
                                        designation
                                        homeworld {
                                            name
                                            species {
                                                designation
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }";
            return GraphQLClientManager.getInstance().sendRequest<Homeworld>(query, "homeworld");
        }

        public Task<List<Homeworld>> GetHomeWorlds()
        {
            string query = @"
                    query{
                        allHomeworlds {
                            name
                            species {
                                designation
                                language
                                subEspecies {
                                    designation
                                    language
                                    subEspecies {
                                        designation
                                        homeworld {
                                            name
                                            species {
                                                designation
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }";

            return GraphQLClientManager.getInstance().sendRequest<List<Homeworld>>(query, "allHomeworlds");
        }
    }
}
