using A1.Models;
using System.Collections.Generic;

namespace A1.ViewModels
{
    public class StudentsViewModel
    {
        public IList<Student> Students { 
            get; set; 
        }
        public Student Selected {
            get; set; 
        }
    }

    public class StudentMembershipsViewModel
    {
        public Student Student { 
            get; set; 
        }
        public IList<Community> Communities {
            get; set; 
        }
    }

    public class CommunitiesViewModel
    {
        public IList<Community> Communities {
            get; set; 
        }
        public Community Selected {
            get; set; 
        }
    }
}
