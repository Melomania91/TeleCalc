using ITUniver.TeleCalc.Web.Interfaces;
using ITUniver.TeleCalc.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ITUniver.TeleCalc.Web.Repositories
{
    public class BaseRepository<T> : IRepository<T>
        where T : IEntity
    {
        string connectionString = "";

        public BaseRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public T Clone(T obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(T obj)
        {
            throw new NotImplementedException();
        }

        internal virtual string GetSelectQuery()
        {
            return "";
        }

        internal virtual T Map(SqlDataReader reader)
        {
            var properties = typeof(T).GetProperties();

            var obj = Activator.CreateInstance<T>();
            foreach (var property in properties)
            {
                try
                {
                    var ind = reader.GetOrdinal(property.Name);
                    if (!reader.IsDBNull(ind))
                    {
                        property.SetValue(obj, reader[property.Name]);
                    }
                }
                catch
                {
                    

                }
            }

            return obj;
        }

        public IEnumerable<T> Find(string condition)
        {
            var items = new List<T>();
            string queryString = GetSelectQuery();

            if (!string.IsNullOrWhiteSpace(condition))
            {
                if (queryString.ToUpper().Contains("WHERE"))
                {
                    if (queryString.ToUpper().Contains("ORDER"))
                    {
                        var firstPart = queryString.Substring(0, (queryString.Length - queryString.ToUpper().IndexOf("ORDER") - 1));
                        var secondPart = queryString.Substring(queryString.ToUpper().IndexOf("ORDER"));
                        queryString = firstPart + $" AND {condition} " + secondPart;
                    }
                    else
                        queryString += $" AND {condition} ";
                }
                else
                    queryString += $" WHERE {condition} ";
            }

            using (var connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                // Call Read before accessing data.
                while (reader.Read())
                {
                    items.Add(Map(reader));
                }

                // Call Close when done reading.
                reader.Close();
                return items;
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// Получение выборки из БД
        /// </summary>
        /// <param name="condition">Условие выборки</param>
        /// <param name="query">true - если передаётся полный запрос, false - если необходимо использовать запрос, определённый для типа</param>
        /// <returns></returns>
        public IEnumerable<T> Find(string condition, bool fullQuery)
        {
            var items = new List<T>();
            string queryString = GetSelectQuery();

            if (fullQuery)
            {
                queryString = condition;
            }

            else
            {
                if (!string.IsNullOrWhiteSpace(condition))
                {
                    if (queryString.ToUpper().Contains("WHERE"))
                    {
                        if (queryString.ToUpper().Contains("ORDER"))
                        {
                            var firstPart = queryString.Substring(0, (queryString.Length - queryString.ToUpper().IndexOf("ORDER") - 1));
                            var secondPart = queryString.Substring(queryString.ToUpper().IndexOf("ORDER"));
                            queryString = firstPart + $" AND {condition} " + secondPart;
                        }
                        else
                            queryString += $" AND {condition} ";
                    }
                    else
                        queryString += $" WHERE {condition} ";
                }
            }

            using (var connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                // Call Read before accessing data.
                while (reader.Read())
                {
                    items.Add(Map(reader));
                }

                // Call Close when done reading.
                reader.Close();
                return items;
            }

            throw new NotImplementedException();
        }


        public T Read(Guid id)
        {
            throw new NotImplementedException();
        }

        internal virtual string GetInsertQuery()
        {
            return "";
        }

        internal SqlParameter[] InverseMap(object obj)
        {
            var properties = typeof(T).GetProperties();
            var parameters = new List<SqlParameter>();

            foreach (var property in properties)
            {
                var val = property.GetValue(obj);
                parameters.Add(new SqlParameter($"@{property.Name}", val));
            }

            return parameters.ToArray();
        }

        public bool Save(T obj)
        {
            if (obj.Id == 0)
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    string queryString = GetInsertQuery();

                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddRange(InverseMap(obj));
                    connection.Open();

                    return command.ExecuteNonQuery() > 0;
                }

            }
            return false;
        }


    }
}