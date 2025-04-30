using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wasted_Food.Data.Entities
{
    public class FoodRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RequestId { get; set; }
        public int DontId { get; set; }
        public string RequestedBy { get; set; }
        public DateTime ExpiryData { get; set; }
        public RequestStatus Status { set; get; } = RequestStatus.Pending;
        public DateTime RequestDate { get; set; } = DateTime.UtcNow;

        [ForeignKey("DontId")]
        [InverseProperty("FoodRequests")]
        public virtual Donations Donation { get; set; }

    }

    public enum RequestStatus
    {
        Pending,
        Accepted,
        Rejected,
        Cancelled
    }

}

