using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL.SQL
{
    public class Repository : IRepository
    {

        private string connectionString = @"Data Source=DESKTOP-04N6AF2\SQLEXPRESS01; User Id=new_sa; Password=1234; Initial Catalog=AvanadeLibrary; Integrated Security=True; TrustServerCertificate=Yes";

        public Repository() 
        {
        }

        public bool Connect()
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                return true;
            }
            catch
            {
                return false;
            }
            

        }

        public void Create<TEntity>(TEntity entity) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public void Delete<TEntity>(int entityId) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> FindAll<TEntity>() where TEntity : class, new()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string tableName = typeof(TEntity).Name + "s";
                using (SqlCommand command = new SqlCommand($"SELECT * FROM {tableName}", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<TEntity> result = new List<TEntity>();
                        while (reader.Read())
                        {
                            TEntity entity = new TEntity();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                string columnName = reader.GetName(i);
                                PropertyInfo property = entity.GetType().GetProperty(columnName);
                                if (property != null)
                                {
                                    object value = reader.IsDBNull(i) ? null : reader[i];
                                    property.SetValue(entity, value);
                                }
                            }
                            result.Add(entity);
                        }
                        return result;
                    }
                }

            }
        }

        public TEntity? FindById<TEntity>(int entityId) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            //Console.WriteLine(connectionString);
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }
    }
}
