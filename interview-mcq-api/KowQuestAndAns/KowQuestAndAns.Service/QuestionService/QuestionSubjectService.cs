using KowQuestAndAns.Data.BaseEntity;
using KowQuestAndAns.DbAccess.DbAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KowQuestAndAns.Service.QuestionService
{
    public class QuestionSubjectService : IQuestionSubjectService
    {
        private readonly ISqlDataAccess _db;

        public QuestionSubjectService(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<List<QuestionSubject>> GetSubjects()
        {
            var response = new List<QuestionSubject>();
            try
            {
                var result = await _db.LoadDataUsingProcedure<QuestionSubject, object>("GetSubjectsList", new
                {

                });
                response = result.ToList();
            }
            catch (Exception ex)
            {
                response = new List<QuestionSubject>();
            }

            return response;
        }

        public async Task<int> Insert(QuestionSubject subject)
        {
            var response = 0;
            try
            {
                subject.is_active = true;
                var result = await _db.LoadDataUsingProcedure<int, object>("InsertQuestionSubject", new
                {
                    subject.subject_name,
                    subject.is_active,
                    subject.is_deleted,
                });
                response = result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                response = 0;
            }

            return response;
        }

        public Task<int> Update(QuestionSubject subject)
        {
            throw new NotImplementedException();
        }
    }
}
