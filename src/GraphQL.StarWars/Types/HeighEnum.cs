using GraphQL.Types;

namespace GraphQL.StarWars.Types
{
    public class HeighEnum : EnumerationGraphType<LengthUnit>
    {
        public HeighEnum()
        {
            Name = "Heigh";
            Description = "Units of height";
            AddValue("METER", "The standard unit around the world", 0);
            AddValue("FOOT", "Primarily used in the United States", 1);
        }
    }

    public enum LengthUnit
    {
        METER  = 0,
        FOOT  = 1
    }
}
