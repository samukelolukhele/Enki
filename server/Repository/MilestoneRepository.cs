using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Data;
using server.Interface;
using server.Model;
using server.Utils;

namespace server.Repository
{
    public class MilestoneRepository : IMilestoneRepository
    {
        private readonly ServerDBContext _context;
        private readonly IUtilRepository _util;
        public MilestoneRepository(ServerDBContext _context, IUtilRepository _util)
        {
            this._util = _util;
            this._context = _context;

        }

        public ICollection<Milestone> GetMilestones(Guid task_id)
        {
            return _util.GetList<Milestone>(m => m.task_id == task_id);
        }

        public Milestone? GetMilestone(Guid id)
        {
            return _util.Get<Milestone>(m => m.id == id);
        }

        public bool MilestoneExists(Guid id)
        {
            return _util.DoesExist<Milestone>(m => m.id == id);
        }
        public bool TaskExists(Guid id)
        {
            return _util.DoesExist<Model.Task>(t => t.id == id);
        }

        public bool CreateMilestone(Milestone milestone)
        {
            return _util.Create<Milestone>(milestone);
        }

        public bool UpdateMilestone(Guid id, Milestone milestone)
        {
            milestone.id = id;
            return _util.Update(milestone, m => m.id, m => m.task_id, m => m.created_at);
        }

        public bool DeleteMilestone(Milestone milestone)
        {
            return _util.Delete<Milestone>(milestone);
        }
    }
}