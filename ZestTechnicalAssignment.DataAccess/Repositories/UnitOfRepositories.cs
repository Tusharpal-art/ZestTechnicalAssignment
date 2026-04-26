using System;
using System.Collections.Generic;
using System.Text;
using ZestTechicalAssignment.Business.Interfaces;
using ZestTechnicalAssignment.DataAccess.ApplicationContext;

namespace ZestTechnicalAssignment.DataAccess.Repositories
{
    public class UnitOfRepositories(ApplicationDBContext applicationDB,IStudentRepositories student) : IUnitOfRepositories
    {
        private  Dictionary<Type,object> keyValues = new();
        private ApplicationDBContext applicationDBContext  = applicationDB;
        public IStudentRepositories StudentRepo => student;

        public IGenericRepositories<T> GetRepository<T>() where T : class 
        {
        
            if (!keyValues.ContainsKey(typeof(T)))
            {
               var value  = new GenericRepositories<T>(applicationDBContext);
                keyValues[typeof(T)] = (object)value;
            }

            return (IGenericRepositories<T>)keyValues[typeof(T)];    
        }

        public async Task SaveChangesAsync(CancellationToken token)
        {
           await applicationDBContext.SaveChangesAsync(token);   
        }
    }
}
