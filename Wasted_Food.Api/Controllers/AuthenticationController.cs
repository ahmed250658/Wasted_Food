using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wasted_Food.Api.Base;
using Wasted_Food.Core.Feautres.Authentication.Commands.Models;
using Wasted_Food.Core.Feautres.Authentication.Querys.Models;
using Wasted_Food.Data.AppMetaData;

namespace Securiy_Authentication.Controllers
{
    [ApiController]
    public class AuthenticationController : AppBaseController
    {
        /// <summary>
        /// تسجيل دخول المستخدم
        /// </summary>
        /// <param name="command">بيانات تسجيل الدخول (البريد الإلكتروني/اسم المستخدم وكلمة المرور)</param>
        /// <returns>رمز الدخول ورمز التحديث</returns>
        [HttpPost(Router.Authentication.SignIn)]
        public async Task<IActionResult> SignIn([FromForm] SignInCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        /// <summary>
        /// تجديد رمز الدخول باستخدام رمز التحديث
        /// </summary>
        /// <param name="command">يحتوي على رمز التحديث القديم</param>
        /// <returns>رمز دخول جديد ورمز تحديث جديد</returns>
        [HttpPost(Router.Authentication.RefreshToken)]
        public async Task<IActionResult> RefreshToken([FromForm] RefreshTokenCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        /// <summary>
        /// التحقق من صلاحية رمز الدخول
        /// </summary>
        /// <param name="query">يحتوي على رمز الدخول المطلوب التحقق منه</param>
        /// <returns>حالة الرمز (صالح/منتهي الصلاحية)</returns>
        [HttpGet(Router.Authentication.VaildateToken)]
        public async Task<IActionResult> VaildateToken([FromQuery] AuthorizeUserQuery query)
        {
            var response = await Mediator.Send(query);
            return NewResult(response);
        }

        /// <summary>
        /// تأكيد البريد الإلكتروني للمستخدم (للمشرفين فقط)
        /// </summary>
        /// <param name="query">يحتوي على معرف المستخدم ورمز التأكيد</param>
        /// <returns>حالة عملية التأكيد</returns>
        [Authorize(Roles = "Admin")]
        [HttpGet(Router.Authentication.ConfirmEmail)]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailQuery query)
        {
            var response = await Mediator.Send(query);
            return NewResult(response);
        }

        /// <summary>
        /// إرسال رابط إعادة تعيين كلمة المرور (للمؤسسات والجمعيات الخيرية)
        /// </summary>
        /// <param name="Command">يحتوي على البريد الإلكتروني للمستخدم</param>
        /// <returns>حالة عملية الإرسال</returns>
        [Authorize(Roles = "Admin,PublicInstitution,CharityOrganization")]
        [HttpPost(Router.Authentication.sendResetPassword)]
        public async Task<IActionResult> SendResetPassword([FromQuery] SendResetPasswordCommand Command)
        {
            var response = await Mediator.Send(Command);
            return NewResult(response);
        }

        /// <summary>
        /// التحقق من صلاحية رابط إعادة تعيين كلمة المرور (للمؤسسات والجمعيات الخيرية)
        /// </summary>
        /// <param name="query">يحتوي على رمز إعادة التعيين</param>
        /// <returns>حالة الرمز (صالح/غير صالح)</returns>
        [Authorize(Roles = "Admin,PublicInstitution,CharityOrganization")]
        [HttpGet(Router.Authentication.ConfirmResetPassword)]
        public async Task<IActionResult> ResetPassword([FromQuery] ConfirmResetPasswordQuery query)
        {
            var response = await Mediator.Send(query);
            return NewResult(response);
        }

        /// <summary>
        /// إعادة تعيين كلمة المرور (للمؤسسات والجمعيات الخيرية)
        /// </summary>
        /// <param name="Command">يحتوي على رمز إعادة التعيين وكلمة المرور الجديدة</param>
        /// <returns>حالة عملية إعادة التعيين</returns>
        [Authorize(Roles = "Admin,PublicInstitution,CharityOrganization")]
        [HttpPost(Router.Authentication.ResetPassword)]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordCommand Command)
        {
            var response = await Mediator.Send(Command);
            return NewResult(response);
        }
    }
}