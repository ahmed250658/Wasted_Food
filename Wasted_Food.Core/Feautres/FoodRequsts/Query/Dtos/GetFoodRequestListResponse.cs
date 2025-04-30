namespace Wasted_Food.Core.Feautres.FoodRequsts.Query.Dtos
{
    public class GetFoodRequestListResponse
    {
        public string MealName { get; set; }
        public int Quantity { get; set; }
        public string RequestedBy { get; set; }
        public string Status { get; set; }

    }
}
