using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gates.BL.Classes
{
    public class Task
    {
        public string Id { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public DateTime DeadlineDate { get; set; }

        public string Group { get; set; }

        public string Priority { get; set; }

        public string Status { get; set; }
    }
}
