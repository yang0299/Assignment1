using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace A1.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { 
            get; set; 
        }

        [Required]
        [StringLength(50)]
        [DisplayName("First Name")]
        [UIHint("_TextEditor")]
        public string FirstName {
            get; set; 
        }

        [Required]
        [StringLength(50)]
        [DisplayName("Last Name")]
        [UIHint("_TextEditor")]
        public string LastName {
            get; set; 
        }

        public string FullName
        {
            get
            {
                return LastName + ", " + FirstName;
            }
        }

        [Required]
        [DisplayName("Enrollment Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [UIHint("_DateEditor")]
        public DateTime EnrollmentDate {
            get; set; 
        }

        public IList<CommunityMembership> Membership {
            get; set; 
        }
    }
}
