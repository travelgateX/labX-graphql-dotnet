
namespace GraphQL.StarWars.Types
{
    public abstract class StarWarsCharacter
    {
        public string Id { get; set; }
        public string Name { get; set; }
 
        public string[] Friends { get; set; }
        public Episodes[] AppearsIn { get; set; }
    }

    public class Human : StarWarsCharacter
    {
        public float Mass { get; set; }

        public float Height { get; set; }

        public Starship[] Starships { get; set; }
    }

    public class Droid : StarWarsCharacter
    {
        public string PrimaryFunction { get; set; }
    }
}
