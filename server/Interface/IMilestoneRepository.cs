using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Model;

namespace server.Interface
{
    public interface IMilestoneRepository
    {
        ICollection<Milestone> GetMilestones(int task_id);
        Milestone? GetMilestone(int id);
        bool MilestoneExists(int id);
        bool TaskExists(int task_id);
    }
}