using GraphQL.Types;

namespace GraphQL.StarWars.Types
{
    public class StarShipType : ObjectGraphType<Starship>
    {
        public StarShipType(StarWarsData data)
        {
            Name = "Starship";

            Field<NonNullGraphType<IdGraphType>>("id", "The ID of the starship");

            Field(h => h.Name, nullable: false).Description("The name of the starship");

            Field<NonNullGraphType<FloatGraphType>>(
                "length",
                arguments: new QueryArguments(
                    new QueryArgument<HeighEnum> { Name = "unit", Description = "Length of the starship, along the longest axis", DefaultValue = LengthUnit.METER }
                ),
                resolve: context => data.GetHeighOrLenght(context.GetArgument<LengthUnit>("unit", LengthUnit.METER), context.Source.Length)
            );
        }
    }
}
