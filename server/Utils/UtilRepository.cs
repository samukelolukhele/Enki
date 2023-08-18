using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Data;

namespace server.Utils
{
    public class UtilRepository : IUtilRepository
    {
        private readonly ServerDBContext _context;
        public string errorMessage;
        public UtilRepository(ServerDBContext _context)
        {
            this._context = _context;
        }

        //TODO: Return specific exceptions.

        //* CRUD methods.


        /// <summary>
        /// Generic get method that returns a collection of entities made to be implemented by a specific repository.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public ICollection<T> GetList<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return _context.Set<T>().Where(predicate).ToList();
        }

        /// <summary>
        /// Generic get method that returns a single entity made to be implemented by a specific repository.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public T Get<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        /// <summary>
        ///  Generic create method that returns a boolean value made to be implemented by a specific repository.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public bool Create<T>(T entity) where T : class
        {
            if (entity == null)
                return false;

            entity.GetType().GetProperty("id").SetValue(entity, Guid.NewGuid());

            _context.Set<T>().Add(entity);
            return Save();
        }

        /// <summary>
        /// Generic update method that takes the new entity object as a first parameter, excluded entity fields as the second parameter and returns a boolean value made to be implemented by a specific repository.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public bool Update<T>(T entity, params Expression<Func<T, object>>[] properties) where T : class
        {
            var dbEntry = _context.Entry(entity);

            dbEntry.State = EntityState.Modified;

            foreach (var includeProperty in properties)
            {
                dbEntry.Property(includeProperty).IsModified = false;
            }

            return Save();
        }

        /// <summary>
        /// Generic delete method that returns a boolean value made to be implemented by a specific repository.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public bool Delete<T>(T entity) where T : class
        {
            _context.Set<T>().Remove(entity);
            return Save();

        }

        // *Utility methods

        /// <summary>
        /// Saves any changes to the database
        /// </summary>
        /// <returns>If updated databases entries is greater than one, then it returns true.</returns> 
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool DoesExist<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return _context.Set<T>().Any(predicate);
        }
    }
}