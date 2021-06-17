using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }

        [Display(Name ="عنوان نقش")]
        public string RoleName { get; set; }

        public virtual ICollection<Person> Person { get; set; }
    }
}
