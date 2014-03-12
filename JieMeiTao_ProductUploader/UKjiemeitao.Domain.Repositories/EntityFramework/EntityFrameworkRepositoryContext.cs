﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Threading;

namespace UKjiemeitao.Domain.Repositories.EntityFramework
{
    public class EntityFrameworkRepositoryContext : RepositoryContext, IEntityFrameworkRepositoryContext
    {
        private readonly ThreadLocal<CatalogDbContext> localCtx = new ThreadLocal<CatalogDbContext>(() => new CatalogDbContext());

        public override void RegisterDeleted<TAggregateRoot>(TAggregateRoot obj)
        {
            localCtx.Value.Entry<TAggregateRoot>(obj).State = System.Data.EntityState.Deleted;
            Committed = false;
        }

        public override void RegisterModified<TAggregateRoot>(TAggregateRoot obj)
        {
            localCtx.Value.Entry<TAggregateRoot>(obj).State = System.Data.EntityState.Modified;
            Committed = false;
        }

        public override void RegisterNew<TAggregateRoot>(TAggregateRoot obj)
        {
            localCtx.Value.Entry<TAggregateRoot>(obj).State = System.Data.EntityState.Added;
            Committed = false;
        }

        public override int RegisterExecuteSqlCommand(string sql, params object[] parameters)
        {
            int v= localCtx.Value.Database.ExecuteSqlCommand(sql, parameters);
            return v;
        }
        
        public override void Commit()
        {
            if (!Committed)
            {
                localCtx.Value.SaveChanges();
                Committed = true;
            }
        }

        public override void Rollback()
        {
            Committed = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!Committed)
                    Commit();
                localCtx.Value.Dispose();
                localCtx.Dispose();
                base.Dispose(disposing);
            }
        }

        #region IEntityFrameworkRepositoryContext Members

        public DbContext Context
        {
            get { return localCtx.Value; }
        }

        #endregion
    }
}
