using KowQuestAndAns.Data.BaseEntity;
using KowQuestAndAns.Data.ReturnViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KowQuestAndAns.Service.QuestionService
{
    public interface IQuestionListService
    {
        Task<int> Insert(QuestionList question);
        Task<int> Update(QuestionList question);
        Task<int> Delete(int question_list_id);
        Task<QuestionList> GetQuestionListById(int id);
        Task<List<QuestionListViewModel>> GetQuestionList(int question_subject_id, int user_info_id);
        Task<List<QuestionListForAdminViewModel>> GetQuestionForAdminList(int question_subject_id, int question_set_id);
    }
}
