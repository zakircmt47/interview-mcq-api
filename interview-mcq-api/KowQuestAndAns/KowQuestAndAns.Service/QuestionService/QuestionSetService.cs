using KowQuestAndAns.Data.BaseEntity;
using KowQuestAndAns.DbAccess.DbAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KowQuestAndAns.Service.QuestionService
{
    public class QuestionSetService : IQuestionSetService
    {
        private readonly ISqlDataAccess _db;

        public QuestionSetService(ISqlDataAccess db)
        {
            _db = db;
        }
        public async Task<List<QuestionSet>> GetQuestionSetList(int question_subject_id)
        {
            var response = new List<QuestionSet>();
            try
            {
                var result = await _db.LoadDataUsingProcedure<QuestionSet, object>("GetQuestionSetList", new
                {
                    question_subject_id = question_subject_id
                });
                response = result.ToList();
            }
            catch (Exception ex)
            {
                response = new List<QuestionSet>();
            }

            return response;
        }

        public Task<QuestionSet> GetQuestionListById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Insert(QuestionSet question)
        {
            var response = 0;
            try
            {
                question.is_active = true;
                var result = await _db.LoadDataUsingProcedure<int, object>("InsertQuestionSet", new
                {
                    question.set_name,
                    question.is_active,
                    question.is_deleted,
                    question.question_subject_id

                });
                response = result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                response = 0;
            }

            return response;
        }

        public Task<int> Update(QuestionSet question)
        {
            throw new NotImplementedException();
        }
    }
}
