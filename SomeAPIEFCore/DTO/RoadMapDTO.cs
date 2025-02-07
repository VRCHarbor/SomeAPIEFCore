using SomeAPIEFCore.Data.Entities;

namespace SomeAPIEFCore.DTO
{
    public class RoadMapDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<RoadMapElementDTO> RoadmapElements { get; set; }

        public RoadMapDTO() { }
        public RoadMapDTO(RoadMap source)
        {
            Id = source.Id;
            Name = source.Name;
        }
    }
}
