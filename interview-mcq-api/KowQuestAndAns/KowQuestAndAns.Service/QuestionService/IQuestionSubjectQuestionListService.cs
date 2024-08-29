using KowQuestAndAns.Data.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KowQuestAndAns.Service.QuestionService
{
    public interface IQuestionSubjectQuestionListService
    {
        Task<int> Insert(QuestionSubjectQuestionList question);
        Task<int> Update(QuestionSubjectQuestionList question);
        Task<QuestionSubjectQuestionList> GetQuestionListById(int id);
        Task<List<QuestionSubjectQuestionList>> GetQuestionList(int qeustion_subject_id);
    }
}
