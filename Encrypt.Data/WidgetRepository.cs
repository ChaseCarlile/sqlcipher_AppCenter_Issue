using System;
using Encrypt.DTOs;

namespace Encrypt.Data
{
    public class WidgetRepository
    {
        public int GetWidgetCount()
        {
            var db = Db.Default.Connection;
            return db.Table<Widget>().Count();
        }

        public bool AddWidget(Widget widget)
        {
            var db = Db.Default.Connection;
            db.InsertOrReplace(widget);
            return true;
        }
    }
}