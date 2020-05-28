namespace Users.API.Models
{
    public class Phone
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int Area_code { get; set; }
        public string Country_code { get; set; }
        public User User { get; set; }
    }
}