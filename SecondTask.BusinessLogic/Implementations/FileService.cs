using Microsoft.EntityFrameworkCore;
using SecondTask.BusinessLogic.Interfaces;
using SecondTask.Model;


namespace SecondTask.BusinessLogic.Implementations
{
    public class FileService : IFileService
    {
        private readonly ApplicationContext _context;

        public FileService(ApplicationContext context)
        {
            _context = context;
        }

        public void Add(Model.Models.File file)
        {
            if (file != null)
            {
                _context.Files.Add(file);
                _context.SaveChanges();                
            }
        }

        public List<Model.Models.File> GetFiles()
        {
            return _context.Files.Include(f=>f.Data).ToList(); //returns list files with data of accounting of each file
        }
    }
}
