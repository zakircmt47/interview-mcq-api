using KowQuestAndAns.Data.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KowQuestAndAns.Data.ReturnViewModel
{
    public class QuestionListViewModel
    {
        public int id { get; set; }
        public string question_name { get; set; }
        public bool is_active { get; set; }
        public bool is_deleted { get; set; }
        public List<AnsListViewModel> ansList { get; set; }
        public string question_set_name { get; set; }
    }
    public class QuestionListForAdminViewModel
    {
        public int id { get; set; }
        public string question_name { get; set; }
        public bool is_active { get; set; }
        public bool is_deleted { get; set; }
        public List<AnsListViewModel> ansList { get; set; }
        public List<QuestionAnsList> right_answer_list { get; set; }

    }
}
