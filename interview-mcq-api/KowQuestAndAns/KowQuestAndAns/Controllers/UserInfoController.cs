using KowQuestAndAns.Data.BaseEntity;
using KowQuestAndAns.Data.RequestViewModel.UserInfo;
using KowQuestAndAns.Data.ReturnViewModel;
using KowQuestAndAns.Service.HelperService;
using KowQuestAndAns.Service.QuestionService;
using KowQuestAndAns.Service.UserInfoService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Transactions;

namespace KowQuestAndAns.Controllers
{
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserInfoService _userInfoService;
        private readonly ICandidateExamMasterInfoService _candidateExamMasterInfoService;
        private readonly IWorkContextService _workContextService;
        public UserInfoController(IConfiguration configuration, IUserInfoService userInfoService,
            ICandidateExamMasterInfoService candidateExamMasterInfoService, IWorkContextService workContextService)
        {
            _configuration= configuration;
            _userInfoService= userInfoService;
            _candidateExamMasterInfoService= candidateExamMasterInfoService;
            _workContextService= workContextService;
        }
        [HttpPost("api/examinee-register")]
        public async Task<IActionResult> Register(UserInfoRequestViewModel request)
        {

            var apiResponse = new UserInfoRequestViewModel();
            try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    // Mail Trim
                    request.email = request.email.Trim();

                    var getUserInfoResult = await _userInfoService.GetUserInfoByPhone(request.phone_no,request.email);

                    if (getUserInfoResult != null && getUserInfoResult.id>0)
                    {

                        var examtime1 = _configuration.GetSection("AppSettings").GetSection("Exam_Time_Set").Value;
                        var returnResponse1 = new UserResponseViewModel
                        {
                            user_info_id = getUserInfoResult.id,
                            question_subject_id = getUserInfoResult.question_subject_id,
                            question_set_id = getUserInfoResult.question_set_id,
                            exam_time = Convert.ToInt32(examtime1),
                        };
                        return Ok(returnResponse1);
                    }

                    byte[] passwordHash1 = null;
                    byte[] passwordSalt1 = null;

                    if (!string.IsNullOrEmpty(request.password))
                    {
                        CreatePasswordHash(request.password, out byte[] passwordHash, out byte[] passwordSalt);
                        passwordHash1= passwordHash;
                        passwordSalt1= passwordSalt;
                    }

                    var passwordh = "";
                    var passwords = "";

                    if (passwordHash1!=null && passwordSalt1!=null)
                    {
                        passwordh = BitConverter.ToString(passwordHash1).Replace("-", "");
                        passwords = BitConverter.ToString(passwordSalt1).Replace("-", "");
                    }
                   
                    var userInfo = new UserInfo
                    { 
                        name= request.name,
                        phone_no= request.phone_no,
                        graduation_name=request.graduation_name,
                        univercity_name=request.univercity_name,
                        district_name=request.district_name,
                        question_subject_id=request.question_subject_id,
                        user_type_id=2,
                        is_active=request.is_active,
                        is_deleted=request.is_deleted,
                        password_hash= passwordh,
                        password_salt= passwords,
                        gender=request.gender,
                        email=request.email,
                    };

                    var userInfoInsertResult = await _userInfoService.Insert(userInfo);
                    if (userInfoInsertResult==0)
                    {
                        scope.Dispose();
                        return Ok(StatusCodes.Status400BadRequest);
                    }

                    scope.Complete();
                    var question_subject_id = request.question_subject_id;
                    var question_set_id = request.question_set_id;
                    var examtime = _configuration.GetSection("AppSettings").GetSection("Exam_Time_Set").Value;
                    var returnResponse = new UserResponseViewModel
                    {
                        user_info_id=userInfoInsertResult,
                        question_subject_id= question_subject_id,
                        question_set_id= question_set_id,
                        exam_time=Convert.ToInt32(examtime),
                    };
                    return Ok(returnResponse);
                }
            }
            catch (Exception ex)
            {
                return Ok(StatusCodes.Status400BadRequest);
            }
        }

        [HttpPost("api/admin-sign-in")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestViewModel request)
        {

            request.email = request.email.Trim();

            var getUserResponse = await _userInfoService.GetUserInfoByEmail(request.email);

            if (getUserResponse == null && getUserResponse.id==0)
            {
                return Ok(StatusCodes.Status400BadRequest);
            }
           

            var passwordHash = Enumerable.Range(0, getUserResponse.password_hash.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(getUserResponse.password_hash.Substring(x, 2), 16))
                             .ToArray();

            var passwordSalt = Enumerable.Range(0, getUserResponse.password_salt.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(getUserResponse.password_salt.Substring(x, 2), 16))
                             .ToArray();

            if (!VerifyPasswordHash(request.password, passwordHash, passwordSalt))
            {
                return Ok(StatusCodes.Status400BadRequest);
            }

            return Ok(getUserResponse);
        }

        [HttpPost("api/insert-candidate-master-info")]
        public async Task<IActionResult> InsertCandidateMasterInfo([FromBody] CreateCandidateExamMasterInfoRequestionViewModel request)
        {
            try
            {
                var checkMasterInfo = await _candidateExamMasterInfoService.GetCandidateMasterInfoByUserInfoIdAndQuestionSetId(request.question_subject_id, request.user_info_id);
                
                if (checkMasterInfo!=null)
                {
                    return Ok(StatusCodes.Status400BadRequest);
                }

                var examMasterInfo = new CandidateExamMasterInfo
                { 
                    question_subject_id= request.question_subject_id,
                    user_info_id= request.user_info_id,
                    starting_time= DateTime.Now,
                };
                var candidateResult = await _candidateExamMasterInfoService.Insert(examMasterInfo);
                if (candidateResult==0)
                {
                    return Ok(StatusCodes.Status400BadRequest);
                }
                return Ok(candidateResult);
            }
            catch (Exception ex) 
            {
                return Ok(StatusCodes.Status400BadRequest);
            }
        }

        [HttpPost("api/update-candidate-master-info")]
        public async Task<IActionResult> UpdateCandidateMasterInfo([FromBody] UpdateCandidateExamMasterInfoRequestionViewModel request)
        {
            try
            {
                var candidate = new CandidateExamMasterInfo 
                {
                    id=request.user_info_id,
                    ending_time=DateTime.Now,
                    question_subject_id=request.question_subject_id,
                };

                var candidateResult = await _candidateExamMasterInfoService.Update(candidate);
                if (candidateResult == 0)
                {
                    return Ok(StatusCodes.Status400BadRequest);
                }
                return Ok(candidateResult);
            }
            catch (Exception ex)
            {
                return Ok(StatusCodes.Status400BadRequest);
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
               
            }
           
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
