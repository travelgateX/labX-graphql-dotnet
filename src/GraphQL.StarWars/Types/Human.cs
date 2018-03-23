using GraphQL.StarWars.Types;


namespace GraphQL.StarWars.Types
{
    public class Human : StarWarsCharacter
    {
        public float Mass { get; set; }

        public float Height { get; set; }

        public Starship[] Starships { get; set; }

        public Homeworld homeworld{ get; set; }
    }
}
