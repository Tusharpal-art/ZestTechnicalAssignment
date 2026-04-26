using System;
using System.Collections.Generic;
using System.Text;

namespace ZestTechicalAssignment.Business.Interfaces
{
    public interface IUnitOfRepositories
    {
        public IStudentRepositories StudentRepo { get; }
        public IGenericRepositories<T> GetRepository<T>() where  T : class;

        Task SaveChangesAsync(CancellationToken token);



    }
}
