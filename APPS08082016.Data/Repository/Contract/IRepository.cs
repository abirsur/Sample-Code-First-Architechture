using System.Collections.Generic;
using System.Linq;
using APPS08082016.Core.EntityModel.Base;

namespace APPS08082016.Data.Repository.Contract
{
    /// <summary>
    /// Repository
    /// </summary>
    public partial interface IRepository<T> where T : BaseEntity
    {
        T GetById(object id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        IQueryable<T> Table { get; }
        IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters)
             where TEntity : BaseEntity, new();
        int ExecuteSqlCommand(string commandText, params object[] parameters);
        IEnumerable<TEnity> SqlQuery<TEnity>(string commandText, params object[] parameters);
    }
}
