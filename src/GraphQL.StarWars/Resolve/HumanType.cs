using GraphQL.StarWars.Enum;
using GraphQL.Types;
using GraphQL.StarWars.Types;
using GraphQL.StarWars.Data;

namespace GraphQL.StarWars.Resolve
{
    public class HumanType : ObjectGraphType<Human>
    {
        public HumanType(StarWarsData data, ExternalData eData)
        {
            Name = "Human";

            Field<NonNullGraphType<IdGraphType>>("id", "The id of the human.");

            Field(h => h.Name, nullable: false).Description("The name of the human.");

            Field(h => h.Mass, nullable: false).Description("Mass in kilograms, or null if unknown");

            Field<NonNullGraphType<FloatGraphType>>(
                "height",
                arguments: new QueryArguments(
                    new QueryArgument<HeighEnum> { Name = "unit", Description = "Height in the preferred unit, default is meters", DefaultValue = LengthUnit.METER }
                ),
                resolve: context => data.GetHeighOrLenght(context.GetArgument<LengthUnit>("unit", LengthUnit.METER), context.Source.Height)
            );


            Field<ListGraphType<CharacterInterface>>(
                "friends",
                resolve: context => context.Source.Friends
            );

            Field<ListGraphType<StarshipType>>(
                "starships",
                resolve: context => context.Source.Starships
            );

            Field<NonNullGraphType<ListGraphType<NonNullGraphType<EpisodeEnum>>>>("appearsIn", "Which movie they appear in.");

            Field<HomeworldType>(
                "homeworld",
                resolve: context => eData.GetHomeWorld(null)
            );

            Interface<CharacterInterface>();
        }
    }
}
