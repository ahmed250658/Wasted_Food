using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Wasted_Food.Data.Entities;
using Wasted_Food.Data.Entities.Identity;
using Wasted_Food.Infrastructure.Data;
using Wasted_Food.Infrastructure.Repository.Abstracts;
using Wasted_Food.Service.Abstracts;


namespace Wasted_Food.Service.Implementions
{
    public class DonationService : IDonationService
    {
        #region Fields
        private readonly IDonationRepository _donationRepository;
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<Users> _userManager;
        private readonly AppDbContext _context;
        #endregion

        #region Constructor
        public DonationService(IDonationRepository donationRepository, IFileService fileService, IHttpContextAccessor httpContextAccessor, UserManager<Users> userManager, AppDbContext context)
        {
            _donationRepository = donationRepository;
            _fileService = fileService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _context = context;
        }

        #endregion

        #region Handler Function
        public async Task<Donations> GetByIDAsync(int id)
        {
            return await _donationRepository.GetTableNoTracking()
            .FirstOrDefaultAsync(d => d.DontId == id);
        }
        public async Task<bool> NameIsExist(string name)
        {
            //Check if the name is Exist Or not
            var Donation = _donationRepository.GetTableNoTracking().Where(x => x.Name.Equals(name)).FirstOrDefault();
            if (Donation == null) return false;
            return true;
        }
        public async Task<string> AddDonationAsync(int id, Donations donation, IFormFile file)
        {
            var context = _httpContextAccessor.HttpContext.Request;
            var baseUrl = context.Scheme + "://" + context.Host;
            var imageUrl = await _fileService.UploadImage("Donation", file);
            switch (imageUrl)
            {
                case "NoImage": return "NoImage";
                case "FailedToUploadImage": return "FailedToUploadImage";
            }
            donation.Image = baseUrl + imageUrl;
            try
            {
                var requesterName = await _userManager.FindByIdAsync(id.ToString());
                donation.RestaurantName = requesterName.UserName;
                await _donationRepository.AddAsync(donation);
                return "Success";
            }
            catch (Exception)
            {
                return "FailedInAdd";
            }
        }

        public async Task<List<Donations>> GetDonationListAsync()
        {

            return await _donationRepository.GetTableNoTracking().Where(x => x.IsAvailable).ToListAsync();


        }
        public async Task<string> DeleteDonationWithRequests(int dontId)
        {
            var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // 1. احصل على التبرع مع الطلبات المرتبطة به
                var donation = await _context.donations
                    .Include(d => d.FoodRequests)
                    .FirstOrDefaultAsync(d => d.DontId == dontId);

                if (donation == null)
                    return "Donation not found";

                if (donation.IsAvailable == false)
                {
                    // 2. احذف الطلبات المرتبطة أولاً
                    _context.foodRequests.RemoveRange(donation.FoodRequests);

                    // 3. احذف التبرع
                    _context.donations.Remove(donation);
                }
                // 4. احذف الصورة إذا لزم الأمر
                if (!string.IsNullOrEmpty(donation.Image))
                {
                    await _fileService.DeleteImage(donation.Image);
                }

                // 5. احفظ التغييرات
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return "Success";
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return "Failed";
            }
        }
        public async Task<string> DeleteExpireAsync()
        {
            var expired = await _context.donations
            .Where(d => d.ExpiryData < DateTime.UtcNow.AddDays(-2))
            .ToListAsync();

            _context.donations.RemoveRange(expired);
            await _context.SaveChangesAsync();
            return "Success";
        }

        #endregion
    }
}
