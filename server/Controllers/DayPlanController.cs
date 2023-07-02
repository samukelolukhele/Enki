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

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DayPlan>))]
        public IActionResult GetDayPlans()
        {
            var dayPlans = _mapper.Map<List<DayPlanDto>>(_dayPlanRepository.GetDayPlans());

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

        // TODO: Replace type with TaskDto instead
        [HttpGet("tasks/{id}")]
        [ProducesResponseType(200, Type = typeof(Model.Task))]
        public IActionResult GetTasksByDayPlan(int day_plan_id)
        {
            if (!_dayPlanRepository.DayPlanExists(day_plan_id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var tasks = _mapper.Map<List<Model.Task>>(_dayPlanRepository.GetTasksByDayPlan(day_plan_id));

            return Ok(tasks);
        }
    }
}