using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace server.Utils
{
    public interface IUtilRepository
    {
        public ICollection<T> GetList<T>(Expression<Func<T, bool>> predicate) where T : class;
        public T Get<T>(Expression<Func<T, bool>> predicate) where T : class;
        public bool Create<T>(T entity) where T : class;
        public bool Update<T>(T entity, params Expression<Func<T, object>>[] projectionProperties) where T : class;
        public bool Delete<T>(T entity) where T : class;
        public bool DoesExist<T>(Expression<Func<T, bool>> predicate) where T : class;
        public bool Save();


    }
}