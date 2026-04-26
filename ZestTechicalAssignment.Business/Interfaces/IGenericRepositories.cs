using System;
using System.Collections.Generic;
using System.Text;

namespace ZestTechicalAssignment.Business.Interfaces
{
    public interface IGenericRepositories<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> Add(T Request);
        Task<T> Update(T Request);
        Task<bool> Delete(Guid Id);
        Task<T> GetById (Guid Id);
    }
}
