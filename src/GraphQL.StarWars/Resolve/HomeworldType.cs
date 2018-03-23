using GraphQL.StarWars.Enum;
using GraphQL.Types;
using GraphQL.StarWars.Types;
using GraphQL.StarWars.Data;

namespace GraphQL.StarWars.Resolve
{
    public class HomeworldType : ObjectGraphType<Homeworld>
    {
        public HomeworldType(StarWarsData data)
        {
            Name = "Homeworld";

            Field(h => h.name, nullable: true).Description("designation");

            Field<ListGraphType<NonNullGraphType<SpecieType>>>(
                "species",
                resolve: context => context.Source.species
            );
        }
    }
}
