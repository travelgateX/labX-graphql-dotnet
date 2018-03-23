﻿using System;
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
        public StarWarsQuery(StarWarsData data)
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
                "review",
              arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<EpisodeEnum>> { Name = "Episode", Description = "Episode of star wars" }
              ),
                 resolve: context => data.GetReview(context.GetArgument<Episodes>("Episode"))
             );

            Field<CharacterInterface>(
                "hero",
                arguments: new QueryArguments(
                    new QueryArgument<EpisodeEnum> { Name = "Episode", Description = "Episode of star wars", DefaultValue = Episodes.NEWHOPE }
                ),
                resolve: context => data.GetHero(context.GetArgument<Episodes>("Episode", Episodes.NEWHOPE))
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

            Field<StarShipType>(
                "starship",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id", Description = "id of the starship" }
                ),
                resolve: context => data.GetStarShipByIdAsync(context.GetArgument<string>("id"))
            );
        }
    }
}
