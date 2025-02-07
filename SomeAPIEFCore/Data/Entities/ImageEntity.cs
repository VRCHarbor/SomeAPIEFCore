using SomeAPIEFCore.DTO;
using System.Reflection.PortableExecutable;
using System.Text;

namespace SomeAPIEFCore.Data.Entities
{
    public class ImageEntity
    {
        public ImageEntity() { }
        public ImageEntity(IFormFile file)
        {
            Name = file.FileName;
            DateOfLoad = DateTime.Now;

            byte[] data;

            using (var Reader = new MemoryStream())
            {
                var handler = file.CopyToAsync(Reader);
                handler.Wait();
                data = Reader.ToArray();
            }

            Data = data;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
        public DateTime DateOfLoad { get; set; }
    }
}
