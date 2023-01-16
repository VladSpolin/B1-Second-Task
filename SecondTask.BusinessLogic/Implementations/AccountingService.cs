using SecondTask.BusinessLogic.Interfaces;
using SecondTask.Model;
using SecondTask.Model.Models;


namespace SecondTask.BusinessLogic.Implementations
{
    public class AccountingService:IAccountingService
    {
        private readonly ApplicationContext _applicationContext;
        public AccountingService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }



        public void ImportRange(List<Accounting> lines) //imports list of lines to db
        {  
            _applicationContext.Accountings.AddRange(lines);
            _applicationContext.SaveChanges();
        }
    }
}
