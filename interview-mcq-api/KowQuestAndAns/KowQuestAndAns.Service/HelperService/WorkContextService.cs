using KowQuestAndAns.Data.BaseEntity;
using KowQuestAndAns.DbAccess.DbAccess;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KowQuestAndAns.Service.HelperService
{
    public class WorkContextService : IWorkContextService
    {
        private readonly IConfiguration _configuration;
        private readonly ISqlDataAccess _db;

        public WorkContextService(IConfiguration configuration, ISqlDataAccess db)
        {
            _configuration = configuration;
            _db = db;

        }

        public async Task<List<string>> GetApiToken()
        {
            var response = new List<string>();
            try
            {
                var result = await _db.LoadDataUsingProcedure<string, object>("GetActiveApiKeys", new
                {

                });
                response = result.ToList();
            }
            catch (Exception ex)
            {
                response = new List<string>();
            }

            return response;
        }

        public async Task<string> GetPublicOrToken(string token)
        {
            var tokenOrPublicKey = "";
            tokenOrPublicKey = token.Replace("bearer ", "");
            if (tokenOrPublicKey == "bearer" || tokenOrPublicKey == "")
            {
                tokenOrPublicKey = null;
            }
            return tokenOrPublicKey;
        }

        public async Task<bool> MatchPublicKey(string publicKey)
        {
            var result = false;
            try
            {
                var tokenOrPublicKey = publicKey.Replace("bearer ", "");
                if (tokenOrPublicKey == "bearer" || tokenOrPublicKey == "")
                {
                    return false;
                }

                var publicApiKey = await GetApiToken();

                foreach (var apikey in publicApiKey)
                {

                    // Remove any non-alphanumeric characters and convert to lowercase
                    string cleanedInput1 = new string(apikey.Where(char.IsLetterOrDigit).Select(char.ToLower).ToArray());
                    string cleanedInput2 = new string(tokenOrPublicKey.Where(char.IsLetterOrDigit).Select(char.ToLower).ToArray());
                    if (cleanedInput1 == cleanedInput2)
                    {
                        result = true;
                    }
                }

            }
            catch (Exception ex)
            {
                result = false;
                return result;
            }
            return result;
        }

    }
}
