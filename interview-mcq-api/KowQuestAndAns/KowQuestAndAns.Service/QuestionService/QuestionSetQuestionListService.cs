using KowQuestAndAns.Data.BaseEntity;
using KowQuestAndAns.DbAccess.DbAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KowQuestAndAns.Service.QuestionService
{
    public class QuestionSetQuestionListService : IQuestionSetQuestionListService
    {
        private readonly ISqlDataAccess _db;

        public QuestionSetQuestionListService(ISqlDataAccess db)
        {
            _db = db;
        }
        public Task<List<QuestionSetQuestionList>> GetQuestionList(int qeustion_subject_id)
        {
            throw new NotImplementedException();
        }

        public Task<QuestionSetQuestionList> GetQuestionListById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Insert(QuestionSetQuestionList question)
        {
            var response = 0;
            try
            {
                question.is_active = true;
                var result = await _db.LoadDataUsingProcedure<int, object>("InsertQuestionSetQuestionList", new
                {
                    question.question_list_id,
                    question.is_deleted,
                    question.is_active,
                    question.question_set_id,

                });
                response = result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                response = 0;
            }

            return response;
        }

        public Task<int> Update(QuestionSetQuestionList question)
        {
            throw new NotImplementedException();
        }
    }
}
