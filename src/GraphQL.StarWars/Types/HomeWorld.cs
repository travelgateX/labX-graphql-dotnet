using GraphQL.StarWars.Types;


namespace GraphQL.StarWars.Types
{
    public class Homeworld 
    {
        public string name { get; set; }

        public Specie[] species { get; set; }

    }
}
