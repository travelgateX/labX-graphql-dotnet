using GraphQL.Types;

namespace GraphQL.StarWars.Types
{
    public class HumanType : ObjectGraphType<Human>
    {
        public HumanType(StarWarsData data)
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
                resolve: context => data.GetFriends(context.Source)
            );

            Field<ListGraphType<StarShipType>>(
                "starships",
                resolve: context => context.Source.Starships
            );

            Field<NonNullGraphType<ListGraphType<NonNullGraphType<EpisodeEnum>>>>("appearsIn", "Which movie they appear in.");

            Interface<CharacterInterface>();
        }
    }
}
