using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer
{
    public class VMLogin
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200)]
        [Display(Name = "ایمیل معتبر")]
        public string PersonEmail { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        public bool Remeber { get; set; }
    }
}
