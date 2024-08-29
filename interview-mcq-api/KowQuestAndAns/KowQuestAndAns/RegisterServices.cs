using KowQuestAndAns.DbAccess.DbAccess;
using KowQuestAndAns.Service.HelperService;
using KowQuestAndAns.Service.QuestionService;
using KowQuestAndAns.Service.UserInfoService;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace KowQuestAndAns
{
    public static class RegisterServices
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddTransient<ISqlDataAccess, SqlDataAccess>();
            builder.Services.AddTransient<IUserInfoService, UserInfoService>();
            builder.Services.AddTransient<IQuestionAnsListService, QuestionAnsListService>();
            builder.Services.AddTransient<IQuestionListService, QuestionListService>();
            builder.Services.AddTransient<IExamineeResultService, ExamineeResultService>();
            builder.Services.AddTransient<IQuestionAnsListService, QuestionAnsListService>();
            builder.Services.AddTransient<IQuestionListAndQuestionAnsListService, QuestionListAndQuestionAnsListService>();
            builder.Services.AddTransient<IQuestionListService, QuestionListService>();
            builder.Services.AddTransient<IQuestionSetQuestionListService, QuestionSetQuestionListService>();
            builder.Services.AddTransient<IQuestionSetService, QuestionSetService>();
            builder.Services.AddTransient<IQuestionSubjectQuestionListService, QuestionSubjectQuestionListService>();
            builder.Services.AddTransient<IQuestionSubjectService, QuestionSubjectService>();
            builder.Services.AddTransient<ICandidateExamMasterInfoService, CandidateExamMasterInfoService>();
            builder.Services.AddTransient<IWorkContextService, WorkContextService>();
        }
    }
}
