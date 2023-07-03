using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using server.Interface;
using server.Model;
using server.Dto;


namespace server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITaskRepository _repo;
        public TaskController(ITaskRepository _repo, IMapper _mapper)
        {
            this._repo = _repo;
            this._mapper = _mapper;

        }

        // Utility method to return valid data
        public IActionResult GetTaskDataValidation<T>(int id, object? repoCallFunc, bool doesExist)
        {

            if (!doesExist)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var tasks = _mapper.Map<T>(repoCallFunc);

            if (tasks == null)
                return NotFound();

            return Ok(tasks);
        }


        [HttpGet("{day_plan_id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TaskDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetTasks(int day_plan_id)
        {
            return GetTaskDataValidation<List<TaskDto>>(day_plan_id, _repo.GetTasks(day_plan_id), _repo.TaskExists(day_plan_id));
        }

        [HttpGet("id/{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TaskDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetTask(int id)
        {
            return GetTaskDataValidation<TaskDto>(id, _repo.GetTask(id), _repo.TaskExists(id));
        }

        [HttpGet("milestones/{task_id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TaskDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetMilestonesByTask(int task_id)
        {
            return GetTaskDataValidation<MilestoneDto>(task_id, _repo.GetMilestonesByTask(task_id), _repo.TaskExists(task_id));

        }

    }
}