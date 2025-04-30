using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wasted_Food.Api.Base;
using Wasted_Food.Core.Feautres.FoodRequsts.Command.Models;
using Wasted_Food.Core.Feautres.FoodRequsts.Query.Models;
using Wasted_Food.Data.AppMetaData;

namespace Wasted_Food.Api.Controllers
{
    [ApiController]
    public class FoodRequestController : AppBaseController
    {
        /// <summary>
        /// إنشاء طلب غذاء جديد
        /// </summary>
        /// <param name="command">بيانات طلب الغذاء المراد إنشاؤه</param>
        /// <returns>نتيجة عملية الإنشاء</returns>
        [Authorize(Roles = "Admin,CharityOrganization")]
        [HttpPost(Router.FoodRequest.Create)]
        public async Task<ActionResult> Create([FromForm] CreateFoodRequest command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// الحصول على قائمة بجميع طلبات الغذاء
        /// </summary>
        /// <returns>قائمة بطلبات الغذاء</returns>
        [Authorize(Roles = "Admin,PublicInstitution")]
        [HttpGet(Router.FoodRequest.GetListRequest)]
        public async Task<IActionResult> GetListRequest()
        {
            var response = await Mediator.Send(new GetFoodRequestList());
            return Ok(response);
        }

        /// <summary>
        /// الحصول على قائمة طلبات السلة (للجمعيات الخيرية)
        /// </summary>
        /// <returns>قائمة بطلبات السلة</returns>
        [Authorize(Roles = "Admin,CharityOrganization")]
        [HttpGet(Router.FoodRequest.GetListCart)]
        public async Task<IActionResult> GetListCart()
        {
            var response = await Mediator.Send(new GetCartList());
            return Ok(response);
        }

        /// <summary>
        /// الحصول على قائمة الطلبات المقبولة
        /// </summary>
        /// <returns>قائمة بالطلبات المقبولة</returns>
        [Authorize(Roles = "Admin,CharityOrganization")]
        [HttpGet(Router.FoodRequest.GetListAccept)]
        public async Task<IActionResult> GetListAccept()
        {
            var response = await Mediator.Send(new GetListAcceptedCommand());
            return Ok(response);
        }

        /// <summary>
        /// قبول طلب غذاء
        /// </summary>
        /// <param name="command">بيانات الطلب المراد قبوله</param>
        /// <returns>حالة عملية القبول</returns>
        [Authorize(Roles = "Admin,PublicInstitution")]
        [HttpPut(Router.FoodRequest.AcceptedRequestCommand)]
        public async Task<IActionResult> AcceptedStatusRequest([FromForm] AcceptedRequestCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        /// <summary>
        /// رفض طلب غذاء
        /// </summary>
        /// <param name="command">بيانات الطلب المراد رفضه</param>
        /// <returns>حالة عملية الرفض</returns>
        [Authorize(Roles = "Admin,PublicInstitution")]
        [HttpPut(Router.FoodRequest.RejectedRequestCommand)]
        public async Task<IActionResult> RejectedStatusRequest([FromForm] RejectedRequestCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        /// <summary>
        /// إلغاء طلب غذاء
        /// </summary>
        /// <param name="command">بيانات الطلب المراد إلغاؤه</param>
        /// <returns>حالة عملية الإلغاء</returns>
        [Authorize(Roles = "Admin,CharityOrganization")]
        [HttpPut(Router.FoodRequest.CancleRequestCommand)]
        public async Task<IActionResult> CancleStatusRequest([FromForm] CancleRequestCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        //[HttpDelete(Router.FoodRequest.Delete)]
        //public async Task<IActionResult> Delete([FromRoute] int id)
        //{
        //    return NewResult(await Mediator.Send(new DeleteRequestCommand(id)));
        //}
    }
}