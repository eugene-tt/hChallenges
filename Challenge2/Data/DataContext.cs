using System;
using SQLite;
using System.Threading.Tasks;
using System.IO;
using Challenge2.Data.Entities;

namespace Challenge2.Data
{
    public class DataContext
    {
        /// <summary>
        /// Shared DB connection
        /// </summary>
        private readonly SQLiteConnection _database;

        /// <summary>
        /// Access to a SQLite db must be serialized to avoid race conditions
        /// </summary>
        public readonly object SyncRoot = new object();

        /// <summary>
        /// A shortcut to the DB async connection
        /// </summary>
        public static SQLiteConnection Database => Instance.Value._database;

        /// <summary>
        /// A singleton providing access to the only instance of DB connection.
        /// </summary>
        public static Lazy<DataContext> Instance = new Lazy<DataContext>(() =>
        {
            // The connection and tables will be initialized only upon the first call to Instance
            CreateTables();

            // Return the new instance one time only
            return new DataContext();
        });


        private DataContext()
        {
            _database = new SQLiteConnection(DatabasePath);
        }

		private static string DatabasePath
        {
            get
            {
                var sqliteFilename = "DataCollection.db";
                string tempPath = Path.GetTempPath();
                var path = Path.Combine(tempPath, sqliteFilename);

                return path;
            }
        }

        /// <summary>
        /// Tables are created synchronously because they are needed from the very start
        /// where we don't have any async code yet
        /// </summary>
        private static void CreateTables()
        {
            var syncDb = new SQLiteConnection(DatabasePath);
            
            syncDb.CreateTable<Transport>();
            syncDb.CreateTable<Container>();
        }
    }
}

