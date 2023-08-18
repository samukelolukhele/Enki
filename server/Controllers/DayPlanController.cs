using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using server.Repository;
using server.Interface;
using server.Model;
using server.Dto;
using Microsoft.AspNetCore.Authorization;

namespace server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DayPlanController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDayPlanRepository _repo;
        public DayPlanController(IDayPlanRepository _repo, IMapper _mapper)
        {
            this._repo = _repo;
            this._mapper = _mapper;

        }

        [Authorize]
        [HttpGet("{user_id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DayPlanDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetDayPlans(Guid user_id)
        {
            if (!_repo.UserExists(user_id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var dayPlans = _mapper.Map<List<DayPlanDto>>(_repo.GetDayPlans(user_id));

            return Ok(dayPlans);
        }

        [Authorize]
        [HttpGet("id/{id}")]
        [ProducesResponseType(200, Type = typeof(DayPlanDto))]
        [ProducesResponseType(400)]
        public IActionResult GetDayPlan(Guid id)
        {
            if (!_repo.DayPlanExists(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var dayPlan = _mapper.Map<DayPlanDto>(_repo.GetDayPlan(id));

            return Ok(dayPlan);

        }

        [Authorize]
        [HttpGet("tasks/{id}")]
        [ProducesResponseType(200, Type = typeof(TaskDto))]
        public IActionResult GetTasksByDayPlan(Guid id)
        {
            if (!_repo.DayPlanExists(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var tasks = _mapper.Map<List<TaskDto>>(_repo.GetTasksByDayPlan(id));

            return Ok(tasks);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult CreateDayPlan([FromBody] CreateDayPlanDto newDayPlan)
        {
            if (newDayPlan == null)
                return BadRequest(ModelState);

            if (!_repo.UserExists(newDayPlan.user_id))
                return NotFound("User does not exist.");

            if (_repo.DayPlanExists(newDayPlan.id))
            {
                ModelState.AddModelError("", "Day plan already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var dayPlanMap = _mapper.Map<DayPlan>(newDayPlan);

            if (!_repo.CreateDayPlan(dayPlanMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving the day plan");
                return StatusCode(500, ModelState);
            }

            return Ok("Day plan successfully created!");
        }


        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDayPlan(Guid id, [FromBody] DayPlan updatedDayPlan)
        {

            if (updatedDayPlan == null)
                return BadRequest("No new data added.");

            if (!_repo.DayPlanExists(id))
                return NotFound("DayPlan does not exist.");

            updatedDayPlan.id = id;
            updatedDayPlan.updated_at = DateTime.UtcNow;

            if (!ModelState.IsValid)
                return BadRequest();

            var dayPlanMap = _mapper.Map<DayPlan>(updatedDayPlan);

            if (!_repo.UpdateDayPlan(dayPlanMap))
            {
                ModelState.AddModelError("", "Something went wrong updating the day plan.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteDayPlan(Guid id)
        {
            if (!_repo.DayPlanExists(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var dayPlanToDelete = _repo.GetDayPlan(id);

            if (dayPlanToDelete == null)
                return NotFound();

            if (!_repo.DeleteDayPlan(dayPlanToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting the day plan.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}