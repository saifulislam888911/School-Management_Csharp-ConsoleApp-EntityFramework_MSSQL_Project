using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public uint Fees { get; set; }

        public List<Topic> Topics { get; set; }
        public List<CourseStudent> Students { get; set; }   // [NOTE : CourseStudent is middle table here.]
    }
}
