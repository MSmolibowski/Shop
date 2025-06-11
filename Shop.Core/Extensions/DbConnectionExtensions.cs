using System.Data.Common;
using System.Data;

namespace Shop.Core.Extensions;
public static class DbConnectionExtensions
{
    public static async Task EnsureOpenAsync(this IDbConnection connection)
    {
        if (connection.State != ConnectionState.Open)
        {
            if (connection is DbConnection dbConn)
            {
                await dbConn.OpenAsync();
            }
            else
            {
                connection.Open();
            }
        }
    }
}