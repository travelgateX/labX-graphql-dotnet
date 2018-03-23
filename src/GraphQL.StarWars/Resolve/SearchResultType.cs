using GraphQL.StarWars.Data;
using GraphQL.Types;

namespace GraphQL.StarWars.Resolve
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
                Name = "StarShip"
            };

            AddPossibleType(nestedObjType);
            AddPossibleType(nestedObjType1);
            AddPossibleType(nestedObjType2);
        }

    }
}