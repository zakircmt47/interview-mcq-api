using KowQuestAndAns.Data.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KowQuestAndAns.Service.QuestionService
{
    public interface IQuestionSubjectService
    {
        Task<List<QuestionSubject>> GetSubjects();
        Task<int> Insert(QuestionSubject subject);
        Task<int> Update(QuestionSubject subject);
    }
}
