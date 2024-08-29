using KowQuestAndAns.Data.BaseEntity;
using KowQuestAndAns.DbAccess.DbAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KowQuestAndAns.Service.QuestionService
{
    public class CandidateExamMasterInfoService : ICandidateExamMasterInfoService
    {
        private readonly ISqlDataAccess _db;

        public CandidateExamMasterInfoService(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<CandidateExamMasterInfo> GetCandidateMasterInfoByUserInfoIdAndQuestionSetId(int question_subject_id, int user_info_id)
        {
            var response = new CandidateExamMasterInfo();
            try
            {

                var result = await _db.LoadDataUsingProcedure<CandidateExamMasterInfo, object>("GetCandidateMasterInfoByQuestionSetIdAndUserInfoId", new
                {
                    question_subject_id = question_subject_id,
                    user_info_id= user_info_id

                });
                response = result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                response = new CandidateExamMasterInfo();
            }

            return response;
        }

        public Task<List<CandidateExamMasterInfo>> GetExamineeResult()
        {
            throw new NotImplementedException();
        }

        public Task<CandidateExamMasterInfo> GetQuestionListById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Insert(CandidateExamMasterInfo question)
        {
            var response = 0;
            try
            {
                question.starting_time = DateTime.Now;
                question.ending_time= DateTime.Now;
               var result = await _db.LoadDataUsingProcedure<int, object>("InsertCandidateExamMasterInfo", new
                {
                    question.user_info_id,
                    question.question_subject_id,
                    question.starting_time,
                    question.ending_time,

                });
                response = result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                response = 0;
            }

            return response;
        }

        public async Task<int> Update(CandidateExamMasterInfo question)
        {
            var response = 0;
            try
            {
                question.ending_time = DateTime.Now;
                var result = await _db.LoadDataUsingProcedure<int, object>("UpdateCandidateExamMasterInfo", new
                {
                    question.ending_time,
                    question.user_info_id,
                    question.question_subject_id,
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
