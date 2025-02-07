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
    [Route("category")]
    public class RoadmapCategoryController : Controller
    {

        private readonly ILogger<RoadmapCategoryController> _logger;
        private readonly RoadMapDbContext _ctx;

        public RoadmapCategoryController(
            ILogger<RoadmapCategoryController> logger,
            RoadMapDbContext ctx)
        {
            _logger = logger;
            _ctx = ctx;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            return Json(_ctx.roadMapCategories.Select(i => new RoadMapCategoryDTO(i)));
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetRoadMapsInCategory([FromRoute] int Id)
        {
            var category = _ctx.roadMapCategories.FirstOrDefault(i => i.Id == Id);
            return Json(_ctx.roadMaps.Where(i => i.CategoryId == category.Id).Select(i => new RoadMapDTO(i)));
        }

        [HttpPost]
        public async Task<IActionResult> AddNewCategory([FromQuery] string newCatName)
        {
            _ctx.roadMapCategories.Add(new RoadMapCategory() { Name = newCatName });
            _ctx.SaveChanges();
            return Ok();
        }

        [HttpPost("{CatId}")]
        public async Task<IActionResult> InsertRoadMapToCategory([FromRoute] int CatId, [FromQuery] string roadMapName)
        {
            try
            {
                var category = _ctx.roadMapCategories.FirstOrDefault(i => i.Id == CatId);

                var roadmap = new RoadMap() { Name= roadMapName , Category = category, CategoryId = category.Id};

                _ctx.roadMaps.Add(roadmap);
                _ctx.SaveChanges();
            }
            catch (Exception e)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            try
            {
                var category = _ctx.roadMapCategories.FirstOrDefault(i => i.Id==id);
                _ctx.Remove(category);
                _ctx.SaveChanges();
                return Ok();

            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

        }
    }
}
