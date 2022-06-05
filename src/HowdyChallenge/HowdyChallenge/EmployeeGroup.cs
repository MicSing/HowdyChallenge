using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HowdyChallenge
{
    internal class EmployeeGroup
    {
        public int Id { get; set; }
        public List<Employee> Employees { get; set; } = new List<Employee>();

        public float GroupEvaluation { get; private set; } = -1;
        
        /// <summary>
        /// Evaluates group of employees.
        /// Gets evaluation of every employee of group and makes average.
        /// </summary>
        /// <returns></returns>
        public float EvaluateGroup()
        {
            if (this.GroupEvaluation == -1)
            {
                List<Employee> emplIds = Employees.GroupBy(em => em.employeeId).Select(em => em.First()).ToList();

                float ev = 0;
                emplIds.ForEach(id => ev += this.EvaluateIndividual(id.employeeId));
                this.GroupEvaluation = ev / emplIds.Count;
            }
            return this.GroupEvaluation;
        }

        /// <summary>
        /// Evaluate employee by ID.
        /// Takes highest values of months and makes average of them.
        /// </summary>
        /// <param name="id">Employee's ID</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public float EvaluateIndividual(int id)
        {
            List<Employee> empl = Employees.FindAll(em => em.employeeId == id);
            if (empl.Count == 0)
            {
                throw new Exception("Unable to find employee with specified ID.");
            }
            List<float> ev = Enumerable.Repeat(-1f, 12).ToList<float>();
            empl.ForEach(em => {
                int month = em.answeredOn.Month;
                float newVal = em.Evaluate();
                if (ev[month] < newVal)
                {
                    ev[month] = newVal;
                }
            });
            float evaluatedMonthsSum = ev.Sum(eval => { return eval >= 0 ? eval : 0f; });
            int evaluatedMonthsCount = ev.Sum(eval => { return eval >= 0 ? 1 : 0; });
            return evaluatedMonthsSum / evaluatedMonthsCount;
        }
    }
}
