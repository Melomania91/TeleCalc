﻿using ITUniver.TeleCalc.Web.Interfaces;
using ITUniver.TeleCalc.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ITUniver.TeleCalc.Web.Repositories
{
    public class OperationRepository : BaseRepository<OperationModel>
    {
        string connectionString = "";

        public OperationRepository(string connectionString) : base(connectionString)
        {          
        }

        internal override string GetSelectQuery()
        {
            return @"SELECT * FROM [Operation]";
        }

        internal override string GetInsertQuery()
        {
            return @"INSERT INTO dbo.Operation (Name, Owner) VALUES (@name, @owner)";
        }

        public OperationModel LoadByName(string name)
        {
            var opers = Find($"[Name] = N'{name}'");

            return opers.FirstOrDefault();
        }
    }
}