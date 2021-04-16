using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace A1.Models
{
    public class CommunityMembership
    {
        [Key, Column(Order = 0)]
        public int StudentID {
            get; set; 
        }
        public Student Student { 
            get; set; 
        }

        [Key, Column(Order = 1)]
        public string CommunityID { 
            get; set; 
        }
        public Community Community { 
            get; set; 
        }
    }
}
