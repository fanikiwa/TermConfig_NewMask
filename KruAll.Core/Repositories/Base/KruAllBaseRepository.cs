﻿using KruAll.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KruAll.Core.Repositories.Base
{
    public class KruAllBaseRepository<T> where T : class
    {
        #region Constructor
        public KruAllBaseRepository() { }

        #endregion

        #region Properties

        internal PZE_Entities _contextPZE = new PZE_Entities();

        #endregion

        #region Methods

        /*
         * All methods are virtual to allow for overriding, however they are also
         * protected meaning they can only be accessed, and overridden from within 
         * classes that are derived from this class.
         */

        protected virtual IQueryable<T> GetAll()
        {
            IQueryable<T> query = _contextPZE.Set<T>();
            return query;
        }

        protected virtual void Add(T entity)
        {
            if (entity == null) throw new ArgumentNullException("Entity should not be null");
            _contextPZE.Set<T>().Add(entity);
        }

        /// <summary>
        /// Generic function to query a collection of items
        /// </summary>
        /// <param name="predicate">the predicate parameter expects a 
        /// lambda function e.g (x => x.[field] == value)</param>
        /// <returns></returns>
        protected virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null) throw new ArgumentNullException("Predicate is required.");
            IQueryable<T> query = _contextPZE.Set<T>().Where(predicate);
            return query;
        }

        protected virtual void Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException("Entity should not be null");
            _contextPZE.Set<T>().Remove(entity);
        }

        protected virtual void Edit(T entity)
        {
            if (entity == null) throw new ArgumentNullException("Entity should not be null");
            _contextPZE.Entry(entity).State = EntityState.Modified;
        }

        protected virtual void Save()
        {
            _contextPZE.SaveChanges();
        }

        /// <summary>
        /// The method checks if an entity exists in the 
        /// entity collection. To use method the entity
        /// must implement an equality comparer.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected virtual bool Exists(T entity)
        {
            if (entity == null) throw new ArgumentNullException("Entity should not be null");
            return _contextPZE.Set<T>().Contains(entity);
        }

        #endregion
    }
}
