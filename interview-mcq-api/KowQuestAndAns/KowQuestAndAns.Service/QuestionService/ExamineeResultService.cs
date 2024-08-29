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
    public class ExamineeResultService : IExamineeResultService
    {
        private readonly ISqlDataAccess _db;

        public ExamineeResultService(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<List<ExamDetailInfoViewModel>> GetExamDetailByUserId(int user_info_id)
        {
            var response = new List<ExamDetailInfoViewModel>();
            try
            {
                var result = await _db.LoadDataUsingProcedure<ExamDetailInfoViewModel, object>("GetExamineeDetailsResults", new
                {
                    user_info_id= user_info_id,
                });
                response = result.ToList();
            }
            catch (Exception ex)
            {
                response = new List<ExamDetailInfoViewModel>();
            }

            return response;
        }

        public async Task<List<ExamineeResultViewModel>> GetExamineeResult()
        {
            var response = new List<ExamineeResultViewModel>();
            try
            {
                var result = await _db.LoadDataUsingProcedure<ExamineeResultViewModel, object>("GetExamineeResult", new
                {
                    
                });
                response = result.ToList();
            }
            catch (Exception ex)
            {
                response = new List<ExamineeResultViewModel>();
            }

            return response;
        }

        public Task<QuestionSet> GetQuestionListById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetTotalQuestion(int question_subject_id)
        {
            var response = 0;
            try
            {
                var result = await _db.LoadDataUsingProcedure<int, object>("GetQuestionListByQuestionSubjectId", new
                {
                    question_subject_id= question_subject_id,
                });
                response = result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                response = 0;
            }

            return response;
        }

        public Task<int> Insert(QuestionSet question)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(QuestionSet question)
        {
            throw new NotImplementedException();
        }
    }
}
