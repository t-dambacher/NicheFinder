using NicheFinder.DataBase.Entities;
using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;

namespace NicheFinder.DataBase
{
    public class NicheContext : DbContext, IDbContext
    {
        private static String GetDbConnectionString()
        {
            String dbFile = Environment.CurrentDirectory;
            if (dbFile.Contains("/bin/Debug") || dbFile.Contains("/bin/Release") || dbFile.Contains(@"\bin\Debug") || dbFile.Contains(@"\bin\Release"))
            {
                dbFile = new DirectoryInfo(dbFile).Parent.Parent.Parent.FullName;
                dbFile = Path.Combine(dbFile, "Data");
            }

            dbFile = Path.Combine(dbFile, "finder.sqlite");

            return new SQLiteConnectionStringBuilder() { DataSource = dbFile }.ToString();
        }

        private static String _dbConnectionString = GetDbConnectionString();


        public static IDbContext Create()
        {
            return new NicheContext(new SQLiteConnection(_dbConnectionString));
        }

        static NicheContext()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<NicheContext>());
        }

        /// <summary>
        /// Constructeur par défaut privé
        /// </summary>
        private NicheContext(DbConnection connection)
            : base(connection, true)
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public IQueryable<Noun> Nouns { get { return this.Set<Noun>(); } }
        public IQueryable<NAF5> NAF5s { get { return this.Set<NAF5>(); } }
        public IQueryable<NAF5Description> NAF5Descriptions { get { return this.Set<NAF5Description>(); } }
        public IQueryable<NAF5Description_Noun> NAF5Descriptions_Nouns { get { return this.Set<NAF5Description_Noun>(); } }

        public void Save<TDbEntity>(TDbEntity entity)
            where TDbEntity : class, IDbEntity
        {
            IDbSet<TDbEntity> set = this.Set<TDbEntity>();
            if (entity.IsNew)
                set.Add(entity);
            else
                set.Attach(entity);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
