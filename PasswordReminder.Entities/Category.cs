namespace PasswordReminder.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<PasswordRecord> PasswordRecords { get; set; }
    }
}
