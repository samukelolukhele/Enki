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
        private readonly IDayPlanRepository _dayPlanRepository;
        public DayPlanController(IDayPlanRepository _dayPlanRepository, IMapper _mapper)
        {
            this._dayPlanRepository = _dayPlanRepository;
            this._mapper = _mapper;

        }

        [HttpGet("{user_id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DayPlan>))]
        public IActionResult GetDayPlans(int user_id)
        {
            if (!_dayPlanRepository.UserExists(user_id))
                return NotFound();

            var dayPlans = _mapper.Map<List<DayPlanDto>>(_dayPlanRepository.GetDayPlans(user_id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(dayPlans);
        }

        [HttpGet("id/{id}")]
        [ProducesResponseType(200, Type = typeof(DayPlan))]
        [ProducesResponseType(400)]
        public IActionResult GetDayPlan(int id)
        {
            if (!_dayPlanRepository.DayPlanExists(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var dayPlan = _mapper.Map<DayPlanDto>(_dayPlanRepository.GetDayPlan(id));

            return Ok(dayPlan);

        }

        [HttpGet("tasks/{id}")]
        [ProducesResponseType(200, Type = typeof(TaskDto))]
        public IActionResult GetTasksByDayPlan(int day_plan_id)
        {
            if (!_dayPlanRepository.DayPlanExists(day_plan_id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var tasks = _mapper.Map<List<TaskDto>>(_dayPlanRepository.GetTasksByDayPlan(day_plan_id));

            return Ok(tasks);
        }
    }
}