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
    public class QuestionListService : IQuestionListService
    {
        private readonly ISqlDataAccess _db;

        public QuestionListService(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<int> Delete(int question_list_id)
        {
            var response = 0;
            try
            {
                var result = await _db.LoadDataUsingProcedure<int, object>("UpdateQuestionListIsDeleted", new
                {
                    question_list_id= question_list_id,

                });
                response = result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                response = 0;
            }

            return response;
        }

        public async Task<List<QuestionListViewModel>> GetQuestionList(int question_subject_id, int user_info_id)
        {
            var response = new List<QuestionListViewModel>();
            try
            {
                var result = await _db.LoadDataUsingProcedure<QuestionListViewModel, object>("GetExamineeQuestions", new
                {
                    question_subject_id = question_subject_id,
                    user_info_id= user_info_id
                });
                response = result.ToList();
            }
            catch (Exception ex)
            {
                response = new List<QuestionListViewModel>();
            }

            return response;
        }
        public async Task<List<QuestionListForAdminViewModel>> GetQuestionForAdminList(int question_subject_id, int question_set_id)
        {
            var response = new List<QuestionListForAdminViewModel>();
            try
            {
                var result = await _db.LoadDataUsingProcedure<QuestionListForAdminViewModel, object>("GetExamineeQuestionsForAdmin", new
                {
                    question_subject_id = question_subject_id,
                    question_set_id= question_set_id
                });
                response = result.ToList();
            }
            catch (Exception ex)
            {
                response = new List<QuestionListForAdminViewModel>();
            }

            return response;
        }

        public async Task<QuestionList> GetQuestionListById(int id)
        {
            var response = new QuestionList();
            try
            {
                var result = await _db.LoadDataUsingProcedure<QuestionList, object>("GetQuestionListById", new
                {
                    id = id,
                });
                response = result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                response = new QuestionList();
            }

            return response;
        }

        public async Task<int> Insert(QuestionList question)
        {
            var response = 0;
            try
            {
                question.is_active= true;
                var result = await _db.LoadDataUsingProcedure<int, object>("InsertQuestionList", new
                {
                   question.question_name,
                   question.is_active,
                   question.is_deleted,

                });
                response = result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                response = 0;
            }

            return response;
        }

        public async Task<int> Update(QuestionList question)
        {
            var response = 0;
            try
            {
                var result = await _db.LoadDataUsingProcedure<int, object>("UpdateQuestionList", new
                {
                    question.question_name,
                    question.is_active,
                    question.is_deleted,

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
