using OneC.EntityData.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace OneC.BusinessLogic.Services
{
    public abstract class BaseService<T> where T : class
    {
        protected IDataContext dataContext;
        public readonly IDbSet<T> dbSet;

        protected BaseService(IDataContext dataContext)
        {
            this.dataContext = dataContext;
            dbSet = dataContext.Set<T>();
        }

        public virtual void Add(T entity)
        {
            dbSet.Add(entity);
            dataContext.SaveChanges();
        }

        public virtual void AddRange(IEnumerable<T> entities)
        {
            foreach (T entity in entities)
            {
                dbSet.Add(entity);
            }

            dataContext.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            dbSet.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
            dataContext.SaveChanges();
        }

        public virtual void Update(IEnumerable<T> entities)
        {
            foreach (T entity in entities)
            {
                dbSet.Attach(entity);
                dataContext.Entry(entity).State = EntityState.Modified;
            }

            dataContext.SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            dbSet.Remove(entity);
            dataContext.SaveChanges();
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbSet.Where(where).AsEnumerable();

            foreach (T obj in objects)
                dbSet.Remove(obj);

            dataContext.SaveChanges();
        }

        public virtual void Delete(IEnumerable<T> entitites)
        {
            foreach (T entity in entitites.ToArray())
            {
                dbSet.Remove(entity);
            }

            dataContext.SaveChanges();
        }

        public virtual T GetById(long id)
        {
            return dbSet.Find(id);
        }

        public virtual List<T> GetAll()
        {
            return dbSet.ToList();
        }

        public virtual List<T> GetMany(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).ToList();
        }

        public virtual T Get(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).FirstOrDefault();
        }

        public virtual int Count()
        {
            return dbSet.Count();
        }

        public virtual int Count(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).Count();
        }

        protected void EnableAutoDetectChanges()
        {
            ((DbContext)dataContext).Configuration.AutoDetectChangesEnabled = true;
        }

        protected void DisableAutoDetectChanges()
        {
            ((DbContext)dataContext).Configuration.AutoDetectChangesEnabled = false;
        }
    }

    public interface IService<T> where T : class
    {
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void Update(IEnumerable<T> entities);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);
        void Delete(IEnumerable<T> entities);
        T GetById(long id);
        List<T> GetAll();
        T Get(Expression<Func<T, bool>> where);
        List<T> GetMany(Expression<Func<T, bool>> where);
        int Count();
        int Count(Expression<Func<T, bool>> where);
    }
}
