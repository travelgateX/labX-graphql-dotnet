using GraphQL.StarWars.Enum;
using GraphQL.Types;
using GraphQL.StarWars.Types;
using GraphQL.StarWars.Data;

namespace GraphQL.StarWars.Resolve
{
    public class SpecieType : ObjectGraphType<Specie>
    {
        public SpecieType(StarWarsData data)
        {
            Name = "Specie";

            Field(h => h.designation, nullable: true).Description("designation");

            Field(h => h.language, nullable: true).Description("language");

            Field<ListGraphType<NonNullGraphType<SpecieType>>>(
                "subEspecies",
                resolve: context => context.Source.subEspecies
            );

            Field<HomeworldType>("homeworld", "The homeworld.");

        }
    }
}
