using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KowQuestAndAns.Data.BaseEntity
{
    public class CandidateExamMasterInfo
    {
        public int id { get; set; }
        public int question_subject_id { get; set; }
        public DateTime starting_time { get; set; } = DateTime.Now;
        public DateTime ending_time { get; set; }= DateTime.Now;
        public int user_info_id { get; set; }
    }
    public class UpdateCandidateExamMasterInfoRequestionViewModel
    {
        public int user_info_id { get; set; }
        public int question_subject_id { get; set; }
    } 
    public class CreateCandidateExamMasterInfoRequestionViewModel
    {
        public int question_subject_id { get; set; }
        public int user_info_id { get; set; }
    }
}
