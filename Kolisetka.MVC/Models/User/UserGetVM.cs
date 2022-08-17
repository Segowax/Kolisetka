namespace Kolisetka.MVC.Models.User
{
    public class UserGetVM
    {
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
