using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using iimWebAppBot.Models;
using iimWebAppBot.Dialogs;

namespace iimWebAppBot.DbInteraction
{
    public class CallRepo
    {
        private readonly IDbConnection _connection;
        private readonly ILogger _log;
        private readonly LogBuilder _logBuilder;




        public CallRepo(ILogger<MainDialog> logger, IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("IIMSqlDb"));
            _log = logger;
            _logBuilder = new LogBuilder();

        }

        public MaterialByIdDb GetMaterialById(string strMaterialId)
        {
            try
            {
                //_log.LogInformation(_logBuilder.MethodName() + " method execution starts");

                //_log.LogDebug("strSupplierName : " + strSupplierName);


                var sqlstatement = "INV_BOT_GET_MATERIAL_DETAILS_BYID";
                var results = _connection.Query<MaterialByIdDb>(
                sqlstatement,
                new
                {
                    @Material = strMaterialId
                },
               commandType: CommandType.StoredProcedure,
               commandTimeout: 0);

                _log.LogDebug("ROW COUNT : " + results.Count());

                var dbOutput = results.SingleOrDefault();

                return dbOutput;
            }
            catch (Exception e)
            {

                _log.LogError(_logBuilder.ErrorsFormation(e));

                return null;
            }

        }
    }
}
