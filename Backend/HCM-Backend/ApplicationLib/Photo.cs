using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLib
{
    public class Photo
    {
        public bool UseAsEmployeePhoto { get; set; }
        public string AssignmentType { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public int Size { get; set; }
        public string Mimetype { get; set; }
        public string Description { get; set; }
        public string DocumentBytes { get; set; }
    }
}
