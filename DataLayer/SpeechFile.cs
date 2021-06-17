using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer
{
    [Table("SpeechFiles")]
    public class SpeechFile
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid FileID { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public string SequenceID { get; set; }

        [Required]
        public int GroupID { get; set; }

        public int CreatorId { get; set; }

        [Required]
        public string FileType { get; set; }

        [Required]
        public double FileDuration { get; set; }

        public string SuggestedText { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public DateTime PublishedDate { get; set; }

        public virtual ICollection<AllocatedFile> AllocatedFiles { get; set; }
        public virtual Group Group { get; set; }

        [ForeignKey("CreatorId")]
        public virtual Person Person { get; set; }
    }
}
