using GraphQL.Types;

namespace GraphQL.StarWars.Types
{
    public class CharacterInterface : InterfaceGraphType<StarWarsCharacter>
    {
        public CharacterInterface()
        {
            Name = "Character";

            Field<NonNullGraphType<IdGraphType>>("id", "The id of the character.");
            Field(d => d.Name, nullable: false).Description("The name of the character.");

            Field<ListGraphType<CharacterInterface>>("friends");
            Field<NonNullGraphType<ListGraphType<NonNullGraphType<EpisodeEnum>>>>("appearsIn", "Which movie they appear in.");
        }
    }
}
