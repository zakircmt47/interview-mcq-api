using KowQuestAndAns.Data.BaseEntity;
using KowQuestAndAns.Data.ReturnViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KowQuestAndAns.Service.QuestionService
{
    public interface IQuestionSetService
    {
        Task<int> Insert(QuestionSet question);
        Task<int> Update(QuestionSet question);
        Task<QuestionSet> GetQuestionListById(int id);
        Task<List<QuestionSet>> GetQuestionSetList(int qeustion_subject_id);
    }
}
