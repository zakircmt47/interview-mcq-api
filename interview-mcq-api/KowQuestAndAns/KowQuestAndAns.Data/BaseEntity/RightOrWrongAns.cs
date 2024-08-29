using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KowQuestAndAns.Data.BaseEntity
{
    public class RightOrWrongAns
    {
        public int id { get; set; }
        public int user_info_id { get; set; }
        public bool is_right { get; set; }
        public bool is_worng { get; set; }
        public bool is_active { get; set; }
        public bool is_deleted { get; set; }
        public DateTime answer_start_time { get; set; }
        public DateTime answer_end_time { get; set; }
        public int user_info_question_ans_list_id { get; set; }
    }

}
