using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KowQuestAndAns.Data.BaseEntity
{
    public class ApiKeyInfo
    {
        public int Id { get; set; }
        public string api_key { get; set; }
        public bool? is_active { get; set; }
        public bool? is_deleted { get; set; }
    }

}
