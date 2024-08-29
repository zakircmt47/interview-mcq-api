using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KowQuestAndAns.Data.ReturnViewModel
{
    public class UserResponseViewModel
    {
        public int user_info_id { get; set; }
        public int question_subject_id { get; set; }
        public int question_set_id { get; set; }
        public int exam_time { get; set; }
    }
}
