using BLL.Interfaces;
using DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TodoDbContext _context;        
        public ITodoRepository Todos { get ;}

        public UnitOfWork(TodoDbContext context)
        {
            _context = context;
            Todos = new TodoRepository(context);
        }


        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
