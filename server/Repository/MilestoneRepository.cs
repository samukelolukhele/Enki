using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Data;
using server.Interface;
using server.Model;

namespace server.Repository
{
    public class MilestoneRepository : IMilestoneRepository
    {
        private readonly ServerDBContext _context;
        public MilestoneRepository(ServerDBContext _context)
        {
            this._context = _context;

        }

        public ICollection<Milestone> GetMilestones(Guid task_id)
        {
            return _context.Milestones.Where(m => m.task_id == task_id).ToList();
        }

        public Milestone? GetMilestone(Guid id)
        {
            return _context.Milestones.Where(m => m.id == id).FirstOrDefault();
        }

        public bool MilestoneExists(Guid id)
        {
            return _context.Milestones.Any(m => m.id == id);
        }
        public bool TaskExists(Guid id)
        {
            return _context.Tasks.Any(t => t.id == id);
        }
    }
}