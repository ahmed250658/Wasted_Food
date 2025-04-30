using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wasted_Food.Api.Base;
using Wasted_Food.Core.Feautres.Donation.Command.Models;
using Wasted_Food.Core.Feautres.Donation.Query.Modles;
using Wasted_Food.Data.AppMetaData;

namespace Wasted_Food.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin,PublicInstitution")]
    public class DonationController : AppBaseController
    {

        [HttpPost(Router.Donation.Create)]

        /// <summary>
        /// إنشاء تبرع جديد
        /// </summary>
        /// <param name="command">بيانات التبرع</param>
        /// <returns>نتيجة العملية</returns>
        public async Task<IActionResult> CreateDonation([FromForm] CreateDonationCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [Authorize(Roles = "Admin,PublicInstitution,CharityOrganization")]
        [HttpGet(Router.Donation.GetListDonation)]

        /// <summary>
        /// إنشاء تبرع جديد
        /// </summary>
        /// <param name="command">ارجاع بيانات التبرع</param>
        /// <returns>نتيجة العملية</returns>
        public async Task<IActionResult> GetListDonation()
        {
            var response = await Mediator.Send(new GetListDonation());
            return Ok(response);
        }
        //[Authorize(Roles = "Admin,PublicInstitution")]
        //[HttpDelete(Router.Donation.Delete)]
        //public async Task<IActionResult> DeleteUser([FromRoute] int id)
        //{
        //    var response = await Mediator.Send(new DeleteDonationCommand(id));
        //    return NewResult(response);
        //}

    }
}
