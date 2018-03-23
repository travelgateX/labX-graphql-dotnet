using System;
using System.Collections.Generic;
using GraphQL.StarWars.Data;
using GraphQL.StarWars.Enum;
using GraphQL.StarWars.Resolve;
using GraphQL.StarWars.Types;
using GraphQL.Types;

namespace GraphQL.StarWars
{
    public class StarWarsQuery : ObjectGraphType<object>
    {
        public StarWarsQuery(StarWarsData data, ExternalData eData)
        {
            Name = "Query";

            Field< NonNullGraphType<ListGraphType<SearchResultType>>>(
                "search",
              arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "text", Description = "Text to find" }
              ),
                resolve: context => data.GetSearchResult(context.GetArgument<string>("text"))
            );

            Field<NonNullGraphType<ListGraphType<ReviewType>>>(
                "reviews",
              arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<EpisodeEnum>> { Name = "episode", Description = "Episode of star wars" }
              ),
                 resolve: context => data.GetReview(context.GetArgument<Episodes>("episode"))
             );

            Field<CharacterInterface>(
                "hero",
                arguments: new QueryArguments(
                    new QueryArgument<EpisodeEnum> { Name = "episode", Description = "Episode of star wars", DefaultValue = Episodes.NEWHOPE }
                ),
                resolve: context => data.GetHero(context.GetArgument<Episodes>("episode", Episodes.NEWHOPE))
            );

            Field<CharacterInterface>(
                "character",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id", Description = "id of the character" }
                ),
                resolve: context => data.GetCharacter(context.GetArgument<string>("id"))
            );

            Field<HumanType>(
                "human",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id", Description = "id of the human" }
                ),
                resolve: context => data.GetHumanByIdAsync(context.GetArgument<string>("id"))
            );

            Field<DroidType>(
                "droid",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id", Description = "id of the droid" }
                ),
                resolve: context => data.GetDroidByIdAsync(context.GetArgument<string>("id"))
            );

            Field<StarshipType>(
                "starship",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id", Description = "id of the starship" }
                ),
                resolve: context => data.GetStarShipByIdAsync(context.GetArgument<string>("id"))
            );

            Field<SpecieType>(
                "species",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType> { Name = "id", Description = "id of the specie" }
                ),
                resolve: context => eData.GetSpecie(context.GetArgument<string>("id"))
            );

            Field<HomeworldType>(
                "homeworld",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType> { Name = "id", Description = "id of the homeworld" }
                ),
                resolve: context => eData.GetHomeWorld(context.GetArgument<string>("id"))
            );

            Field<ListGraphType<NonNullGraphType<HomeworldType>>>(
                "allHomeworlds",
                resolve: context => eData.GetHomeWorlds()
            );
        }
    }
}
