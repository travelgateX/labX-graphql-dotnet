using GraphQL.StarWars.Data;
using GraphQL.StarWars.Enum;
using GraphQL.StarWars.Types;
using GraphQL.Types;

namespace GraphQL.StarWars.Resolve
{
    public class DroidType : ObjectGraphType<Droid>
    {
        public DroidType(StarWarsData data)
        {
            Name = "Droid";
            Description = "A mechanical creature in the Star Wars universe.";

            Field<NonNullGraphType<IdGraphType>>("id", "The id of the droid.");

            //Field(d => d.Id).Description("The id of the droid.");
            Field(d => d.Name, nullable: false).Description("The name of the droid.");

            Field<ListGraphType<CharacterInterface>>(
                "friends",
                resolve: context => context.Source.Friends
            );
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<EpisodeEnum>>>>("appearsIn", "Which movie they appear in.");
            Field(d => d.PrimaryFunction, nullable: true).Description("The primary function of the droid.");

            Interface<CharacterInterface>();
        }
    }
}
