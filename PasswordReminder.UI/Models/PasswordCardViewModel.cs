namespace PasswordReminder.UI.Models
{
    public class PasswordCardViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string? CategoryName { get; set; }

        // Ekstra: Domain → logo için
        public string Domain
        {
            get
            {
                try
                {
                    return new Uri(Url).Host.Replace("www.", "");
                }
                catch
                {
                    return "";
                }
            }
        }
    }

}
