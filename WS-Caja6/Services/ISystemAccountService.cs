using WS_Caja6.Models.Request;
using WS_Caja6.Models.Response;

namespace WS_Caja6.Services
{
    public interface ISystemAccountService
    {
        public SystemAccountResponse Auth(AuthRequest model);
    }
}
