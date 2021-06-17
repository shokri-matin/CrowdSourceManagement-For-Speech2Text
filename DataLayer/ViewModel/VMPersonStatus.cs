using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class VMPersonStatus
    {
        public string PersonName { get; set; }
        public int TaskCompleted { get; set; }
        public bool HaveProgressTask { get; set; }
        public DateTime LastLoginTime { get; set; }
        public int FileCompeleted { get; set; }
        public int TodayFileCompleted { get; set; }
    }
}
