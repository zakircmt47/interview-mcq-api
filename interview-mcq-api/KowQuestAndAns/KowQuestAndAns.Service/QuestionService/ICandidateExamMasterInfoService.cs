using KowQuestAndAns.Data.BaseEntity;
using KowQuestAndAns.Data.ReturnViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KowQuestAndAns.Service.QuestionService
{
    public interface ICandidateExamMasterInfoService
    {
        Task<int> Insert(CandidateExamMasterInfo question);
        Task<int> Update(CandidateExamMasterInfo question);
        Task<CandidateExamMasterInfo> GetQuestionListById(int id);
        Task<CandidateExamMasterInfo> GetCandidateMasterInfoByUserInfoIdAndQuestionSetId(int question_set_id, int user_info_id);
        Task<List<CandidateExamMasterInfo>> GetExamineeResult();
    }
}

