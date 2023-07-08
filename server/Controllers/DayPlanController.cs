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

        [HttpGet("tasks/{id}")]
        [ProducesResponseType(200, Type = typeof(TaskDto))]
        public IActionResult GetTasksByDayPlan(Guid day_plan_id)
        {
            if (!_repo.DayPlanExists(day_plan_id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var tasks = _mapper.Map<List<TaskDto>>(_repo.GetTasksByDayPlan(day_plan_id));

            return Ok(tasks);
        }

        [HttpPost]
        [ProducesResponseType(204)]
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
                ModelState.AddModelError("", "User already exists");
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

            if (!ModelState.IsValid)
                return BadRequest();

            var dayPlanMap = _mapper.Map<DayPlan>(updatedDayPlan);

            if (!_repo.UpdateDayPlan(id, dayPlanMap))
            {
                ModelState.AddModelError("", "Something went wrong updating the day plan.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

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

            if (!_repo.DeleteDayPlan(dayPlanToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting the day plan.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}