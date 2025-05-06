using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ITodoRepository Todos { get; }
        Task<int> CompleteAsync();
    }

}
