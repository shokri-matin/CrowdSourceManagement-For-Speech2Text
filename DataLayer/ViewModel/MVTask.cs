using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class MVTask
    {
        public Guid TaskID { get; set; }
        public Guid FileID { get; set; }
        public string SequenceId { get; set; }
        public int CreatorId { get; set; }
        public string SuggestedText { get; set; }
        public string Text { get; set; }
        public int Gender { get; set; }
        public string FileType { get; set; }
        public double FileDuration { get; set; }
        public bool IsSubmited { get; set; }
        public bool IsSupervised { get; set; }
        public int Count { get; set; }
    }
}
