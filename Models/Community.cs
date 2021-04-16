using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace A1.Models
{
    public class Community
    {
        [Key]
        [Required]
        [DisplayName("Registration Number")]
        [UIHint("_TextEditor")]
        public string ID { 
            get; set; 
        }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        [UIHint("_TextEditor")]
        public string Title { 
            get; set; 
        }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "${0:0.00}")]
        [UIHint("_DecimalEditor")]
        public decimal Budget { 
            get; set;
        }

        public IList<CommunityMembership> Membership {
            get; set; 
        }

        public IList<Advertisement> Advertisements {
            get; set; 
        }
    }
}
