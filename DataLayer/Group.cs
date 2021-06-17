using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Group
    {
        [Key]
        public int GroupID { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200)]
        [Display(Name = "عنوان گروه")]
        public string GroupName { get; set; }

        public virtual ICollection<SpeechFile> SpeechFile { get; set; }
    }
}
