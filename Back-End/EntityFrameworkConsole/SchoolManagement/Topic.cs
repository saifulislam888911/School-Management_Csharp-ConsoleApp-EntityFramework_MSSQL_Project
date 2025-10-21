using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement
{
    public class Topic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public uint Duration { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }
    }
}
