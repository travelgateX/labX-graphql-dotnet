using GraphQL.StarWars.Data;
using GraphQL.Types;

namespace GraphQL.StarWars.Resolve
{

    //union SearchResult = Human | Droid | Starship
    public class SearchResultType : UnionGraphType
    {
        public SearchResultType(StarWarsData data, ExternalData eData)
        {
            var nestedObjType = new HumanType(data, eData)
            {
                Name = "Human"
            };
            var nestedObjType1 = new DroidType(data)
            {
                Name = "Droid"
            };
            var nestedObjType2 = new StarshipType(data)
            {
                Name = "Starship"
            };

            AddPossibleType(nestedObjType);
            AddPossibleType(nestedObjType1);
            AddPossibleType(nestedObjType2);
        }

    }
}