using NicheFinder.DataBase.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicheFinder.DataBase.Configuration
{
    public class IDbEntityConfiguration<TDbEntity> : EntityTypeConfiguration<TDbEntity>
        where TDbEntity : class, IDbEntity
    {
        protected IDbEntityConfiguration()
        {
            this.Ignore(t => t.IsNew);
            this.ToTable(typeof(TDbEntity).Name);
        }
    }

    public class EntityConfiguration<TEntity> : IDbEntityConfiguration<TEntity>
        where TEntity : Entity
    {
        protected EntityConfiguration()
        {
            this.HasKey(t => t.ID);
            this.Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
