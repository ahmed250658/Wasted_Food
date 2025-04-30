using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Wasted_Food.Data.Entities
{

    public class Donations
    {
        public Donations()
        {
            FoodRequests = new HashSet<FoodRequest>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DontId { get; set; }
        public string Name { get; set; }
        public string? RestaurantName { get; set; }
        public decimal Quantity { get; set; }
        public bool IsExpire { get; set; } = false;
        public bool IsAvailable { get; set; } = true;
        public DateTime ExpiryData { get; set; } = DateTime.UtcNow;
        public DateTime EndExpiryDate { get; set; } = DateTime.UtcNow.AddDays(2);
        public string Location { get; set; }
        public string Image { get; set; }
        [InverseProperty("Donation")]
        public ICollection<FoodRequest> FoodRequests { get; set; }

    }
}
