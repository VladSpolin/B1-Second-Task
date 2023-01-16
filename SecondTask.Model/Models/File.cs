

namespace SecondTask.Model.Models
{
    public class File
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Accounting> Data { get; set; }
    }
}
