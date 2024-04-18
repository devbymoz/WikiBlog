namespace WikiBlog.DTOs.Users
{
    public class ChangePasswordDTO
    {
        public string CurrentPassword { get; internal set; }
        public string NewPassword { get; internal set; }
    }
}
