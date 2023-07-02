using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Data;
using server.Interface;
using server.Model;

namespace server.Repository
{
    public class DayPlanRepository : IDayPlanRepository
    {
        private readonly ServerDBContext _context;
        public DayPlanRepository(ServerDBContext _context)
        {
            this._context = _context;

        }

        public ICollection<DayPlan> GetDayPlans()
        {
            return _context.DayPlans.OrderBy(dp => dp.id).ToList();
        }

        public DayPlan? GetDayPlan(int id)
        {
            return _context.DayPlans.Where(dp => dp.id == id).FirstOrDefault();
        }

        public bool DayPlanExists(int id)
        {
            return _context.DayPlans.Any(dp => dp.id == id);
        }

        public ICollection<Model.Task> GetTasksByDayPlan(int day_plan_id)
        {
            return _context.Tasks.Where(t => t.day_plan_id == day_plan_id).ToList();
        }
    }
}