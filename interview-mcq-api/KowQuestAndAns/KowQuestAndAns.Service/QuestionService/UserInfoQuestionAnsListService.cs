using KowQuestAndAns.Data.BaseEntity;
using KowQuestAndAns.DbAccess.DbAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KowQuestAndAns.Service.QuestionService
{
    public class UserInfoQuestionAnsListService : IUserInfoQuestionAnsListService
    {
        private readonly ISqlDataAccess _db;

        public UserInfoQuestionAnsListService(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<int> Insert(UserInfoQuestionAnsList question)
        {
            var response = 0;
            try
            {
                
                var result = await _db.LoadDataUsingProcedure<int, object>("InsertUserQuestionAnswer", new
                {
                    question.user_info_id,
                    question.answer_end_time, 
                    question.answer_start_time,
                    question.question_list_id,
                    question.question_ans_list_id,
                    question.question_set_id,
                    question.is_active,
                    question.is_right,
                    question.is_wrong,
                    question.is_deleted,
                    question.timeout,
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
