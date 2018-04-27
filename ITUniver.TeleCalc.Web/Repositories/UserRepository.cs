using ITUniver.TeleCalc.Web.Interfaces;
using ITUniver.TeleCalc.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ITUniver.TeleCalc.Web.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        string connectionString = "";

        public UserRepository(string connectionString) : base(connectionString)
        {          
        }

        internal override string GetSelectQuery()
        {
            return @"SELECT Id, Name, Login, Password, Email, BirthDate, Sex, Status FROM [User]";
        }

        internal override string GetInsertQuery()
        {
            return @"INSERT INTO dbo.[User] (Name, Login, Password, Email, BirthDate, Sex, Status) VALUES (@name, @login, @password, @email, @birthDate, @sex, @status)";
        }

        internal User GetUserByLogin(string login)
        {
            var users = Find($"[Login] = N'{login}'");

            return users.FirstOrDefault();
        }
    }
}