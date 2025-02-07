using SomeAPIEFCore.Data.Entities;

namespace SomeAPIEFCore.DTO
{
    public class RoadMapElementDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string EditDate { get; set; }
        public int StepNumber { get; set; }

        public RoadMapElementDTO() { }
        public RoadMapElementDTO(RoadMapElement source)
        {

        }
    }
}
