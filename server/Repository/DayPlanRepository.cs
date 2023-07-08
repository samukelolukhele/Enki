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

        public ICollection<DayPlan> GetDayPlans(Guid user_id)
        {
            return _context.DayPlans.Where(dp => dp.user_id == user_id).ToList();
        }

        public DayPlan? GetDayPlan(Guid id)
        {
            return _context.DayPlans.Where(dp => dp.id == id).FirstOrDefault();
        }

        public bool DayPlanExists(Guid id)
        {
            return _context.DayPlans.Any(dp => dp.id == id);
        }

        public bool UserExists(Guid user_id)
        {
            return _context.Users.Any(u => u.id == user_id);
        }
        public ICollection<Model.Task> GetTasksByDayPlan(Guid day_plan_id)
        {
            return _context.Tasks.Where(t => t.day_plan_id == day_plan_id).ToList();
        }

        public bool CreateDayPlan(DayPlan dayPlan)
        {
            dayPlan.id = Guid.NewGuid();
            _context.DayPlans.Add(dayPlan);
            return Save();
        }
        public bool UpdateDayPlan(Guid id, DayPlan dayPlan)
        {
            var exist = _context.DayPlans.Where(dp => dp.id == id).FirstOrDefault();

            if (exist == null)
                return false;

            exist.title = dayPlan.title;
            exist.description = dayPlan.description;
            exist.updated_at = DateTime.UtcNow;

            return Save();
        }

        public bool DeleteDayPlan(DayPlan dayPlan)
        {
            _context.DayPlans.Remove(dayPlan);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}