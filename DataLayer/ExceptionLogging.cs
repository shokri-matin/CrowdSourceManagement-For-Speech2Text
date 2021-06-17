using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    [Table("ExceptionsLogging")]
    public class ExceptionLogging
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid LogId { get; set; }

        public int PersonID { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public string Exception { get; set; }

        public string InnerException { get; set; }

        public DateTime LogDate { get; set; }

        public virtual Person Person { get; set; }

    }
}
