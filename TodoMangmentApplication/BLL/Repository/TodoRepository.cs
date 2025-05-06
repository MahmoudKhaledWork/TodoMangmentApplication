using BLL.Interfaces;
using DAL.Context;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repository
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoDbContext _context;

        public TodoRepository(TodoDbContext context)
        {
            _context = context;
        }

        public async Task<Todo?> GetAsync(Guid id)
        {
            return await _context.Todos.FindAsync(id);
        }

        public async Task<IReadOnlyList<Todo>> GetListAsync(TodoStatus? status = null)
        {
            IQueryable<Todo> query = _context.Todos;

            if (status.HasValue)
            {
                query = query.Where(t => t.Status == status.Value);
            }

            return await query.OrderByDescending(t => t.CreatedDate).ToListAsync();
        }

        public async Task AddAsync(Todo todo)
        {
            await _context.Todos.AddAsync(todo);
        }

        public Task UpdateAsync(Todo todo)
        {
            todo.LastModifiedDate = DateTimeOffset.Now;
            _context.Todos.Update(todo);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo != null)
            {
                _context.Todos.Remove(todo);
            }
        }


        public async Task MarkAsCompletedAsync(Guid id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo is not null)
            {
                todo.Status = TodoStatus.Completed;
                todo.LastModifiedDate = DateTimeOffset.Now;
                _context.Todos.Update(todo);
            }
        }
    }


}
