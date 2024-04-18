
namespace WikiBlog.Models
{
    public class Theme
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Article>? Articles { get; set;}
    }
}
