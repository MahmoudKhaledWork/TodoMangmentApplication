using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using DAL.Models;

namespace WebApp.Controllers
{
    public class TodoController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public TodoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: /Todo/
        public async Task<IActionResult> Index(string status = "")
        {
            var todos = await _unitOfWork.Todos.GetListAsync();
            if (!string.IsNullOrEmpty(status))
            {
                todos = todos.Where(t => t.Status.ToString() == status).ToList();
            }
            return View(todos);
        }

        // GET: /Todo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Todo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Todo todo)
        {
            if (ModelState.IsValid)
            {
                todo.Id = Guid.NewGuid(); 
                await _unitOfWork.Todos.AddAsync(todo);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(todo);
        }

        // GET: /Todo/Edit/{id}
        public async Task<IActionResult> Edit(Guid id)
        {
            var todo = await _unitOfWork.Todos.GetAsync(id);
            if (todo == null)
            {
                return NotFound();
            }
            return View(todo);
        }

        // POST: /Todo/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Todo todo)
        {
            if (id != todo.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Todos.UpdateAsync(todo);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(todo);
        }

        // GET: /Todo/Delete/{id}
        public async Task<IActionResult> Delete(Guid id)
        {
            var todo = await _unitOfWork.Todos.GetAsync(id);
            if (todo == null)
            {
                return NotFound();
            }
            return View(todo);
        }

        // POST: /Todo/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var todo = await _unitOfWork.Todos.GetAsync(id);
            if (todo != null)
            {
                await _unitOfWork.Todos.DeleteAsync(id);
                await _unitOfWork.CompleteAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
