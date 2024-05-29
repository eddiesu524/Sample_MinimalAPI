using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace Sample_MinimalAPI.DataAccesses
{
    /// <summary>
    /// 資料庫存取介面
    /// </summary> 
    public interface IRepository
    {
        string ConnectionString { get; }

        IDbConnection Connection { get; }
    }
}