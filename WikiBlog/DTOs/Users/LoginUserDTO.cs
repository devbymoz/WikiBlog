namespace WikiBlog.DTOs.Users
{
    public class LoginUserDTO
    {
        public string Login { get; internal set; }
        public string Password { get; internal set; }
        public bool RememberMe { get; internal set; }
    }
}
