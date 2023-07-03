using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Model;

namespace server.Interface
{
    public interface IDayPlanRepository
    {
        ICollection<DayPlan> GetDayPlans(int user_id);
        DayPlan? GetDayPlan(int id);
        bool DayPlanExists(int id);
        bool UserExists(int user_id);
        ICollection<Model.Task> GetTasksByDayPlan(int day_plan_id);
    }
}