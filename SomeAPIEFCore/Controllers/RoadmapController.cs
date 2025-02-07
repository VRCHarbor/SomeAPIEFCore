using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
using Microsoft;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data;
using System;
using System.Drawing;
using SomeAPIEFCore.DTO;
using SomeAPIEFCore.Data.Context;
using SomeAPIEFCore.Data.Entities;
using SomeAPIEFCore.Data.Migrations;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace SomeAPIEFCore.Controllers
{
    [Route("roadmap")]
    public class RoadmapController : Controller
    {

        private readonly ILogger<RoadmapController> _logger;
        private readonly RoadMapDbContext _ctx;
        private readonly IMapper _mapper;

        public RoadmapController(
            ILogger<RoadmapController> logger,
            RoadMapDbContext ctx,
            IMapper mapper)
        {
            _logger = logger;
            _ctx = ctx;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoadmap([FromRoute] int id)
        {
                var roadMap = _ctx.roadMaps.Where(r => r.Id == id)
                    .ProjectTo<RoadMapDTO>(_mapper.ConfigurationProvider).FirstOrDefault();
                return Json(roadMap); 
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> insertNewElement([FromRoute] int id, [FromQuery] string title)
        {
            try
            {
                var rmEls = _ctx.roadMapElements.Where(i => i.RoadMapId == id).ToList();
                RoadMapElement newEl = new RoadMapElement() { Title = title, EditDate = DateTime.UtcNow, RoadMapId = id, Content=""};

                if (rmEls.Count != 0)
                    newEl.StepNumber = rmEls.Max(i => i.StepNumber) + 1;
                else
                    newEl.StepNumber = 0;

                _ctx.roadMapElements.Add(newEl);
                _ctx.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetRoadmaps()
        {
            try
            {
                var roadMaps = _ctx.roadMaps;
                return Json(roadMaps.
                    Select(i=> new 
                    { 
                        Name = i.Name, 
                        CategoryID = i.CategoryId, 
                        CategoryName= i.Category.Name, 
                        Elements = i.RoadMapElements.Select(i => new RoadMapElementDTO(i)).ToArray() }));
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteRoadmap([FromRoute] int id)
        {
            try
            {
                var roadMap = _ctx.roadMaps.FirstOrDefault(i => i.Id == id);
                _ctx.Remove(roadMap);
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
