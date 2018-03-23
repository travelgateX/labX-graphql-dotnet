using GraphQL.StarWars.Enum;
using GraphQL.Types;

namespace GraphQL.StarWars
{
    public class ReviewInputType : InputObjectGraphType
    {
        public ReviewInputType()
        {
            Name = "ReviewInputType";
            Field<NonNullGraphType<IntGraphType>>("stars");
            Field<StringGraphType>("commentary");
        }

    }
}