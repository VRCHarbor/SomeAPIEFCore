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
using Microsoft.AspNetCore.Components.Forms;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace SomeAPIEFCore.Controllers
{
    [Route("element")]
    public class RoadMapElementController : Controller
    {
        private readonly ILogger<RoadMapElementController> _logger;
        private readonly RoadMapDbContext _ctx;
        private readonly IMapper _mapper;
        public RoadMapElementController(ILogger<RoadMapElementController> logger,
            RoadMapDbContext ctx,
            IMapper mapper)
        {
            _logger = logger;
            _ctx = ctx;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getElement(int id)
        {
                var element = _ctx.roadMapElements.Where(x => x.Id == id)
                .ProjectTo<RoadMapElementDTO>(_mapper.ConfigurationProvider).FirstOrDefault();
                return Json(element);
        }

        [HttpPost]
        public async Task<IActionResult> setNewElement(RoadMapElementDTO el)
        {
            try
            {
                _ctx.roadMapElements.Add(new RoadMapElement(el));
                _ctx.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPatch("{Id}")]
        public async Task<IActionResult> updateElementData([FromRoute] int Id, [FromForm] string content)
        {
            try
            {
                var element = _ctx.roadMapElements.FirstOrDefault(i => i.Id == Id);
                element.EditContent(content);
                _ctx.Update(element);
                _ctx.SaveChanges();
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteElement([FromRoute] int id)
        {
            try
            {
                var element = _ctx.roadMapElements.FirstOrDefault(i => i.Id == id);
                _ctx.Remove(element);
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
