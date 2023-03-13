using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Linq.Expressions;

namespace SocialNetwork.Backend.Services
{
    public abstract class BaseService
    {
        protected readonly DefaultContext Context;

        public BaseService(DefaultContext context)
        {
            Context = context;
        }

        #region Find()
        public virtual TEntity Find<TEntity>(int id) where TEntity : class
        {
            return Context.Find<TEntity>(id);
        }
        #endregion

        #region FindAsync()
        public virtual async Task<TEntity> FindAsync<TEntity>(int id) where TEntity : class
        {
            return await Context.FindAsync<TEntity>(id);
        }
        #endregion


        #region Attach()
        public virtual TEntity Attach<TEntity>(TEntity entity) where TEntity : class
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                Context.Attach<TEntity>(entity);
            }

            return entity;
        }

        public virtual List<TEntity> Attach<TEntity>(params TEntity[] entities) where TEntity : class
        {
            Context.AttachRange(entities.Where(p => Context.Entry(p).State == EntityState.Detached));

            return new List<TEntity>(entities);
        }
        #endregion

        #region Detach()
        public virtual TEntity Detach<TEntity>(TEntity entity) where TEntity : class
        {
            if (Context.Entry(entity).State != EntityState.Detached)
            {
                Context.Entry(entity).State = EntityState.Detached;
            }

            return entity;
        }

        public virtual List<TEntity> Detach<TEntity>(params TEntity[] entities) where TEntity : class
        {
            foreach (var entity in entities)
            {
                Detach(entity);
            }

            return new List<TEntity>(entities);
        }
        #endregion


        #region Create()
        public virtual TEntity Create<TEntity>(TEntity entity) where TEntity : class
        {
            Context.Add<TEntity>(entity);
            SaveChanges();

            return entity;
        }

        public virtual List<TEntity> Create<TEntity>(params TEntity[] entities) where TEntity : class
        {
            if (entities.Length > 0)
            {
                Context.AddRange(entities);
                SaveChanges();
            }

            return new List<TEntity>(entities);
        }
        #endregion

        #region CreateAsync()
        public virtual async Task<TEntity> CreateAsync<TEntity>(TEntity entity) where TEntity : class
        {
            await Context.AddAsync<TEntity>(entity);
            await SaveChangesAsync();

            return entity;
        }

        public virtual async Task<List<TEntity>> CreateAsync<TEntity>(params TEntity[] entities) where TEntity : class
        {
            if (entities.Length > 0)
            {
                await Context.AddRangeAsync(entities);
                await SaveChangesAsync();
            }

            return new List<TEntity>(entities);
        }
        #endregion


        #region Update()
        public virtual TEntity Update<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity != null)
            {
                if (Context.Entry(entity).State == EntityState.Detached)
                {
                    Context.Update(entity);
                }

                SaveChanges();
            }

            return entity;
        }

        public virtual List<TEntity> Update<TEntity>(params TEntity[] entities) where TEntity : class
        {
            if (entities.Length > 0)
            {
                Context.UpdateRange(entities.Where(p => Context.Entry(p).State == EntityState.Detached));
                SaveChanges();
            }

            return new List<TEntity>(entities);
        }
        #endregion

        #region UpdateAsync()
        public virtual async Task<TEntity> UpdateAsync<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity != null)
            {
                if (Context.Entry(entity).State == EntityState.Detached)
                {
                    Context.Update(entity);
                }

                await SaveChangesAsync();
            }

            return entity;
        }

        public virtual async Task<List<TEntity>> UpdateAsync<TEntity>(params TEntity[] entities) where TEntity : class
        {
            if (entities.Length > 0)
            {
                Context.UpdateRange(entities.Where(p => Context.Entry(p).State == EntityState.Detached));
                await SaveChangesAsync();
            }

            return new List<TEntity>(entities);
        }
        #endregion


        #region Remove()
        public virtual TEntity Remove<TEntity>(int id) where TEntity : class
        {
            return Remove(Find<TEntity>(id));
        }

        public virtual TEntity Remove<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity != null)
            {
                Context.Remove(entity);
                SaveChanges();
            }

            return entity;
        }

        public virtual List<TEntity> Remove<TEntity>(params TEntity[] entities) where TEntity : class
        {
            if (entities.Length > 0)
            {
                Context.RemoveRange(entities);
                SaveChanges();
            }

            return new List<TEntity>(entities);
        }
        #endregion

        #region RemoveAsync()
        public virtual async Task<TEntity> RemoveAsync<TEntity>(int id) where TEntity : class
        {
            return await RemoveAsync(await FindAsync<TEntity>(id));
        }

        public virtual async Task<TEntity> RemoveAsync<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity != null)
            {
                Context.Remove(entity);
                await SaveChangesAsync();
            }

            return entity;
        }

        public virtual async Task<List<TEntity>> RemoveAsync<TEntity>(params TEntity[] entities) where TEntity : class
        {
            if (entities.Length > 0)
            {
                Context.RemoveRange(entities);
                await SaveChangesAsync();
            }

            return new List<TEntity>(entities);
        }
        #endregion


        #region SaveChanges()
        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return Context.SaveChanges(acceptAllChangesOnSuccess);
        }
        #endregion

        #region SaveChangesAsync()
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Context.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        #endregion
    }
}