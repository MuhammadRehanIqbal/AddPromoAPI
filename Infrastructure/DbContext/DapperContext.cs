using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System.Data;

namespace Infrastructure.DbContext
{
    public class DapperContext
    {
        private readonly DapperSettings _dapperSettings;

        public DapperContext(IOptions<DapperSettings> DapperSettings)
        {
            _dapperSettings = DapperSettings.Value;
        }
        public IDbConnection CreateConnection() => new MySqlConnection(_dapperSettings.MySql);
         

    }
}
