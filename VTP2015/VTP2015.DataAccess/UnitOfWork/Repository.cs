using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using VTP2015.Entities;

namespace VTP2015.DataAccess.UnitOfWork
{
    public class Repository<T> : IRepository<T> where T : BaseEntity 
    {
        private readonly Context _context;
        private IDbSet<T> _entities;
        string _errorMessage = string.Empty;

        public Repository(Context context)
        {
            _context = context;
        }

        public T GetById(object id)
        {
            return Entities.Find(id);
        }

        public void Insert(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }
                Entities.Add(entity);
                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (
                    var validationError in
                        dbEx.EntityValidationErrors.SelectMany(validationErrors => validationErrors.ValidationErrors))
                {
                    _errorMessage += $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}" +
                                     Environment.NewLine;
                }
                throw new Exception(_errorMessage, dbEx);
            }
        }

        public void Update(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }
                Entities.AddOrUpdate(entity);
                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (
                    var validationError in
                        dbEx.EntityValidationErrors.SelectMany(validationErrors => validationErrors.ValidationErrors))
                {
                    _errorMessage += Environment.NewLine +
                                     $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}";
                }

                throw new Exception(_errorMessage, dbEx);
            }
        }

        public void Delete(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }

                Entities.Remove(entity);
                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (
                    var validationError in
                        dbEx.EntityValidationErrors.SelectMany(validationErrors => validationErrors.ValidationErrors))
                {
                    _errorMessage += Environment.NewLine +
                                     $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}";
                }
                throw new Exception(_errorMessage, dbEx);
            }
        }

        public void Delete(int id)
        {
            var model = GetById(id);
            Delete(model);
        }

        public virtual IQueryable<T> Table => this.Entities;

        private IDbSet<T> Entities => _entities ?? (_entities = _context.Set<T>());
    }
}
