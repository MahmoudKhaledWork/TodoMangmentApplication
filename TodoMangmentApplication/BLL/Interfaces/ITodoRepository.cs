using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITodoRepository
    {
        Task<Todo?> GetAsync(Guid id);
        Task<IReadOnlyList<Todo>> GetListAsync(TodoStatus? status = null);
        Task AddAsync(Todo todo);
        Task UpdateAsync(Todo todo);
        Task DeleteAsync(Guid id);
        Task MarkAsCompletedAsync(Guid id);
    }
}
