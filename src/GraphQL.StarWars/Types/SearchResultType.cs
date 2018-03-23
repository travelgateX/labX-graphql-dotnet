using GraphQL.Types;
using GraphQL.StarWars.Types;

namespace GraphQL.StarWars.Types
{

    //union SearchResult = Human | Droid | Starship
    public class SearchResultType : UnionGraphType
    {
        public SearchResultType(StarWarsData data)
        {
            var nestedObjType = new HumanType(data)
            {
                Name = "Human"
            };
            var nestedObjType1 = new DroidType(data)
            {
                Name = "Droid"
            };
            var nestedObjType2 = new StarShipType(data)
            {
                Name = "Starship"
            };

            AddPossibleType(nestedObjType);
            AddPossibleType(nestedObjType1);
            AddPossibleType(nestedObjType2);
        }

    }
}