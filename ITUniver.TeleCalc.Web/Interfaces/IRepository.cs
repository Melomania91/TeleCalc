﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITUniver.TeleCalc.Web.Interfaces
{
    public interface IEntity
    {
        int Id { get; set; }
    }
    /// <summary>
    /// Хранилище данных
    /// </summary>
    public interface IRepository<T> where T : IEntity
    {
        T Read(Guid id);

        bool Save(T obj);

        T Clone(T obj);

        bool Delete(T obj);

        IEnumerable<T> Find(string condition);
    }
}
