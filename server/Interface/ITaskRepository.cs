using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Model;

namespace server.Interface
{
    public interface ITaskRepository
    {
        ICollection<Model.Task> GetTasks(Guid day_plan_id);
        Model.Task? GetTask(Guid id);
        ICollection<Milestone> GetMilestonesByTask(Guid task_id);
        bool CreateTask(Model.Task task);
        bool UpdateTask(Model.Task task);
        bool DeleteTask(Model.Task id);
        bool TaskExists(Guid id);
        bool DayPlanExists(Guid day_plan_id);

    }
}