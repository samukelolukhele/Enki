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

    //!DELETE AFTER TESTING API cc14abc0-13d2-4136-91de-fcddad6c175b
    public class DayPlanRepository : IDayPlanRepository
    {
        private readonly ServerDBContext _context;
        private readonly IUtilRepository _util;
        public DayPlanRepository(ServerDBContext _context, IUtilRepository _util)
        {
            this._util = _util;
            this._context = _context;

        }

        public ICollection<DayPlan> GetDayPlans(Guid user_id)
        {
            return _util.GetList<DayPlan>(dp => dp.user_id == user_id);
        }

        public DayPlan? GetDayPlan(Guid id)
        {
            return _util.Get<DayPlan>(dp => dp.id == id);
        }

        public bool DayPlanExists(Guid id)
        {
            return _util.DoesExist<DayPlan>(dp => dp.id == id);
        }

        public bool UserExists(Guid user_id)
        {
            return _util.DoesExist<User>(u => u.id == user_id);
        }
        public ICollection<Model.Task> GetTasksByDayPlan(Guid day_plan_id)
        {
            return _util.GetList<Model.Task>(t => t.day_plan_id == day_plan_id).ToList();
        }

        public bool CreateDayPlan(DayPlan dayPlan)
        {
            return _util.Create<DayPlan>(dayPlan);
        }
        public bool UpdateDayPlan(DayPlan dayPlan)
        {
            return _util.Update<DayPlan>(dayPlan, dp => dp.user_id, dp => dp.id, dp => dp.created_at);
        }

        public bool DeleteDayPlan(DayPlan dayPlan)
        {
            return _util.Delete<DayPlan>(dayPlan);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}