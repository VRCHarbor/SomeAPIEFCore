using SomeAPIEFCore.DTO;
using System.Globalization;
using System;
using System.Text;


namespace SomeAPIEFCore.Data.Entities
{
    public class RoadMapElement
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime EditDate { get; set; }
        public int StepNumber { get; set; }
        public RoadMap Host { get; set; }
        public int RoadMapId { get; set; }

        public RoadMapElement() { }

        public RoadMapElement(RoadMapElementDTO source)
        {
            Title = source.Title; 
            Content = source.Content;
            EditDate = DateTime.UtcNow;
            StepNumber = source.StepNumber;
        }

        public void Edit(RoadMapElementDTO source)
        {
            Title = source.Title;
            Content = source.Content;
            EditDate = DateTime.UtcNow;
            StepNumber = source.StepNumber;
        }

        public void EditContent(string content)
        {
            Content = content;
            EditDate = DateTime.UtcNow;
        }
    }
}
