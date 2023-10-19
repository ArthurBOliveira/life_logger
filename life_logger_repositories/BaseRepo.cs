using System.Configuration;
using System.Data;
using System.Reflection;

using Dapper;

using life_logger_models;

using MySql.Data.MySqlClient;

namespace life_logger_repositories
{
    public abstract class BaseRepo<T> : IBaseRepo<T> where T : BaseModel
    {
        protected readonly string connectionString;
        protected readonly string tableName;

        public BaseRepo()
        {
            connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            tableName = typeof(T).Name; // Get the name of the class as the table name
        }

        public virtual T Create(T item)
        {
            Type entityType = typeof(T);
            PropertyInfo[] properties = entityType.GetProperties();
            string columnNames = string.Join(", ", properties.Select(p => p.Name));

            using IDbConnection db = new MySqlConnection(connectionString);
            var sql = $"INSERT INTO {tableName} ({columnNames}) VALUES (@{string.Join(", @", properties.Select(p => p.Name))})";
            var id = db.Query<int>(sql, item).FirstOrDefault();
            return item;
        }

        public virtual T Select(Guid id)
        {
            using IDbConnection db = new MySqlConnection(connectionString);
            var sql = $"SELECT * FROM {tableName} WHERE Id = @Id AND Active = 1;";
            return db.QuerySingleOrDefault<T>(sql, new { Id = id });
        }

        public virtual IEnumerable<T> SelectMany()
        {
            using IDbConnection db = new MySqlConnection(connectionString);
            var sql = $"SELECT * FROM {tableName} WHERE Active = 1;";
            return db.Query<T>(sql);
        }

        public virtual IEnumerable<T> SelectFilter(string filter)
        {
            using IDbConnection db = new MySqlConnection(connectionString);
            var sql = $"SELECT * FROM {tableName} WHERE Active = 1 AND {filter}";
            return db.Query<T>(sql);
        }

        public virtual T Update(T item)
        {
            Type entityType = typeof(T);
            PropertyInfo[] properties = entityType.GetProperties();
            string columnNames = string.Join(", ", properties.Select(p => p.Name));

            using IDbConnection db = new MySqlConnection(connectionString);
            string setClause = string.Join(", ", properties.Select(p => $"{p.Name} = @{p.Name}"));

            string updateScript = $"UPDATE {tableName} SET {setClause} WHERE Id = @Id";
            db.Execute(updateScript, item);
            return item;
        }

        public virtual void Delete(Guid id)
        {
            using IDbConnection db = new MySqlConnection(connectionString);
            var sql = $"UPDATE {tableName} SET Active = 0 WHERE Id = @Id;";
            db.Execute(sql, new { Id = id });
        }
    }
}