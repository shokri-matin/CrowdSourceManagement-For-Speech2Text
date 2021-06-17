using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class VMUserHomeProfile
    {
        public int UserID { get; set; }
        public int WithdrawTasks { get; set; }
        public int InProgressTasks { get; set; }
        public float ProgressActiveTask { get; set; }
        public int CompeletedTasks { get; set; }
        public int TotalCompeleteFile { get; set; }
        public List<PersonActivityLog>  personActivityLogs { get; set; }
    }
}
