using GraphQL.Types;

namespace GraphQL.StarWars.Types
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