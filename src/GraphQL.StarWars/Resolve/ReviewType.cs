using GraphQL.Types;
using GraphQL.StarWars.Types;
using GraphQL.StarWars.Data;

namespace GraphQL.StarWars.Resolve
{
    public class ReviewType : ObjectGraphType<Review>
    {
        public ReviewType(StarWarsData data)
        {
            Name = "Review";
            Description = "Represents a review for a movie";
            Field(d => d.Stars, nullable: false).Description("The number of stars this review gave, 1-5.");
            Field(d => d.Commentary, nullable: true).Description("Commentary about the film");

        }
    }
}