namespace PasswordReminder.Entities
{
    public class PasswordRecord
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        // Foreign Keys
        public int UserId { get; set; }
        public User? User { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
