using MediatR;
using Microsoft.AspNetCore.Http;
using Wasted_Food.Core.Bases;

namespace Wasted_Food.Core.Feautres.Donation.Command.Models
{
    public class CreateDonationCommand : IRequest<Response<string>>
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public DateTime ExpiryData { get; set; }
        public string Location { get; set; }
        public IFormFile Image { get; set; }
    }
}
