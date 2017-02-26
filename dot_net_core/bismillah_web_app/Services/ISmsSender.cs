using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bismillah_web_app.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
