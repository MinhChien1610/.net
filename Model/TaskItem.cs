using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuan_05.Model
{
    public class TaskItem
    {
        public string Name { get; set; }
        public string Priority { get; set; }
        public bool IsCompleted { get; set; }
        public string Note { get; set; }
    }
}