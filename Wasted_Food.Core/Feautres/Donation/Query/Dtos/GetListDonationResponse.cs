namespace Wasted_Food.Core.Feautres.Donation.Query.Dtos
{
    public class GetListDonationResponse
    {
        public string Name { get; set; }
        public string ResturnatName { get; set; }
        public decimal Quantity { get; set; }
        public DateTime ExpiryData { get; set; }
        public string Location { get; set; }
        public string Image { get; set; }

    }
}
