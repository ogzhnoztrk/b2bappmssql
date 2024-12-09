using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using B2BApp.DataAccess.Context;
using Core.Models.Concrete;
using Core.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Linq;

namespace B2BApp.DataAccess.Repository
{
    public class SqlRepositoryBase<T> : IRepository<T> where T : class, new()
    {
        private readonly SqlDbContext _dbContext;
        internal DbSet<T> _db;
        public SqlRepositoryBase(SqlDbContext db)
        {
            _dbContext = db;
            _db = _dbContext.Set<T>();
        }
        public Result<T> Add(T entity)
        {
            try
            {
                _db.Add(entity); _dbContext.SaveChanges();
                return new Result<T>(((int)HttpStatusCode.Created), "Veri Eklendi", entity);
            }
            catch (Exception e)
            {

                return new Result<T>(((int)HttpStatusCode.BadRequest), e.Message, entity);

            }
            finally
            {
                GC.Collect();
            }

        }
        public async Task<Result<T>> AddAsync(T entity)
        {
            try
            {
                await _db.AddAsync(entity); _dbContext.SaveChanges();
                return new Result<T>(((int)HttpStatusCode.Created), "Veri Eklendi", entity);

            }
            catch (Exception e)
            {

                return new Result<T>(((int)HttpStatusCode.BadRequest), e.Message, entity);

            }
            finally
            {
                GC.Collect();
            }
        }

        public Result<List<T>> AddMany(List<T> entities)
        {
            try
            {
                _db.AddRange(entities); _dbContext.SaveChanges();
                return new Result<List<T>>(((int)HttpStatusCode.Created), nameof(T) + "Verileri Eklendi", entities);

            }
            catch (Exception e)
            {
                return new Result<List<T>>(((int)HttpStatusCode.BadRequest), e.Message, entities);


            }
            finally
            {
                GC.Collect();
            }
        }

        public Result<ICollection<T>> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            try
            {
                IQueryable<T> query = _db;
                if (filter != null)
                {
                    query = query.Where(filter);

                }
                if (includeProperties != null)
                {
                    foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProp);
                    }
                }

                return new Result<ICollection<T>>(((int)HttpStatusCode.OK), "Veri Getirildi", query.ToList());
            }
            catch (Exception e)
            {

                return new Result<ICollection<T>>(((int)HttpStatusCode.BadRequest), e.Message, null);
            }
            finally
            {
                GC.Collect();
            }
        }
        public Result<T> GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = true)
        {
            try
            {
                IQueryable<T> query;
                if (tracked)
                {
                    query = _db;

                }
                else
                {
                    query = _db.AsNoTracking();

                }

                query = query.Where(filter);
                if (includeProperties != null)
                {
                    foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProp);
                    }
                }
                return new Result<T>(((int)HttpStatusCode.OK), "Veri Getirildi", query.FirstOrDefault());
            }
            catch (Exception e)
            {

                return new Result<T>(((int)HttpStatusCode.OK), "Veri Getirildi", null);
            }
            finally
            {
                GC.Collect();
            }
        }
        public Result<T> Remove(T entity)
        {
            try
            {
                _db.Remove(entity);
                _dbContext.SaveChanges();
                return new Result<T>(((int)HttpStatusCode.OK), "Veri Silindi", entity);
            }
            catch (Exception e)
            {

                return new Result<T>(((int)HttpStatusCode.BadRequest), e.Message, null);

            }
            finally
            {
                GC.Collect();
            }
        }
        public Result<IEnumerable<T>> RemoveRange(IEnumerable<T> entity)
        {
            try
            {
                _db.RemoveRange(entity); _dbContext.SaveChanges();
                return new Result<IEnumerable<T>>(((int)HttpStatusCode.OK), "Veriler Silindi", entity);
            }
            catch (Exception e)
            {

                return new Result<IEnumerable<T>>(((int)HttpStatusCode.BadRequest), e.Message, null);

            }
            finally
            {
                GC.Collect();
            }
        }
        public Result<T> Update(T entity)
        {
            try
            {
                _db.AttachRange(entity);
                _dbContext.Entry(entity).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return new Result<T>(((int)HttpStatusCode.OK), "Veri Güncellendi", entity);
            }
            catch (Exception e)
            {
                return new Result<T>(((int)HttpStatusCode.BadRequest), e.Message, entity);

            }
            finally { GC.Collect(); }
        }

    }
}
