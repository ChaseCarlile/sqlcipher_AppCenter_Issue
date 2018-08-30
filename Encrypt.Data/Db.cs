using System;
using System.Collections.Generic;
using System.Text;

using System.Reflection;

using System.Threading.Tasks;
using System.Linq;
using SQLite;

using System.IO;

namespace Encrypt.Data
{
    public class Db
    {
        static readonly object _locker = new Object();

        string FileName { get; } = "user.db3";
        string _key = "reallyBadPassword";


        static Db _Default;
        public static Db Default
        {
            get
            {
                if (_Default == null)
                {
                    lock (_locker)
                    {
                        if (_Default == null)
                        {
                            _Default = new Db();

                        }
                    }
                }
                return _Default;
            }
        }

        public SQLiteConnection Connection { get; private set; }


        private Db()
        {

            CreateTablesFromModels();
            Connection = GetOpenConnection();
        }


        string Directory
        {
            get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)); }
        }


        string FullFileName
        {
            get { return Path.Combine(Directory, FileName); }
        }

        SQLiteConnection GetOpenConnection()
        {
            var connection = new SQLiteConnection(FullFileName, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.FullMutex, true);
            if (!string.IsNullOrEmpty(_key))
            {
                connection.Query<int>($"PRAGMA key='{_key}'");
            }
            return connection;
        }

        void CreateTablesFromModels()
        {
            var dbConn = new SQLiteConnection(FullFileName);

            if (!string.IsNullOrEmpty(_key))
            {
                dbConn.Query<int>($"PRAGMA key='{_key}'");
            }

            dbConn.CreateTable(typeof(DTOs.Widget), CreateFlags.None);
        }
    }
}