using System.Runtime.CompilerServices;

namespace SomeAPIEFCore.Data.Entities
{
    public class RoadMapCategory
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<RoadMap> roadMaps { get; set; }

        public RoadMapCategory()
        {
            roadMaps = new HashSet<RoadMap>();
        }
    }
}
