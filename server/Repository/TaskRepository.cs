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
    public class TaskRepository : ITaskRepository
    {
        private readonly ServerDBContext _context;
        private readonly IUtilRepository _util;
        public TaskRepository(ServerDBContext _context, IUtilRepository _util)
        {
            this._util = _util;
            this._context = _context;

        }

        public ICollection<Model.Task> GetTasks(Guid day_plan_id)
        {
            return _util.GetList<Model.Task>(t => t.day_plan_id == day_plan_id);
        }

        public Model.Task? GetTask(Guid id)
        {
            return _util.Get<Model.Task>(t => t.id == id);
        }

        public bool TaskExists(Guid id)
        {
            return _util.DoesExist<Model.Task>(t => t.id == id);
        }

        public bool DayPlanExists(Guid day_plan_id)
        {
            return _util.DoesExist<DayPlan>(dp => dp.id == day_plan_id);
        }

        public ICollection<Milestone> GetMilestonesByTask(Guid task_id)
        {
            return _util.GetList<Milestone>(m => m.task_id == task_id);
        }

        public bool CreateTask(Model.Task task)
        {
            return _util.Create<Model.Task>(task);
        }

        public bool UpdateTask(Model.Task task)
        {
            return _util.Update(task, t => t.id, t => t.day_plan_id, t => t.created_at);
        }

        public bool DeleteTask(Model.Task task)
        {
            return _util.Delete<Model.Task>(task);
        }
    }
}