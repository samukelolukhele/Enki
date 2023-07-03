using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Model;

namespace server.Interface
{
    public interface ITaskRepository
    {
        ICollection<Model.Task> GetTasks(int day_plan_id);
        Model.Task? GetTask(int id);
        ICollection<Milestone> GetMilestonesByTask(int task_id);
        bool TaskExists(int id);
        bool DayPlanExists(int day_plan_id);

    }
}