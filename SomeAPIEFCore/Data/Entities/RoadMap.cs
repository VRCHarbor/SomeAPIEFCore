using SomeAPIEFCore.Data.Entities;
using SomeAPIEFCore.DTO;

namespace SomeAPIEFCore.Data.Entities
{
    public class RoadMap
    {

        public ICollection<RoadMapElement> RoadMapElements { get; set;}
        public int Id { get; set; }
        public string Name { get; set; }
        public RoadMapCategory Category { get; set; }
        public int CategoryId { get; set; }
        public RoadMap() { }
        public RoadMap(RoadMapDTO source, RoadMapCategory category):this()
        {
            Id = source.Id;
            Name = source.Name;
            Category = category;
        }
    }
}
