using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Data;
using server.Interface;
using server.Model;

namespace server.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ServerDBContext _context;
        public TaskRepository(ServerDBContext _context)
        {
            this._context = _context;

        }

        public ICollection<Model.Task> GetTasks(int day_plan_id)
        {
            return _context.Tasks.Where(t => t.day_plan_id == day_plan_id).ToList();
        }

        public Model.Task? GetTask(int id)
        {
            return _context.Tasks.Where(t => t.id == id).FirstOrDefault();
        }

        public bool TaskExists(int id)
        {
            return _context.Tasks.Any(t => t.id == id);
        }

        public bool DayPlanExists(int day_plan_id)
        {
            return _context.DayPlans.Any(dp => dp.id == day_plan_id);
        }

        public ICollection<Milestone> GetMilestonesByTask(int task_id)
        {
            return _context.Milestones.Where(m => m.task_id == task_id).ToList();
        }
    }
}