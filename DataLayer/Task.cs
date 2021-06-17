using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    [Table("Task")]
    public class Task
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid TaskID { get; set; }

        [Required]
        public int PersonID { get; set; }

        [Required]
        public DateTime StartTaskDate { get; set; }

        [Required]
        public DateTime EndTaskDate { get; set; }

        [Required]
        public int Status { get; set; }

        public virtual Person Person { get; set; }
    }
}
