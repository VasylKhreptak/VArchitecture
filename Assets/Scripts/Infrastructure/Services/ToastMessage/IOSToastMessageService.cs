using Infrastructure.Services.Log.Core;
using Infrastructure.Services.ToastMessage.Core;

namespace Infrastructure.Services.ToastMessage
{
    public class IOSToastMessageService : IToastMessageService
    {
        private readonly ILogService _logService;

        public IOSToastMessageService(ILogService logService)
        {
            _logService = logService;
        }

        public void Send(string message) => _logService.Log($"Toast message: {message}");
    }
}