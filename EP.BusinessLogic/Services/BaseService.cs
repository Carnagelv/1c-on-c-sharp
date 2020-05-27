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
        protected IDataContext DataContext;
        public readonly IDbSet<T> Dbset;

        protected BaseService(IDataContext dataContext)
        {
            DataContext = dataContext;
            Dbset = dataContext.Set<T>();
        }

        public virtual void Add(T entity)
        {
            Dbset.Add(entity);
            DataContext.SaveChanges();
        }

        public virtual void AddRange(IEnumerable<T> entities)
        {
            foreach (T entity in entities)
            {
                Dbset.Add(entity);
            }

            DataContext.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            Dbset.Attach(entity);
            DataContext.Entry(entity).State = EntityState.Modified;
            DataContext.SaveChanges();
        }

        public virtual void Update(IEnumerable<T> entities)
        {
            foreach (T entity in entities)
            {
                Dbset.Attach(entity);
                DataContext.Entry(entity).State = EntityState.Modified;
            }

            DataContext.SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            Dbset.Remove(entity);
            DataContext.SaveChanges();
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = Dbset.Where(where).AsEnumerable();

            foreach (T obj in objects)
                Dbset.Remove(obj);

            DataContext.SaveChanges();
        }

        public virtual void Delete(IEnumerable<T> entitites)
        {
            foreach (T entity in entitites.ToArray())
            {
                Dbset.Remove(entity);
            }

            DataContext.SaveChanges();
        }

        public virtual T GetById(long id)
        {
            return Dbset.Find(id);
        }

        public virtual List<T> GetAll()
        {
            return Dbset.ToList();
        }

        public virtual List<T> GetMany(Expression<Func<T, bool>> where)
        {
            return Dbset.Where(where).ToList();
        }

        public virtual T Get(Expression<Func<T, bool>> where)
        {
            return Dbset.Where(where).FirstOrDefault();
        }

        public virtual int Count()
        {
            return Dbset.Count();
        }

        public virtual int Count(Expression<Func<T, bool>> where)
        {
            return Dbset.Where(where).Count();
        }

        protected void EnableAutoDetectChanges()
        {
            ((DbContext)DataContext).Configuration.AutoDetectChangesEnabled = true;
        }

        protected void DisableAutoDetectChanges()
        {
            ((DbContext)DataContext).Configuration.AutoDetectChangesEnabled = false;
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
