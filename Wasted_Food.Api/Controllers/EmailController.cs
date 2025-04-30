using Microsoft.AspNetCore.Mvc;
using Wasted_Food.Api.Base;
using Wasted_Food.Core.Feautres.Email.Commands.Models;
using Wasted_Food.Data.AppMetaData;

namespace Securiy_Authentication.Controllers
{
    [ApiController]
    public class EmailController : AppBaseController
    {
        /// <summary>
        /// إرسال بريد إلكتروني إلى المستخدم
        /// </summary>
        /// <param name="command">يحتوي على بيانات البريد الإلكتروني (المستلم، الموضوع، المحتوى)</param>
        /// <returns>حالة عملية الإرسال (نجاح/فشل)</returns>
        /// <summary>
        /// Send email to user
        /// </summary>
        /// <param name="command">Contains email data (recipient, subject, content)</param>
        /// <returns>Email sending status (success/failure)</returns>
        //[Authorize(Roles = "Admin")]
        [HttpPost(Router.Email.SendEmail)]
        public async Task<IActionResult> SendEmail([FromQuery] SendEmailCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
    }
}