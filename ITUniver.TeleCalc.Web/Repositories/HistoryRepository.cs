using ITUniver.TeleCalc.Web.Interfaces;
using ITUniver.TeleCalc.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ITUniver.TeleCalc.Web.Repositories
{
    public class HistoryRepository : BaseRepository<OperationHistory>
    {
        string connectionString = "";

        public HistoryRepository(string connectionString) : 
            base(connectionString)
        {
        }

        internal override string GetSelectQuery()
        {
            return @"SELECT his.Id, us.Name Initiator, op.Name Operation, his.Args, 
                    his.Result, his.CalcDate, his.Time 
                    FROM [dbo].[History] as his, [User] as us, [Operation] as op 
                    WHERE his.Initiator = us.Id AND his.Operation = op.Id;";
        }

        internal override string GetInsertQuery()
        {
            return @"INSERT INTO dbo.History (Initiator, Operation, Args, Result, CalcDate, Time) VALUES (@initiator, @operation, @args, @result, @calcDate, @time)";
        }

    }
}