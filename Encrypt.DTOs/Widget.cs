using System;
using SQLite;

namespace Encrypt.DTOs
{
    [Table("Widget")]
    public class Widget
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
