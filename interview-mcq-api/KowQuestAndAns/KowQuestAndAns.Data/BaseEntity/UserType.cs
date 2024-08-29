using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KowQuestAndAns.Data.BaseEntity
{
    public class UserType
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool is_active { get; set; }
        public bool is_deleted { get; set; }
    }

}
