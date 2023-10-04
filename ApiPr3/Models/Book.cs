namespace ApiPr3.Models
{
    public class Book
    {
        public int ID { get; set; }

        public int PublisherID { get; set; }

        public string Title { get; set; }
                                
        public int Pages { get; set; }

        public int? Weight { get; set; }

        public string Subject { get; set; }
    }
}