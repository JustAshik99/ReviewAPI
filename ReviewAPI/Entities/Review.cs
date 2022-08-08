namespace ReviewAPI.Entities
{
    public class Review
    {
        public int id { get; set; }
        public string name { get; set; }
        public string review { get; set; }
        public int rating { get; set; }
        public string restaurantID { get; set; }
    }
}
