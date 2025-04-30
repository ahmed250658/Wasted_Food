using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Wasted_Food.Data.Entities;
using Wasted_Food.Data.Entities.Identity;
using Wasted_Food.Infrastructure.Data;
using Wasted_Food.Infrastructure.Repository.Abstracts;
using Wasted_Food.Service.Abstracts;



namespace Wasted_Food.Service.Implementions
{
    public class FoodRequestService : IFoodRequestService
    {
        #region Fields
        private readonly IFoodRequestRepository _foodRequestRepository;
        private readonly IDonationService _donationService;
        private readonly UserManager<Users> _userManager;
        private readonly AppDbContext _context;
        #endregion

        #region Constructor
        public FoodRequestService(IFoodRequestRepository foodRequestRepository, AppDbContext context, UserManager<Users> userManager, IDonationService donationService)
        {
            _foodRequestRepository = foodRequestRepository;
            _context = context;
            _userManager = userManager;
            _donationService = donationService;
        }

        #endregion

        #region Handler Function
        public async Task<string> CreateAsync(Donations donation, int userid)
        {

            var user = await _userManager.FindByIdAsync(userid.ToString());
            if (user == null)
                return "UserNotFound";
            var foodRequest = new FoodRequest
            {
                DontId = donation.DontId,
                RequestedBy = user.UserName,
                Status = RequestStatus.Pending,
                ExpiryData = donation.EndExpiryDate,
            };
            await _foodRequestRepository.AddAsync(foodRequest);
            return "Success";
        }



        public async Task<List<FoodRequest>> GetAllPendingAsync()
        {
            return await _foodRequestRepository.GetTableNoTracking().Where(x => x.Status == RequestStatus.Pending).
                Include(x => x.Donation).
                ToListAsync();
        }
        public async Task<FoodRequest> GetByIdAsync(int id)
        {
            return await _foodRequestRepository.GetTableNoTracking()
          .Where(x => x.RequestId.Equals(id))
           .Include(x => x.Donation)
           .FirstOrDefaultAsync();
        }

        //public async Task<string> UpdateStatusAsync(FoodRequest foodRequest, RequestStatus status)
        //{

        //    // قم بتحديث حالة الطلب
        //    foodRequest.Status = status;

        //    // تحديث حالة التوفر بناءً على حالة الطلب
        //    if (foodRequest.Donation != null)
        //    {
        //        switch (status)
        //        {
        //            case RequestStatus.Accepted:

        //                break;

        //            case RequestStatus.Rejected:
        //            case RequestStatus.Cancelled:
        //                // تحقق إذا كان هناك طلبات مقبولة أخرى لهذا التبرع
        //                bool hasOtherAcceptedRequests = foodRequest.Donation.FoodRequests?
        //                    .Any(r => r.RequestId != foodRequest.RequestId &&
        //                              r.Status == RequestStatus.Accepted) ?? false;


        //                break;

        //                // الحالة الافتراضية (Pending) لا تغير حالة التوفر
        //        }
        //    }

        //    await _foodRequestRepository.UpdateAsync(foodRequest);
        //    if (foodRequest.Donation != null)
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    return "Success";
        //}

        //public async Task<string> DeleteRequestAsync(FoodRequest foodRequest)
        //{

        //    if (foodRequest != null)
        //    {
        //        await _foodRequestRepository.DeleteAsync(foodRequest);
        //        return "Success";
        //    }
        //    return "CannotDelete";
        //}

        public async Task<List<FoodRequest>> GetAccpetListAsync()
        {
            return await _foodRequestRepository.GetTableNoTracking().Where(x => x.Status == RequestStatus.Accepted).
                Include(x => x.Donation).
                ToListAsync();
        }

        public async Task<string> AcceptedStatusAsync(int requestId)
        {

            var request = await _context.foodRequests
                .Include(r => r.Donation)
                .FirstOrDefaultAsync(r => r.RequestId == requestId);

            if (request == null)
                return "Request not found";
            if (request.Status == RequestStatus.Accepted || request.Status == RequestStatus.Rejected || request.Status == RequestStatus.Cancelled)
            {
                return "AllreadySelected";
            }

            // قبول الطلب
            request.Status = RequestStatus.Accepted;

            // جعل التبرع غير متاح
            request.Donation.IsAvailable = false;
            await _context.SaveChangesAsync();


            return "Success";
        }

        public async Task<string> RejectedStatusAsync(int requestId)
        {

            var request = await _context.foodRequests
                .Include(r => r.Donation)
                .FirstOrDefaultAsync(r => r.RequestId == requestId);

            if (request == null)
                return "Request not found";
            if (request.Status == RequestStatus.Accepted || request.Status == RequestStatus.Rejected || request.Status == RequestStatus.Cancelled)
            {
                return "AllreadySelected";
            }

            // قبول الطلب
            request.Status = RequestStatus.Rejected;

            // جعل التبرع غير متاح
            request.Donation.IsAvailable = true;
            await _context.SaveChangesAsync();


            return "Success";
        }

        public async Task<string> CanceldStatusAsync(int requestId)
        {

            var request = await _context.foodRequests
                .Include(r => r.Donation)
                .FirstOrDefaultAsync(r => r.RequestId == requestId);

            if (request == null)
                return "Request not found";
            if (request.Status == RequestStatus.Accepted || request.Status == RequestStatus.Rejected || request.Status == RequestStatus.Cancelled)
            {
                return "AllreadySelected";
            }

            // قبول الطلب
            request.Status = RequestStatus.Cancelled;

            // جعل التبرع غير متاح
            request.Donation.IsAvailable = true;
            await _context.SaveChangesAsync();


            return "Success";
        }


        #endregion
    }
}
