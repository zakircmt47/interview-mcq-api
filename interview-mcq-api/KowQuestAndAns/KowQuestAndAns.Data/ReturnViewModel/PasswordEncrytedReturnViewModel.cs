using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KowQuestAndAns.Data.ReturnViewModel
{
    public class PasswordEncrytedReturnViewModel
    {
        public string password_hash { get; set; }
        public string password_salt { get; set; }
    }
}
