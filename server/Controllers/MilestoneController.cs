using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.Interface;
using server.Model;
using server.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace server.Controllers
{

    //!5d8e04e1-d8cb-4258-ad02-e239e142774d
    [ApiController]
    [Route("api/[controller]")]
    public class MilestoneController : ControllerBase
    {
        private readonly IMilestoneRepository _repo;
        private readonly IMapper _mapper;
        public MilestoneController(IMilestoneRepository _repo, IMapper _mapper)
        {
            this._mapper = _mapper;
            this._repo = _repo;
        }

        public IActionResult GetMilestoneDataValidator<T>(Guid id, object? repoCallFunc, bool doesExist)
        {

            if (!doesExist)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var milestones = _mapper.Map<T>(repoCallFunc);

            if (milestones == null)
                return NotFound();

            return Ok(milestones);
        }

        [Authorize]
        [HttpGet("{task_id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<MilestoneDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetMilestones(Guid task_id)
        {
            return GetMilestoneDataValidator<List<MilestoneDto>>(task_id, _repo.GetMilestones(task_id), _repo.TaskExists(task_id));
        }

        [Authorize]
        [HttpGet("id/{id}")]
        [ProducesResponseType(200, Type = typeof(MilestoneDto))]
        [ProducesResponseType(400)]
        public IActionResult GetTask(Guid id)
        {
            return GetMilestoneDataValidator<MilestoneDto>(id, _repo.GetMilestone(id), _repo.MilestoneExists(id));
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult CreateMilestone([FromBody] CreateMilestoneDto newMilestone)
        {
            if (newMilestone == null)
                return BadRequest(ModelState);

            if (!_repo.TaskExists(newMilestone.task_id))
                return NotFound("Task does not exist.");

            if (_repo.MilestoneExists(newMilestone.id))
            {
                ModelState.AddModelError("", "Milestone already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var MilestoneMap = _mapper.Map<Milestone>(newMilestone);

            if (!_repo.CreateMilestone(MilestoneMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving the mileston");
                return StatusCode(500, ModelState);
            }

            return Ok("Milestone successfully created!");
        }

        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult U(Guid id, [FromBody] Milestone updatedMilestone)
        {
            if (updatedMilestone == null)
                return BadRequest("No new data added.");

            if (!_repo.MilestoneExists(id))
                return NotFound();

            var milestoneMap = _mapper.Map<Milestone>(updatedMilestone);

            if (!_repo.UpdateMilestone(id, milestoneMap))
            {
                ModelState.AddModelError("", "Something went wrong updating the milestone.");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTask(Guid id)
        {
            if (!_repo.MilestoneExists(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var milestoneToDelete = _repo.GetMilestone(id);

            if (milestoneToDelete == null)
                return NotFound();

            if (!_repo.DeleteMilestone(milestoneToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting the milestone.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}