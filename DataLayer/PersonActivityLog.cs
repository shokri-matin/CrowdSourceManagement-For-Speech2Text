using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    [Table("PersonActivityLog")]
    public class PersonActivityLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }

        [ForeignKey("Person")]
        public int PersonID { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0: yyyy/MM/dd}")]
        public DateTime LoginTime { get; set; }

        [Required]
        public int ActivityStatus { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public virtual Person Person { get; set; }

    }
}
