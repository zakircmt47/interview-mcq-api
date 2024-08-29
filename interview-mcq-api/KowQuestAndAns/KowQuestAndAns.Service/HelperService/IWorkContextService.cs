using KowQuestAndAns.Data.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KowQuestAndAns.Service.HelperService
{
    public interface IWorkContextService
    {
        Task<bool> MatchPublicKey(string publicKey=null);
        Task<string> GetPublicOrToken(string token);
        Task<List<string>> GetApiToken();
    }
}
