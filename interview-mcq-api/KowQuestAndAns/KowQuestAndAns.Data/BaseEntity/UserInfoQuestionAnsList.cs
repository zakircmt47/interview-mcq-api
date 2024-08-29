using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KowQuestAndAns.Data.BaseEntity
{
    public class UserInfoQuestionAnsList
    {
        public int id { get; set; }
        public int user_info_id { get; set; }
        public bool is_right { get; set; }
        public bool is_wrong { get; set; }
        public bool is_active { get; set; }
        public bool is_deleted { get; set; }
        public bool timeout { get; set; }
        public DateTime answer_start_time { get; set; }
        public DateTime answer_end_time { get; set; }
        public int question_list_id { get; set; }
        public int question_set_id { get; set; }
        public int question_ans_list_id { get; set; }
    }
}
