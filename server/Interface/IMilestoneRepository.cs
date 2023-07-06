using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Model;

namespace server.Interface
{
    public interface IMilestoneRepository
    {
        ICollection<Milestone> GetMilestones(Guid task_id);
        Milestone? GetMilestone(Guid id);
        bool MilestoneExists(Guid id);
        bool TaskExists(Guid task_id);
    }
}