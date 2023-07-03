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
        public IActionResult GetDayPlans(int user_id)
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
        public IActionResult GetDayPlan(int id)
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
        public IActionResult GetTasksByDayPlan(int day_plan_id)
        {
            if (!_repo.DayPlanExists(day_plan_id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var tasks = _mapper.Map<List<TaskDto>>(_repo.GetTasksByDayPlan(day_plan_id));

            return Ok(tasks);
        }
    }
}