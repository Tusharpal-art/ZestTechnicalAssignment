using Microsoft.AspNetCore.Identity;

namespace ZestTechnicalAssignment.Domain.Entities
{
    public class User: IdentityUser<Guid>
    {
       
        public string Name { get; set; }

        public virtual ICollection<Student>? Students { get; set; }
    }
}
