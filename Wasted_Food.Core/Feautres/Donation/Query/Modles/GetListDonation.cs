using MediatR;
using Wasted_Food.Core.Bases;
using Wasted_Food.Core.Feautres.Donation.Query.Dtos;

namespace Wasted_Food.Core.Feautres.Donation.Query.Modles
{
    public class GetListDonation : IRequest<Response<List<GetListDonationResponse>>>
    {

    }
}
