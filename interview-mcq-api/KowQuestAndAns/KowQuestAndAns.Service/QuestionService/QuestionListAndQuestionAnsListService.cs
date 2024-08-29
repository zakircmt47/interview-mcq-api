using KowQuestAndAns.Data.BaseEntity;
using KowQuestAndAns.Data.ReturnViewModel;
using KowQuestAndAns.DbAccess.DbAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KowQuestAndAns.Service.QuestionService
{
    public class QuestionListAndQuestionAnsListService : IQuestionListAndQuestionAnsListService
    {
        private readonly ISqlDataAccess _db;

        public QuestionListAndQuestionAnsListService(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<List<QuestionListAndQuestionAnsList>> GetQuestionList(int qeustion_subject_id)
        {
            throw new NotImplementedException();
        }

        public List<QuestionAnsList> GetQuestionAnsListById(int question_list_id)
        {
            var response = new List<QuestionAnsList>();
            try
            {
                var result = _db.LoadDataUsingProcedure<QuestionAnsList, object>("GetQuestionAnsListByQuestionId", new
                {
                    question_list_id= question_list_id,
                });
                response = result.Result;
            }
            catch (Exception ex)
            {
                response = new List<QuestionAnsList>();
            }

            return response;
        }

        public async Task<int> Insert(QuestionListAndQuestionAnsList question)
        {
            var response = 0;
            try
            {
                question.is_active = true;
                var result = await _db.LoadDataUsingProcedure<int, object>("InsertQuestionListAndQuestionAnsList", new
                {
                    question.question_ans_list_id,
                    question.is_deleted,
                    question.is_active,
                    question.question_list_id,

                });
                response = result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                response = 0;
            }

            return response;
        }

        public Task<int> Update(QuestionListAndQuestionAnsList question)
        {
            throw new NotImplementedException();
        }
    }
}
