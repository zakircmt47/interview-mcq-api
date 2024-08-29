using KowQuestAndAns.Core.AppConstant;
using KowQuestAndAns.Core.Enum;
using KowQuestAndAns.Data.BaseEntity;
using KowQuestAndAns.Data.RequestViewModel.Question;
using KowQuestAndAns.Data.ReturnViewModel;
using KowQuestAndAns.Service.QuestionService;
using KowQuestAndAns.Service.UserInfoService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KowQuestAndAns.Controllers
{
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionListService _questionListService;
        private readonly IQuestionAnsListService _questionAnsListService;
        private readonly IUserInfoService _userInfoService;
        private readonly IQuestionSubjectService _questionSubjectService;
        private readonly IQuestionSetService _questionSetService;
        private readonly IExamineeResultService _examineeResultService;
        public QuestionsController(IQuestionListService questionListService,
             IQuestionAnsListService questionAnsListService,
             IUserInfoService userInfoService,
             IQuestionSubjectService questionSubjectService,
             IQuestionSetService questionSetService,
             IExamineeResultService examineeResultService)
        {
            _questionListService = questionListService;
            _questionAnsListService = questionAnsListService;
            _userInfoService = userInfoService;
            _questionSubjectService = questionSubjectService;
            _questionSetService = questionSetService;
            _examineeResultService = examineeResultService;
        }

        [HttpGet("api/examinee-questions")]
        public async Task<IActionResult> GetQuestions(int question_subject_id,int user_info_id)
        {
            try
            {
                if ((question_subject_id == 0 || question_subject_id == null))
                {
                    return Ok(StatusCodes.Status404NotFound);
                }


                var questions = await _questionListService.GetQuestionList(question_subject_id, user_info_id);
                if (questions != null && questions.Any())
                {
                    foreach (var question in questions)
                    {
                        var ansList = await _questionAnsListService.GetQuestionAnsListByQuestionListId(question.id);
                        question.ansList = ansList.Take(4).ToList();
                    }
                }

                return Ok(questions);
            }
            catch (Exception ex)
            {
                return Ok(StatusCodes.Status400BadRequest);
            }
        }
       
        [HttpPost("api/check-answer")]
        public async Task<IActionResult> CheckAns(CheckAnswerViewModel request)
        {
            try
            {
                var insertResult = 0;
                if (request.qeustion_list_id == 0 || request.user_info_id == 0)
                {
                    return Ok(StatusCodes.Status404NotFound);
                }

                var questionListIdsSplit = request.question_ans_list_id.Split("|||");
                var result = false;
                var rightAnsList = await _questionAnsListService.GetQuestionAnsListByQuestionId(request.qeustion_list_id);
                var DbrightAnsCount = rightAnsList.Count;
                var CheckRightAnsCount = 0;
                var givenAnsRightCount = 0;
                var wrongAnsCount = 0;
                foreach (var ans in questionListIdsSplit)
                {
                    foreach (var answer in rightAnsList)
                    {
                        if (answer.id == Convert.ToInt32(ans))
                        {
                            CheckRightAnsCount=CheckRightAnsCount+ 1;
                        }
                        else
                        {
                            result= false;
                        }
                    }
                    givenAnsRightCount++;
                }

                if (CheckRightAnsCount == DbrightAnsCount && givenAnsRightCount == CheckRightAnsCount)
                {
                    result = true;
                }

                if (result)
                {
                    var is_right = true;
                    var is_wrong = false;
                    foreach (var question_ans_list_id in questionListIdsSplit)
                    {
                       var questionsUser = await _questionAnsListService.CheckQuestionAnsForUser(request.qeustion_list_id, Convert.ToInt32(question_ans_list_id), request.user_info_id, request.timeout, request.question_set_id,is_right,is_wrong);
                    }

                    var questions = await _questionAnsListService.CheckQuestionAns(request.qeustion_list_id,request.user_info_id, request.timeout, request.question_set_id, is_right, is_wrong);
                    insertResult = questions;
                }
                else
                {
                    
                    var is_right = false;
                    var is_wrong = true;
                    foreach (var question_ans_list_id in questionListIdsSplit)
                    {
                        var questionsUser = await _questionAnsListService.CheckQuestionAnsForUser(request.qeustion_list_id, Convert.ToInt32(question_ans_list_id), request.user_info_id, request.timeout, request.question_set_id, is_right, is_wrong);
                    }

                    var questions = await _questionAnsListService.CheckQuestionAns(request.qeustion_list_id,request.user_info_id, request.timeout, request.question_set_id, is_right, is_wrong);
                    insertResult = questions;
                }
                if (insertResult > 0)
                {
                    return Ok(StatusCodes.Status200OK);
                }
                else
                {
                    return Ok(StatusCodes.Status400BadRequest);
                }
            }
            catch (Exception ex)
            {
                return Ok(StatusCodes.Status400BadRequest);
            }
        }

        [HttpGet("api/subjects")]
        public async Task<IActionResult> GetSubjects()
        {
            try
            {

                var subjects = await _questionSubjectService.GetSubjects();
                if (subjects == null && subjects.Count==0)
                {
                    return Ok(StatusCodes.Status400BadRequest);
                }

                return Ok(subjects);
            }
            catch (Exception ex)
            {
                return Ok(StatusCodes.Status400BadRequest);
            }
        }

        [HttpGet("api/question-sets")]
        public async Task<IActionResult> GetQuestionSets(int quesiton_subject_id)
        {
            try
            {

                var questionSets = await _questionSetService.GetQuestionSetList(quesiton_subject_id);
                if (questionSets == null && questionSets.Count == 0)
                {
                    return Ok(StatusCodes.Status400BadRequest);
                }

                return Ok(questionSets);
            }
            catch (Exception ex)
            {
                return Ok(StatusCodes.Status400BadRequest);
            }
        }
        [HttpGet("api/examinee-justin-result")]
        public async Task<IActionResult> ExamineeDetailResult(int user_info_id,int question_subject_id)
        {
            try
            {
                var getExamineeResultList = await _examineeResultService.GetExamineeResult();
                //var exam

                var userExamResult=getExamineeResultList.FirstOrDefault(x=>x.id==user_info_id);

                if (userExamResult == null) 
                {
                    return BadRequest();
                }
                var totalQuestion = await _examineeResultService.GetTotalQuestion(question_subject_id);
                var examResult = new ExamDetailInfoViewModel
                {
                    total_questions=totalQuestion,
                    total_right_ans= userExamResult.total_right_ans
                };
                
                return Ok(examResult);
            }
            catch (Exception ex)
            {
                return Ok(StatusCodes.Status400BadRequest);
            }
        }
    }
}
