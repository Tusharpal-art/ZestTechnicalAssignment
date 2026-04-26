using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ZestTechicalAssignment.Business.Interfaces;
using ZestTechnicalAssignment.DataAccess.ApplicationContext;

namespace ZestTechnicalAssignment.DataAccess.Repositories
{
    public class GenericRepositories<T> (ApplicationDBContext dBContext): IGenericRepositories<T> where T : class
    {
        public async Task<T> Add(T Request)
        {
            var result = await dBContext.Set<T>().AddAsync(Request);
            return  Request;
        }

        public async Task<bool> Delete(Guid Id)
        {
            var result =  await dBContext.Set<T>().FindAsync(Id);
            if(result != null)
            {
                dBContext.Set<T>().Remove(result);
                return true;
            }
            return false ;
        }

        public async Task<List<T>> GetAll()
        {
           return await dBContext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetById(Guid Id)
        {
            return await dBContext.Set<T>().FindAsync(Id);
        }

        public async Task<T> Update(T Request)
        {
            var result =  dBContext.Set<T>().Update(Request);
            return await Task.FromResult(Request);
        }
    }
}
