namespace SportsStore.Models
{
    public class FeedBack
    {
        public int ID { get; set; }
        public string Comment { get; set; }
        public int ApplicationUserID { get; set; }
        public Product Product { get; set; }
        public int ProductID { get; set; }
    }
}
