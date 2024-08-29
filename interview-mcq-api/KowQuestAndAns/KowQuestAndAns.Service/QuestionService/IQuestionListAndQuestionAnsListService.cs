using KowQuestAndAns.Data.BaseEntity;
using KowQuestAndAns.Data.ReturnViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KowQuestAndAns.Service.QuestionService
{
    public interface IQuestionListAndQuestionAnsListService
    {
        Task<int> Insert(QuestionListAndQuestionAnsList question);
        Task<int> Update(QuestionListAndQuestionAnsList question);
        List<QuestionAnsList> GetQuestionAnsListById(int question_list_id);
        Task<List<QuestionListAndQuestionAnsList>> GetQuestionList(int qeustion_subject_id);
    }
}
