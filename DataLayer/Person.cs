using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer
{
    [Table("Person")]
    public class Person
    {
        [Key]
        public int PersonID { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100)]
        [Display(Name = "نام و نام خانوادگی")]
        public string PersonName { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200)]
        [Display(Name = "ایمیل معتبر")]
        public string PersonEmail { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "فعال/غیر فعال")]
        public bool IsDeleted { get; set; }

        [Required]
        [Display(Name ="عنوان نقش")]
        public int RoleID { get; set; }

        public int? CreatorId { get; set; }

        [ForeignKey("CreatorId")]
        public virtual Person AdminPerson { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<PersonActivityLog> PersonActivityLog { get; set; }

        [Display(Name = "نقش کاربر")]
        public virtual Role Role { get; set; }
    }
}
