using KowQuestAndAns.Data.BaseEntity;
using KowQuestAndAns.Data.ReturnViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KowQuestAndAns.Service.QuestionService
{
    public interface IQuestionAnsListService
    {
        Task<int> Insert(QuestionAnsList question);
        Task<int> Update(QuestionAnsList question);
        Task<QuestionAnsList> GetQuestionAnsListById(int id);
        Task<List<QuestionAnsList>> GetQuestionAnsListByQuestionId(int question_list_id);
        Task<List<AnsListViewModel>> GetQuestionAnsListByQuestionListId(int qeustion_list_id);
        Task<int> CheckQuestionAns(int question_list_id,int user_info_id, bool timeout,int question_set_id, bool is_right, bool is_wrong);
        Task<List<QuestionAnsList>> GeUserGinventQuestionAnsListById(int question_list_id,int user_info_id);
        Task<List<QuestionAnsList>> GeRightQuestionAnsListByQuestionListId(int question_list_id);
        Task<int> CheckQuestionAnsForUser(int question_list_id, int question_ans_list_id, int user_info_id, bool timeout, int question_set_id, bool is_right, bool is_wrong);

    }
}
