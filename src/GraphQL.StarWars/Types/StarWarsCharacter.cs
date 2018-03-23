
using GraphQL.StarWars.Enum;

namespace GraphQL.StarWars.Types
{
    public abstract class StarWarsCharacter
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public StarWarsCharacter[] Friends { get; set; }
        public Episodes[] AppearsIn { get; set; }
    }


}
