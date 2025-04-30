using Microsoft.AspNetCore.Mvc;
using Wasted_Food.Api.Base;
using Wasted_Food.Core.Feautres.User.Commands.Models;
using Wasted_Food.Data.AppMetaData;

namespace Securiy_Authentication.Controllers
{
    [ApiController]
    public class UserController : AppBaseController
    {
        /// <summary>
        /// إنشاء مستخدم جديد
        /// Creates a new user
        /// </summary>
        /// <param name="command">بيانات المستخدم الجديد / New user data</param>
        /// <returns>نتيجة العملية / Operation result</returns>
        [HttpPost(Router.AppUserRouting.Create)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        //[HttpGet(Router.AppUserRouting.Paginated)]
        //public async Task<IActionResult> GetUserList([FromQuery] GetUserListQuery command)
        //{
        //    var response = await Mediator.Send(command);
        //    return Ok(response);
        //}

        //[HttpGet(Router.AppUserRouting.GetById)]
        //public async Task<IActionResult> GetUserByID([FromRoute] int id)
        //{
        //    var response = await Mediator.Send(new GetUserByIdQuery(id));
        //    return NewResult(response);
        //}

        /// <summary>
        /// تعديل بيانات المستخدم
        /// Edit user information
        /// </summary>
        /// <param name="command">بيانات التعديل / Edit data</param>
        /// <returns>نتيجة العملية / Operation result</returns>
        [HttpPut(Router.AppUserRouting.Edit)]
        public async Task<IActionResult> EditUser([FromBody] EditUserCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        /// <summary>
        /// إضافة مؤسسة عامة
        /// Add public organization
        /// </summary>
        /// <param name="command">بيانات المؤسسة / Organization data</param>
        /// <returns>نتيجة العملية / Operation result</returns>
        [HttpPut(Router.AppUserRouting.publicOrganization)]
        public async Task<IActionResult> publicOrganization([FromForm] AddpublicCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        /// <summary>
        /// إضافة جمعية خيرية
        /// Add charity organization
        /// </summary>
        /// <param name="command">بيانات الجمعية / Charity data</param>
        /// <returns>نتيجة العملية / Operation result</returns>
        [HttpPut(Router.AppUserRouting.CharityOrganization)]
        public async Task<IActionResult> CharityOrganization([FromForm] AddCharityCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        /// <summary>
        /// حذف مستخدم
        /// Delete user
        /// </summary>
        /// <param name="id">معرف المستخدم / User ID</param>
        /// <returns>نتيجة العملية / Operation result</returns>
        [HttpDelete(Router.AppUserRouting.Delete)]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            var response = await Mediator.Send(new DeleteUserCommand(id));
            return NewResult(response);
        }
    }
}