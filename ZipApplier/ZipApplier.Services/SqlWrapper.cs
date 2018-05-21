using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZipApplier.Services
{
    public class SqlWrapper
    {
        public SqlCommand Wrapper(string proc, SqlConnection con)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = proc;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            return cmd;
        }
    }
}
