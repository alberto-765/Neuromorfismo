using Microsoft.Extensions.Configuration;
using System.Text;

namespace WebMedicina.BackEnd.Data {
    public class DBSettings {


        public static string DbConnectionSting (IConfiguration config) {
            StringBuilder sb = new StringBuilder ();

            sb.Append($"Server={config["database:server"]};");
            sb.Append($"Port={config["database:port"]};");
            sb.Append($"DataBase={config["database:DataBaseName"]};");
            sb.Append($"Uid={config["database:user"]};");
            sb.Append($"Password={config["database:Password"]};");

           //if( config.GetSection<bool>(Key: "database: AllowUserVariables") == true) {
           //     sb.Append($"Allow User Variables=true;");
           // }

            return sb.ToString ();
        }
    }
}