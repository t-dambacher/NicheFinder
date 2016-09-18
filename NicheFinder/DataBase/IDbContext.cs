using NicheFinder.DataBase.Entities;
using System;
using System.Linq;

namespace NicheFinder.DataBase
{
    /// <summary>
    /// Interface public du DbContext utilisé par l'application
    /// </summary>
    public interface IDbContext : IDisposable
    {
        IQueryable<Noun> Nouns { get; }
        IQueryable<NAF5> NAF5s { get; }
        IQueryable<NAF5Description> NAF5Descriptions { get; }
        IQueryable<NAF5Description_Noun> NAF5Descriptions_Nouns { get; }

        void Save<TDbEntity>(TDbEntity entity)
            where TDbEntity : class, IDbEntity;
        Int32 SaveChanges();
    }
}
