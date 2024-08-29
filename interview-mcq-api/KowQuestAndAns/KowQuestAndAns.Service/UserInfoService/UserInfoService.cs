using KowQuestAndAns.Data.BaseEntity;
using KowQuestAndAns.DbAccess.DbAccess;
using Mailjet.Client.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KowQuestAndAns.Service.UserInfoService
{
    public class UserInfoService : IUserInfoService
    {
        private readonly ISqlDataAccess _db;

        public UserInfoService(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<UserInfo> GetUserInfoByEmail(string email)
        {
            var response = new UserInfo();
            try
            {
                var result = await _db.LoadDataUsingProcedure<UserInfo, object>("GetUserByEmail", new
                {
                    email = email,
                });
                response = result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                response = new UserInfo();
            }

            return response;
        }

        public async Task<UserInfo> GetUserInfoById(int user_id)
        {
            var response = new UserInfo();
            try
            {
                var result = await _db.LoadDataUsingProcedure<UserInfo, object>("GetUserById", new
                {
                    user_id=user_id,
                });
                response = result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                response = new UserInfo();
            }

            return response;
        }

        public async Task<UserInfo> GetUserInfoByPhone(string phone_no, string email)
        {
            var response = new UserInfo();
            try
            {
                var result = await _db.LoadDataUsingProcedure<UserInfo, object>("GetUserInfoByPhoneNOAndEmail", new
                {
                    phone_no = phone_no,
                    email= email
                });
                response = result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                response = new UserInfo();
            }

            return response;
        }

        public async Task<UserType> GetUserTypeById(int user_info_id)
        {
            var response = new UserType();
            try
            {
                var result = await _db.LoadDataUsingProcedure<UserType, object>("GetUserTypeByUserId", new
                {
                    user_info_id = user_info_id,
                });
                response = result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                response = new UserType();
            }

            return response;
        }

        public async Task<int> Insert(UserInfo user)
        {
            var response = 0;
            try
            {
                user.is_active = true;
                var result = await _db.LoadDataUsingProcedure<int, object>("InsertUserInfo", new
                {
                    user.name,
                    user.phone_no,
                    user.graduation_name,
                    user.univercity_name,
                    user.district_name,
                    user.question_subject_id,
                    user.user_type_id,
                    user.is_active,
                    user.is_deleted,
                    user.password_hash,
                    user.password_salt,
                    user.gender,
                    user.email,

                });
                response = result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                response = 0;
            }

            return response;
        }

        public async Task<int> Update(UserInfo user)
        {
            var response = 0;
            try
            {
                var result = await _db.LoadDataUsingProcedure<int, object>("UpdateUserInfo", new
                {
                    user.name,
                    user.phone_no,
                    user.graduation_name,
                    user.univercity_name,
                    user.district_name,
                    user.question_subject_id,
                    user.user_type_id,
                    user.is_active,
                    user.is_deleted,
                    user.password_hash,
                    user.password_salt,

                });
                response = result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                response = 0;
            }

            return response;
        }
    }
}
