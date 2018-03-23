using GraphQL.StarWars.Data;
using GraphQL.StarWars.Enum;
using GraphQL.StarWars.Resolve;
using GraphQL.StarWars.Types;
using GraphQL.Types;

namespace GraphQL.StarWars
{
    /// <example>
    /// This is an example JSON request for a mutation
    /// {
    ///   "query": "mutation ($human:HumanInput!){ createHuman(human: $human) { id name } }",
    ///   "variables": {
    ///     "human": {
    ///       "name": "Boba Fett"
    ///     }
    ///   }
    /// }
    /// </example>
    public class StarWarsMutation : ObjectGraphType<object>
    {


        public StarWarsMutation(StarWarsData data)
        {
            Name = "Mutation";

            Field<ReviewType>(
                "createReview",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<EpisodeEnum>> {Name = "episode" },
                    new QueryArgument<NonNullGraphType<ReviewInputType>> { Name = "review" }
                ),
                resolve: context =>
                {
                    return data.AddReview(context.GetArgument<Episodes>("episode"), context.GetArgument<Review>("review"));
                });
        }
    }
}