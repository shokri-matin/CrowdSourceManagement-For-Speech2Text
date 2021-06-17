using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class MVPagination
    {
        public List<MVTask> MVTask { get; set; }
        public int CurrentPage { get; set; }
        public int Count { get; set; }
    }
}
