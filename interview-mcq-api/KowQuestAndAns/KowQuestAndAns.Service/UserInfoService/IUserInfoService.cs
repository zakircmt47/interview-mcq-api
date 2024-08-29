using KowQuestAndAns.Data.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KowQuestAndAns.Service.UserInfoService
{
    public interface IUserInfoService
    {
        Task<int> Insert(UserInfo user);
        Task<int> Update(UserInfo user);
        Task<UserInfo> GetUserInfoById(int user_id);
        Task<UserInfo> GetUserInfoByPhone(string phone_no,string email);
        Task<UserInfo> GetUserInfoByEmail(string email);
        Task<UserType> GetUserTypeById(int user_info_id);
    }
}
