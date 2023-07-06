using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.Interface;
using server.Model;
using server.Dto;
using AutoMapper;

namespace server.Controllers
{
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

        [HttpGet("{task_id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<MilestoneDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetMilestones(Guid task_id)
        {
            return GetMilestoneDataValidator<List<MilestoneDto>>(task_id, _repo.GetMilestones(task_id), _repo.TaskExists(task_id));
        }

        [HttpGet("id/{id}")]
        [ProducesResponseType(200, Type = typeof(MilestoneDto))]
        [ProducesResponseType(400)]
        public IActionResult GetTask(Guid id)
        {
            return GetMilestoneDataValidator<MilestoneDto>(id, _repo.GetMilestone(id), _repo.MilestoneExists(id));
        }
    }
}