

namespace SecondTask.BusinessLogic.Interfaces
{
    public interface IFileService
    {
        public void Add(Model.Models.File file);
        public List<Model.Models.File> GetFiles();

    }
}
