using MediatR;
using Wasted_Food.Core.Bases;

namespace Wasted_Food.Core.Feautres.Donation.Command.Models
{
    public class DeleteDonationCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public DeleteDonationCommand(int id)
        {
            Id = id;
        }
    }
}

