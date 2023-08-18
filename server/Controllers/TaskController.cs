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
        public IActionResult GetTaskDataValidation<T>(Guid id, object? repoCallFunc, bool doesExist)
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
        public IActionResult GetTasks(Guid day_plan_id)
        {
            return GetTaskDataValidation<List<TaskDto>>(day_plan_id, _repo.GetTasks(day_plan_id), _repo.DayPlanExists(day_plan_id));
        }

        [HttpGet("id/{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TaskDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetTask(Guid id)
        {
            return GetTaskDataValidation<Model.Task>(id, _repo.GetTask(id), _repo.TaskExists(id));
        }

        [HttpGet("milestones/{task_id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TaskDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetMilestonesByTask(Guid task_id)
        {
            return GetTaskDataValidation<MilestoneDto>(task_id, _repo.GetMilestonesByTask(task_id), _repo.TaskExists(task_id));

        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult CreateTask([FromBody] CreateTaskDto newTask)
        {
            if (newTask == null)
                return BadRequest(ModelState);

            if (!_repo.DayPlanExists(newTask.day_plan_id))
                return NotFound("Day plan does not exist.");

            if (_repo.TaskExists(newTask.id))
            {
                ModelState.AddModelError("", "Task already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var taskMap = _mapper.Map<Model.Task>(newTask);

            if (!_repo.CreateTask(taskMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving the day plan");
                return StatusCode(500, ModelState);
            }

            return Ok(taskMap);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTask(Guid id, [FromBody] Model.Task updatedTask)
        {
            if (updatedTask == null)
                return BadRequest("No new data added");

            if (!_repo.TaskExists(id))
                return NotFound("Task does not exist");

            updatedTask.id = id;
            updatedTask.updated_at = DateTime.UtcNow;

            var taskMap = _mapper.Map<Model.Task>(updatedTask);

            if (!_repo.UpdateTask(taskMap))
            {
                ModelState.AddModelError("", "Something went wrong with updating the task.");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTask(Guid id)
        {
            if (!_repo.TaskExists(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var taskToDelete = _repo.GetTask(id);

            if (taskToDelete == null)
                return NotFound();

            if (!_repo.DeleteTask(taskToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting the task");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}