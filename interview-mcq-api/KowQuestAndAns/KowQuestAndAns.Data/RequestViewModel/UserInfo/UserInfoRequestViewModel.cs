using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KowQuestAndAns.Data.RequestViewModel.UserInfo
{
    public class UserInfoRequestViewModel
    {
        public string? name { get; set; }
        public string phone_no { get; set; }
        public string? graduation_name { get; set; }
        public string? univercity_name { get; set; }
        public string? district_name { get; set; }
        public int question_subject_id { get; set; }
        public int question_set_id { get; set; }
        public int user_type_id { get; set; }
        public bool is_active { get; set; }
        public bool is_deleted { get; set; }
        public string? password { get; set; }
        public string email { get; set; }
        public string? gender { get; set; }
    }
}
