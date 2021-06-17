using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class AllocatedFile
    {
        [Key, Column(Order = 0)]
        public Guid FileID { get; set; }

        [Key, Column(Order = 1)]
        public Guid TaskID { get; set; }
        public string Text { get; set; }
        public bool IsSubmited { get; set; } // Is Submited?
        public bool IsSupervised { get; set; }
        public int Gender { get; set; }
        public DateTime? SubmitedTime { get; set; }
        public virtual SpeechFile SpeechFile { get; set; }
        public virtual Task Task { get; set; }
    }
}