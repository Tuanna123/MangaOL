using MangaCore.Sqlite.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace MangaCore.Sqlite.ViewModels
{
    public class DatabaseHelper
    {
        public static string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Database/Manga.sqlite");

        private static DatabaseHelper _DatabaseHelper;

        private DatabaseHelper()
        {
        }

        public static DatabaseHelper Instance()
        {
            if (DatabaseHelper._DatabaseHelper == null)
            {
                DatabaseHelper._DatabaseHelper = new DatabaseHelper();
            }
            return DatabaseHelper._DatabaseHelper;
        }

        public void createDatabase()
        {
            IsolatedStorageFile userStoreForApplication = IsolatedStorageFile.GetUserStoreForApplication();
            if (!userStoreForApplication.DirectoryExists("Database"))
            {
                userStoreForApplication.CreateDirectory("Database");
                using (SQLiteConnection sQLiteConnection = new SQLiteConnection(DatabaseHelper.dbpath))
                {
                    sQLiteConnection.CreateTable<SqlMangaHistory>(CreateFlags.None);
                    sQLiteConnection.CreateTable<SqlMangaFavorite>(CreateFlags.None);
                    sQLiteConnection.CreateTable<SqlChaperBookmask>(CreateFlags.None);
                    sQLiteConnection.CreateTable<SqlHistoryRead>(CreateFlags.None);
                    sQLiteConnection.CreateTable<SqlDownload>(CreateFlags.None);
                    sQLiteConnection.CreateTable<SqlDownLoadedImage>(CreateFlags.None);
                }
            }
            else
            {
                //string update = MangaCore.Utils.SaveAppSeting("Update", "1", true);
                //if (string.IsNullOrEmpty(update))
                //{
                //    var list = Select<SqlMangaFavorite>();
                //    using (SQLiteConnection sQLiteConnection = new SQLiteConnection(DatabaseHelper.dbpath))
                //    {
                //        sQLiteConnection.DropTable<SqlMangaFavorite>();
                //        sQLiteConnection.CreateTable<SqlMangaFavorite>(CreateFlags.None);
                //    }
                //    if (list.Count > 0)
                //    {
                //        InsertList<SqlMangaFavorite>(list);
                //    }
                //}
            }
        }

        public void Delete<T>()
        {
            using (SQLiteConnection sQLiteConnection = new SQLiteConnection(DatabaseHelper.dbpath, false))
            {
                sQLiteConnection.DeleteAll<T>();
            }
        }

        public void Delete<T>(object _object)
        {
            using (SQLiteConnection sQLiteConnection = new SQLiteConnection(DatabaseHelper.dbpath, false))
            {
                sQLiteConnection.Delete(_object);
            }
        }

        public void Insert<T>(object _object = null)
		{
			using (SQLiteConnection Connection = new SQLiteConnection(DatabaseHelper.dbpath, false))
			{
				Connection.RunInTransaction(delegate
				{
					Connection.Insert((T)((object)_object));
				});
			}
		}

        public void InsertList<T>(object _object = null)
		{
			
			using (SQLiteConnection Connection = new SQLiteConnection(DatabaseHelper.dbpath, false))
			{
				Connection.RunInTransaction(delegate
				{
					Connection.InsertAll((List<T>)_object);
				});
			}
		}

        public List<T> Select<T>() where T : class, new()
        {
            List<T> result;
            using (SQLiteConnection sQLiteConnection = new SQLiteConnection(DatabaseHelper.dbpath, false))
            {
                List<T> list = Enumerable.ToList<T>(sQLiteConnection.Table<T>());
                result = list;
            }
            return result;
        }

        public T Select<T>(Func<T, bool> pre = null) where T : class, new()
        {
            T result;
            using (SQLiteConnection sQLiteConnection = new SQLiteConnection(DatabaseHelper.dbpath, false))
            {
                //Enumerable.FirstOrDefault<T>(Enumerable.ToList<T>(sQLiteConnection.Table<T>()), pre);
                T t = sQLiteConnection.Table<T>().FirstOrDefault(pre);
                result = t;
            }
            return result;
        }

        public void Update<T>(object _object = null) where T : class, new()
		{
			using (SQLiteConnection Connection = new SQLiteConnection(DatabaseHelper.dbpath, false))
			{
				Connection.RunInTransaction(delegate
				{
					Connection.Update((T)((object)_object));
				});
			}
		}
    }
}
