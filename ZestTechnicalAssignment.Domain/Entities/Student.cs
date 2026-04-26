using System;
using System.Collections.Generic;
using System.Text;

namespace ZestTechnicalAssignment.Domain.Entities
{
    public class Student
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string Course { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public Guid? CreatedById { get; set; }
        public virtual User? CreatedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
