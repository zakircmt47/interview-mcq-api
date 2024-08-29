using KowQuestAndAns.Data.BaseEntity;
using KowQuestAndAns.DbAccess.DbAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KowQuestAndAns.Service.QuestionService
{
    public class QuestionSubjectQuestionListService : IQuestionSubjectQuestionListService
    {
        private readonly ISqlDataAccess _db;

        public QuestionSubjectQuestionListService(ISqlDataAccess db)
        {
            _db = db;
        }
        public Task<List<QuestionSubjectQuestionList>> GetQuestionList(int qeustion_subject_id)
        {
            throw new NotImplementedException();
        }

        public Task<QuestionSubjectQuestionList> GetQuestionListById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Insert(QuestionSubjectQuestionList question)
        {
            var response = 0;
            try
            {
                question.is_active = true;
                var result = await _db.LoadDataUsingProcedure<int, object>("InsertQuestionSubjectQuestionList", new
                {
                    question.question_subject_id,
                    question.is_active,
                    question.is_deleted,
                    question.question_list_id

                });
                response = result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                response = 0;
            }

            return response;
        }

        public Task<int> Update(QuestionSubjectQuestionList question)
        {
            throw new NotImplementedException();
        }
    }
}
