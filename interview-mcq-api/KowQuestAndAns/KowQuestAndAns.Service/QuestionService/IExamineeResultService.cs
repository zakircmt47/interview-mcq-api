using KowQuestAndAns.Data.BaseEntity;
using KowQuestAndAns.Data.ReturnViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KowQuestAndAns.Service.QuestionService
{
    public interface IExamineeResultService
    {
        Task<int> Insert(QuestionSet question);
        Task<int> Update(QuestionSet question);
        Task<QuestionSet> GetQuestionListById(int id);
        Task<List<ExamDetailInfoViewModel>> GetExamDetailByUserId(int user_info_id);
        Task<List<ExamineeResultViewModel>> GetExamineeResult();
        Task<int> GetTotalQuestion(int question_subject_id);
    }
}
