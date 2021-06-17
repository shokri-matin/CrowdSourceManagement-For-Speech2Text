using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModel
{
    public class VMFileConfirmation
    {
        public Guid FileID { get; set; }
        public string FileName { get; set; }
        public string SequenceID { get; set; }
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public int CreatorId { get; set; }
        public string FileType { get; set; }
        public double FileDuration { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}
