using GraphQL.StarWars.Types;


namespace GraphQL.StarWars.Types
{
    public class Specie 
    {
        public string designation { get; set; }

        public string language { get; set; }

        public Specie[] subEspecies { get; set; }

        public Homeworld homeworld { get; set; }

        public Specie[] species { get; set; }

    }
}
