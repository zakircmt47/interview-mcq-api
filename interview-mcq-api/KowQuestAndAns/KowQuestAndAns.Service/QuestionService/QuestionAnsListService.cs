using KowQuestAndAns.Data.BaseEntity;
using KowQuestAndAns.Data.ReturnViewModel;
using KowQuestAndAns.DbAccess.DbAccess;
using Mailjet.Client.Resources;
using Nancy.Bootstrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KowQuestAndAns.Service.QuestionService
{
    public class QuestionAnsListService : IQuestionAnsListService
    {
        private readonly ISqlDataAccess _db;

        public QuestionAnsListService(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<int> CheckQuestionAns(int question_list_id, int user_info_id,bool timeout,int question_set_id, bool is_right, bool is_wrong)
        {
            var response = 0;
            try
            {
                var result = await _db.LoadDataUsingProcedure<int,object>("CheckRightQuestionAns", new
                {
                    question_list_id = question_list_id,
                    user_info_id= user_info_id,
                    timeout=timeout,
                    answer_start_time = DateTime.Now,
                    question_set_id= question_set_id,
                    is_right = is_right,
                    is_wrong= is_wrong,
                    
                });
                response = result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                response = 0;
            }

            return response;
        }
        public async Task<int> CheckQuestionAnsForUser(int question_list_id, int question_ans_list_id, int user_info_id,bool timeout,int question_set_id, bool is_right, bool is_wrong)
                {
                    var response = 0;
                    try
                    {
                        var result = await _db.LoadDataUsingProcedure<int,object>("CheckRightQuestionAnsUserInfoQuestionAnsList", new
                        {
                            question_list_id = question_list_id,
                            question_ans_list_id= question_ans_list_id,
                            user_info_id= user_info_id,
                            timeout=timeout,
                            answer_start_time = DateTime.Now,
                            question_set_id= question_set_id,
                            is_right = is_right,
                            is_wrong= is_wrong,
                    
                        });
                        response = result.FirstOrDefault();
                    }
                    catch (Exception ex)
                    {
                        response = 0;
                    }

                    return response;
                }

        public async Task<QuestionAnsList> GetQuestionAnsListById(int id)
        {
            var response = new QuestionAnsList();
            try
            {
                var result = await _db.LoadDataUsingProcedure<QuestionAnsList, object>("GetUserById", new
                {
                    id = id,
                });
                response = result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                response = new QuestionAnsList();
            }

            return response;
        }
        public async Task<List<QuestionAnsList>> GeUserGinventQuestionAnsListById(int question_list_id,int user_info_id)
        {
            var response = new List<QuestionAnsList>();
            try
            {
                var result = await _db.LoadDataUsingProcedure<QuestionAnsList, object>("GetExamineeGivenAnsListByQuestionListId", new
                {
                    question_list_id = question_list_id,
                    user_info_id= user_info_id
                });
                response = result;
            }
            catch (Exception ex)
            {
                response = new List<QuestionAnsList>();
            }

            return response;
        }

        public async Task<List<QuestionAnsList>> GetQuestionAnsListByQuestionId(int question_list_id)
        {

            var response = new List<QuestionAnsList>();
            try
            {
                var result = await _db.LoadDataUsingProcedure<QuestionAnsList, object>("GetAllRightAnswersListByQuestionListId", new
                {
                    question_list_id = question_list_id,
                });
                response = result.ToList();
            }
            catch (Exception ex)
            {
                response = new List<QuestionAnsList>();
            }

            return response;
        }

        public async Task<List<AnsListViewModel>> GetQuestionAnsListByQuestionListId(int qeustion_list_id)
        {
            var response = new List<AnsListViewModel>();
            try
            {
                var result = await _db.LoadDataUsingProcedure<AnsListViewModel, object>("GetExamineeQuestionsAnsList", new
                {
                    qeustion_list_id = qeustion_list_id,
                });
                response = result.ToList();
            }
            catch (Exception ex)
            {
                response = new List<AnsListViewModel>();
            }

            return response;
        }

        public async Task<int> Insert(QuestionAnsList question)
        {
            var response = 0;
            try
            {
                question.is_active= true;
                var result = await _db.LoadDataUsingProcedure<int, object>("InsertQuestionAnsList", new
                {
                    question.question_ans,
                    question.is_deleted,
                    question.is_active,
                    question.isRight,
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

        public async Task<int> Update(QuestionAnsList question)
        {
            var response = 0;
            try
            {
                var result = await _db.LoadDataUsingProcedure<int, object>("UpdateQuestionAnsList", new
                {
                    question.question_ans,
                    question.is_deleted,
                    question.is_active,
                    question.isRight,

                });
                response = result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                response = 0;
            }

            return response;
        }

        public async Task<List<QuestionAnsList>> GeRightQuestionAnsListByQuestionListId(int question_list_id)
        {
            var response = new List<QuestionAnsList>();
            try
            {
                var result = await _db.LoadDataUsingProcedure<QuestionAnsList, object>("GetQuestionAnsListByQuestionListId", new
                {
                    question_list_id = question_list_id,
                });
                response = result;
            }
            catch (Exception ex)
            {
                response = new List<QuestionAnsList>();
            }

            return response;
        }
    }
}
