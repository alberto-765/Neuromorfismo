using Microsoft.Extensions.Configuration;
using System.Text;

namespace WebMedicina.BackEnd.Data {
    public class DBSettings {


        public static string DBConnectionSting(IConfiguration config) {
            // Bindeamos uno a uno los valores del config a los de nuestra clase de configuracion
            DBSettingsModel dbConfig = new();
            config.Bind(key: "database", config);

            // Asignamos los valroes de la configuracion
            StringBuilder sb = new();
            sb.Append($"Server={dbConfig.Server};");
            sb.Append($"Port={dbConfig.Port};");
            sb.Append($"DataBase={dbConfig.DataBase};");
            sb.Append($"Uid={dbConfig.User};");
            sb.Append($"Password={dbConfig.Password};");

            // devolvemos el string builder
            return sb.ToString();
        }
    }

    public class DBSettingsModel {
        public string Server { get; set; }
        public int Port { get; set; }
        public string DataBase { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public bool AllowUserVariables { get; set; }

    }
}