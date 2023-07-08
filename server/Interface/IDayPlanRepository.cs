using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Model;

namespace server.Interface
{
    public interface IDayPlanRepository
    {
        ICollection<DayPlan> GetDayPlans(Guid user_id);
        bool CreateDayPlan(DayPlan dayPlan);
        bool UpdateDayPlan(Guid id, DayPlan dayPlan);
        bool DeleteDayPlan(DayPlan dayPlan);
        DayPlan? GetDayPlan(Guid id);
        bool DayPlanExists(Guid id);
        bool UserExists(Guid user_id);
        ICollection<Model.Task> GetTasksByDayPlan(Guid day_plan_id);
        bool Save();
    }
}