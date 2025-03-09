using Infrastructure.Services.Log.Core;
using Infrastructure.Services.ToastMessage.Core;

namespace Infrastructure.Services.ToastMessage
{
    public class DefaultToastMessageService : IToastMessageService
    {
        private readonly ILogService _logService;

        public DefaultToastMessageService(ILogService logService)
        {
            _logService = logService;
        }

        public void Send(string message) => _logService.Log($"Toast message: {message}");
    }
}