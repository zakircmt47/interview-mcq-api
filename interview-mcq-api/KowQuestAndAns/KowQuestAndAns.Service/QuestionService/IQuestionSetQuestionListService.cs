using KowQuestAndAns.Data.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KowQuestAndAns.Service.QuestionService
{
    public interface IQuestionSetQuestionListService
    {
        Task<int> Insert(QuestionSetQuestionList question);
        Task<int> Update(QuestionSetQuestionList question);
        Task<QuestionSetQuestionList> GetQuestionListById(int id);
        Task<List<QuestionSetQuestionList>> GetQuestionList(int qeustion_subject_id);
    }
}
