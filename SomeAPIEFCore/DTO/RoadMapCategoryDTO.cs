using SomeAPIEFCore.Data.Entities;


namespace SomeAPIEFCore.DTO
{
    public class RoadMapCategoryDTO
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public RoadMapCategoryDTO(RoadMapCategory source) 
        {
            Id = source.Id;
            Name = source.Name;
        }
    }
}
