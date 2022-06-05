using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HowdyChallenge
{
    public class Employee
    {
        public int employeeId { get; set; }
        public int groupId { get; set; }
        public DateTimeOffset answeredOn { get; set; }
        public int answer1 { get; set; }
        public int answer2 { get; set; }
        public int answer3 { get; set; }
        public int answer4 { get; set; }
        public int answer5 { get; set; }

        public float Evaluate()
        {
            return (answer1 + answer2 + answer3 + answer4 + answer5) / 5;
        }
    }
}