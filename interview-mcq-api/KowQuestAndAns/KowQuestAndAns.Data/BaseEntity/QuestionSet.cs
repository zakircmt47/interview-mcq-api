using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KowQuestAndAns.Data.BaseEntity
{
    public class QuestionSet
    {
        public int id { get; set; }
        public string set_name { get; set; }
        public int question_subject_id { get; set; }
        public bool is_active { get; set; }
        public bool is_deleted { get; set; }
    }

}
