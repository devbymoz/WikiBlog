using System;

namespace WikiBlog.Services
{
    public class UserService
    {
        public static bool IsMajor(DateTime date)
        {
            if (date.AddYears(18) < DateTime.Now)
            {
                return false;
            }
            
            return true;
        }
    }
}
