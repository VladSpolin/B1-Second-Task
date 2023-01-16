using SecondTask.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondTask.BusinessLogic.Interfaces
{
    public interface IAccountingService
    {
        public void ImportRange(List<Accounting> lines);
    }
}
