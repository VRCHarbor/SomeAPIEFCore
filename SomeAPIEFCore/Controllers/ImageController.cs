using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
using Microsoft;
using Microsoft.Data;
using System;
using System.Drawing;
using SomeAPIEFCore.DTO;
using SomeAPIEFCore.Data.Context;
using SomeAPIEFCore.Data.Entities;

namespace SomeAPIEFCore.Controllers
{
    [Route("image")]
    public class ImageController : Controller
    {
        private readonly ILogger<ImageController> _logger;
        private readonly RoadMapDbContext _ctx;
        public ImageController(ILogger<ImageController> logger,
            RoadMapDbContext ctx)
        {
            _logger = logger;
            _ctx = ctx;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetImage([FromRoute] int id)
        {
            if (!_ctx.images.Any(i => i.Id == id))
                return NotFound();

            //return Json(new { error = true, msg = "not found an image" });

            var img = _ctx.images.FirstOrDefault(i => i.Id == id);

            return File(img.Data, "image/jpeg");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage([FromRoute] int id)
        {
            if (!_ctx.images.Any(i => i.Id == id))
                return NotFound();
            var img = _ctx.images.FirstOrDefault(x => x.Id == id);

            _ctx.images.Remove(img);
            await _ctx.SaveChangesAsync();
            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> GetImages()
        {
            var img = _ctx.images.FirstOrDefault();
            return Json(new { ids = _ctx.images.Select(i => i.Id).ToArray() });
        }

        [HttpPost]
        public async Task<IActionResult> SetImage(IFormFile img)
        {
            try
            {
                if(img == null)
                    return NotFound();
                var imgToSave = new ImageEntity(img);
                _ctx.images.Add(imgToSave);
                _ctx.SaveChanges();
                var imgSaved = _ctx.images.FirstOrDefault(i => i.Data == imgToSave.Data && i.Name == imgToSave.Name);
                return Ok(new {imageId = imgSaved?.Id});
            }
            catch(Exception ex) 
            {
                return NotFound(new { error = true, msg = ex.Message });
            }
        }

        [HttpPost("many")]
        public async Task<IActionResult> SetImages(ICollection<IFormFile> img)
        {
            try
            {
                img.ToList().ForEach(i => _ctx.images.Add(new ImageEntity(i)));
                _ctx.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(new { error = true, msg = ex.Message });
            }
        }
    }
}