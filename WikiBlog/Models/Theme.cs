
namespace WikiBlog.Models
{
    public class Theme
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Article>? Articles { get; set;}

        internal object Select(Func<object, object> value)
        {
            throw new NotImplementedException();
        }
    }
}
