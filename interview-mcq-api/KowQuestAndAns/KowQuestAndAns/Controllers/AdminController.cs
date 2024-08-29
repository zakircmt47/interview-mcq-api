using KowQuestAndAns.Core.AppConstant;
using KowQuestAndAns.Data.BaseEntity;
using KowQuestAndAns.Data.RequestViewModel.Question;
using KowQuestAndAns.Data.ReturnViewModel;
using KowQuestAndAns.Service.HelperService;
using KowQuestAndAns.Service.QuestionService;
using KowQuestAndAns.Service.UserInfoService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace KowQuestAndAns.Controllers
{
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IQuestionListService _questionListService;
        private readonly IQuestionAnsListService _questionAnsListService;
        private readonly IUserInfoService _userInfoService;
        private readonly IQuestionListAndQuestionAnsListService _questionandanslist;
        private readonly IQuestionSetQuestionListService _questionSetQuestionListService;
        private readonly IQuestionSubjectQuestionListService _questionSubjectQuestionListService;
        private readonly IExamineeResultService _examineeResultService;
        private readonly IWorkContextService _workContextService;
        private readonly IQuestionSubjectService _questionSubjectService;
        private readonly IQuestionSetService _questionSetService;
        public AdminController(IQuestionListService questionListService,
             IQuestionAnsListService questionAnsListService,
             IUserInfoService userInfoService,
             IQuestionListAndQuestionAnsListService questionandanslist,
             IQuestionSetQuestionListService questionSetQuestionListService,
             IQuestionSubjectQuestionListService questionSubjectQuestionListService,
             IExamineeResultService examineeResultService,
             IWorkContextService workContextService,
             IQuestionSubjectService questionSubjectService,
             IQuestionSetService questionSetService)
        {
            _questionListService = questionListService;
            _questionAnsListService = questionAnsListService;
            _userInfoService = userInfoService;
            _questionandanslist = questionandanslist;
            _questionSetQuestionListService = questionSetQuestionListService;
            _questionSubjectQuestionListService = questionSubjectQuestionListService;
            _examineeResultService = examineeResultService;
            _workContextService = workContextService;
            _questionSubjectService = questionSubjectService;
            _questionSetService = questionSetService;
        }
        [HttpPost("api/save-question")]
        public async Task<IActionResult> SaveQuestions(QuestionCreateRequestViewModel request)
        {
            try
            {
                string authorization_token = Request.Headers["Authorization"].FirstOrDefault();
                var isMatchToken = await _workContextService.MatchPublicKey(authorization_token);
                if (!isMatchToken)
                {
                    return BadRequest();
                }
                var userType = await _userInfoService.GetUserTypeById(request.user_info_id);
                if (userType.name == UserTypeConst.Admin)
                {
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var questionList = new QuestionList
                        {
                            question_name = request.question_name,
                        };
                        var questionResult = await _questionListService.Insert(questionList);
                        if (questionResult == 0)
                        {
                            scope.Dispose();
                            return Ok(StatusCodes.Status400BadRequest);
                        }

                        var splitAnsList = request.question_ans.Split("|||").ToList();
                        foreach (var ans in splitAnsList)
                        {
                            
                            var questionAnsResult = 0;
                            var rightAnsList = request.right_ans.Split("|||").ToList();
                            var rightAnsIsTrue = false;
                            foreach (var rightans in rightAnsList)
                            {
                                if (rightans == ans)
                                {
                                    rightAnsIsTrue = true;
                                    break;
                                }
                                else
                                { 
                                    rightAnsIsTrue= false;
                                }
                            }

                            if (rightAnsIsTrue)
                            {
                                var ansList = new QuestionAnsList
                                {
                                    question_ans = ans,
                                    isRight = true,
                                    question_list_id = questionResult
                                };

                                questionAnsResult = await _questionAnsListService.Insert(ansList);
                                if (questionAnsResult == 0)
                                {
                                    scope.Dispose();
                                    return Ok(StatusCodes.Status400BadRequest);
                                }
                            }
                            else
                            {
                                var ansList = new QuestionAnsList()
                                {
                                    question_ans = ans,
                                    question_list_id = questionResult
                                };
                                questionAnsResult = await _questionAnsListService.Insert(ansList);
                                if (questionAnsResult == 0)
                                {
                                    scope.Dispose();
                                    return Ok(StatusCodes.Status400BadRequest);
                                }
                            }
                            var questionandanslist = new QuestionListAndQuestionAnsList
                            {
                                question_list_id = questionResult,
                                question_ans_list_id = questionAnsResult,
                            };
                            var questionandanslistResutl = await _questionandanslist.Insert(questionandanslist);
                            if (questionandanslistResutl == 0)
                            {
                                scope.Dispose();
                                return Ok(StatusCodes.Status400BadRequest);
                            }


                            var questionsubjectquestionlist = new QuestionSubjectQuestionList
                            {
                                question_subject_id = request.question_subject_id,
                                question_list_id = questionResult,
                            };
                            var questionsubjectquestionlistResult = await _questionSubjectQuestionListService.Insert(questionsubjectquestionlist);
                            if (questionsubjectquestionlistResult == 0)
                            {
                                scope.Dispose();
                                return Ok(StatusCodes.Status400BadRequest);
                            }

                        }

                        var questionsetquestionlist = new QuestionSetQuestionList
                        {
                            question_set_id = request.question_set_id,
                            question_list_id = questionResult,
                        };

                        var questionsetquestionlistResult = await _questionSetQuestionListService.Insert(questionsetquestionlist);
                        if (questionsetquestionlistResult == 0)
                        {
                            scope.Dispose();
                            return Ok(StatusCodes.Status400BadRequest);
                        }
                        scope.Complete();
                    }
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
            return Ok(StatusCodes.Status200OK);
        }

        [HttpPost("api/update-question")]
        public async Task<IActionResult> UpdateQuestions(QuestionUpdateRequestViewModel request)
        {
            try
            {
                string authorization_token = Request.Headers["Authorization"].FirstOrDefault();
                var isMatchToken = await _workContextService.MatchPublicKey(authorization_token);
                if (!isMatchToken)
                {
                    return BadRequest();
                }
                var userType = await _userInfoService.GetUserTypeById(request.user_info_id);
                if (userType.name == UserTypeConst.Admin)
                {
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var questionList = new QuestionList
                        {
                            question_name = request.question_name,
                        };
                        var questionResult = await _questionListService.Insert(questionList);
                        if (questionResult == 0)
                        {
                            scope.Dispose();
                            return Ok(StatusCodes.Status400BadRequest);
                        }

                        var splitAnsList = request.question_ans.Split("|||").ToList();
                        foreach (var ans in splitAnsList)
                        {

                            var questionAnsResult = 0;
                            var rightAnsList = request.right_ans.Split("|||").ToList();
                            var rightAnsIsTrue = false;
                            foreach (var rightans in rightAnsList)
                            {
                                if (rightans == ans)
                                {
                                    rightAnsIsTrue = true;
                                    break;
                                }
                                else
                                {
                                    rightAnsIsTrue = false;
                                }
                            }

                            if (rightAnsIsTrue)
                            {
                                var ansList = new QuestionAnsList
                                {
                                    question_ans = ans,
                                    isRight = true,
                                    question_list_id = questionResult
                                };

                                questionAnsResult = await _questionAnsListService.Insert(ansList);
                                if (questionAnsResult == 0)
                                {
                                    scope.Dispose();
                                    return Ok(StatusCodes.Status400BadRequest);
                                }
                            }
                            else
                            {
                                var ansList = new QuestionAnsList()
                                {
                                    question_ans = ans,
                                    question_list_id = questionResult
                                };
                                questionAnsResult = await _questionAnsListService.Insert(ansList);
                                if (questionAnsResult == 0)
                                {
                                    scope.Dispose();
                                    return Ok(StatusCodes.Status400BadRequest);
                                }
                            }
                            var questionandanslist = new QuestionListAndQuestionAnsList
                            {
                                question_list_id = questionResult,
                                question_ans_list_id = questionAnsResult,
                            };
                            var questionandanslistResutl = await _questionandanslist.Insert(questionandanslist);
                            if (questionandanslistResutl == 0)
                            {
                                scope.Dispose();
                                return Ok(StatusCodes.Status400BadRequest);
                            }


                            var questionsubjectquestionlist = new QuestionSubjectQuestionList
                            {
                                question_subject_id = request.question_subject_id,
                                question_list_id = questionResult,
                            };
                            var questionsubjectquestionlistResult = await _questionSubjectQuestionListService.Insert(questionsubjectquestionlist);
                            if (questionsubjectquestionlistResult == 0)
                            {
                                scope.Dispose();
                                return Ok(StatusCodes.Status400BadRequest);
                            }

                        }

                        var questionsetquestionlist = new QuestionSetQuestionList
                        {
                            question_set_id = request.question_set_id,
                            question_list_id = questionResult,
                        };

                        var questionsetquestionlistResult = await _questionSetQuestionListService.Insert(questionsetquestionlist);
                        if (questionsetquestionlistResult == 0)
                        {
                            scope.Dispose();
                            return Ok(StatusCodes.Status400BadRequest);
                        }
                        scope.Complete();
                    }
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
            return Ok(StatusCodes.Status200OK);
        }
        [HttpGet("api/delete-question")]
        public async Task<IActionResult> DeleteQuestions(int question_list_id,int user_info_id)
        {
            try
            {
                string authorization_token = Request.Headers["Authorization"].FirstOrDefault();
                var isMatchToken = await _workContextService.MatchPublicKey(authorization_token);
                if (!isMatchToken)
                {
                    return BadRequest();
                }
                var userType = await _userInfoService.GetUserTypeById(user_info_id);
                if (userType.name == UserTypeConst.Admin)
                {
                    var questionDeleteResult = await _questionListService.Delete(question_list_id);
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
            return Ok(StatusCodes.Status200OK);
        }
        [HttpGet("api/examinee-result")]
        public async Task<IActionResult> ExamineeResult(int user_info_id)
        {
            try
            {
                var userType = await _userInfoService.GetUserTypeById(user_info_id);
                if (userType.name == UserTypeConst.Admin)
                {

                    var getExamineeResultList = await _examineeResultService.GetExamineeResult();
                    if (getExamineeResultList == null)
                    {
                        return Ok(StatusCodes.Status400BadRequest);
                    }
                    return Ok(getExamineeResultList);

                }
                return Ok(StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                return Ok(StatusCodes.Status400BadRequest);
            }
        }
        [HttpGet("api/examinee-detail-result")]
        public async Task<IActionResult> ExamineeDetailResult(int user_info_id)
        {
            try
            {
                var getExamineeResultList = await _examineeResultService.GetExamDetailByUserId(user_info_id);

                foreach (var examResult in getExamineeResultList)
                {
                    var questionAnsList = _questionandanslist.GetQuestionAnsListById(examResult.id);
                    examResult.answerList = questionAnsList;
                    var givenAnsList = await _questionAnsListService.GeUserGinventQuestionAnsListById(examResult.id,user_info_id);
                    //examResult.given_answer_list= givenAnsList;

                    List<int> givenAns = new List<int>();
                    foreach (var question in givenAnsList)
                    {
                        givenAns.Add(question.id);
                    }
                    examResult.given_answer = givenAns.ToArray();

                    var righAnsLIst = await _questionAnsListService.GeRightQuestionAnsListByQuestionListId(examResult.id);
                   // examResult.right_answer_list= righAnsLIst;

                    List<int> rigntAns = new List<int>();
                    foreach (var question in righAnsLIst)
                    {
                        rigntAns.Add(question.id);
                    }
                   
                    examResult.right_answer = rigntAns.ToArray();
                }

                if (getExamineeResultList == null)
                {
                    return Ok(StatusCodes.Status400BadRequest);
                }
                return Ok(getExamineeResultList);
            }
            catch (Exception ex)
            {
                return Ok(StatusCodes.Status400BadRequest);
            }
        }

        [HttpPost("api/subject-info")]
        public async Task<IActionResult> CreateSubjectInfo(QuestionSubjectRequestViewModel request)
        {
            try
            {
                string authorization_token = Request.Headers["Authorization"].FirstOrDefault();
                var isMatchToken = await _workContextService.MatchPublicKey(authorization_token);
                if (!isMatchToken)
                {
                    return BadRequest();
                }

                var subject = new QuestionSubject
                {
                    subject_name=request.subject_name
                };
                var questionSubjectInsertResult = await _questionSubjectService.Insert(subject);
            }
            catch (Exception ex)
            {
                return Ok(StatusCodes.Status400BadRequest);
            }
            return Ok(StatusCodes.Status200OK); 
        }
        [HttpPost("api/question-set-info")]
        public async Task<IActionResult> CreateQuestionSetInfo(QuestionSetRequestViewModel request)
        {
            try
            {
                string authorization_token = Request.Headers["Authorization"].FirstOrDefault();
                var isMatchToken = await _workContextService.MatchPublicKey(authorization_token);
                if (!isMatchToken)
                {
                    return BadRequest();
                }

                var subject = new QuestionSet
                {
                    set_name = request.set_name,
                    question_subject_id=request.question_subject_id
                };
                var questionSubjectInsertResult = await _questionSetService.Insert(subject);
            }
            catch (Exception ex)
            {
                return Ok(StatusCodes.Status400BadRequest);
            }
            return Ok(StatusCodes.Status200OK);
        }
        [HttpGet("api/questions")]
        public async Task<IActionResult> GetQuestions(int question_subject_id,int user_info_id,int question_set_id)
        {
            try
            {
                if ((question_subject_id == 0 || question_subject_id == null))
                {
                    return Ok(StatusCodes.Status404NotFound);
                }

                string authorization_token = Request.Headers["Authorization"].FirstOrDefault();
                var isMatchToken = await _workContextService.MatchPublicKey(authorization_token);
                if (!isMatchToken)
                {
                    return BadRequest();
                }

                var userType = await _userInfoService.GetUserTypeById(user_info_id);
                if (userType.name == UserTypeConst.Admin)
                {

                    var questions = await _questionListService.GetQuestionForAdminList(question_subject_id, question_set_id);
                    if (questions != null && questions.Any())
                    {
                        foreach (var question in questions)
                        {
                            var ansList = await _questionAnsListService.GetQuestionAnsListByQuestionListId(question.id);
                            question.ansList = ansList.Take(4).ToList();
                            var righAnsLIst = await _questionAnsListService.GeRightQuestionAnsListByQuestionListId(question.id);
                            question.right_answer_list = righAnsLIst;
                        }
                    }
                    return Ok(questions);
                }
                return Ok(StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                return Ok(StatusCodes.Status400BadRequest);
            }
        }
    }
}
