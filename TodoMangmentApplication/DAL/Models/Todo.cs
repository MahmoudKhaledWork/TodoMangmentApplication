using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Todo
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Title is Required")]
        [MaxLength(100)]
        public string Title { get; set; }
        public string? Description { get; set; }
        public TodoStatus Status { get; set; } = TodoStatus.Pending;
        public TodoPriority Priority { get; set; } = TodoPriority.Medium;
        public DateTimeOffset? DueDate { get; set; }
        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset LastModifiedDate { get; set; } = DateTimeOffset.Now;
    }
} 
