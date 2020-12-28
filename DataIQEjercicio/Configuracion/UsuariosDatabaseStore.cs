using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataIQEjercicio.Configuracion
{
    // 1 - Clase con la Interface para tomar los datos de conexion del appsettings.json
    public class UsuariosDatabaseStore : IUsuariosDatabaseStore
    {
        public string UsuariosCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IUsuariosDatabaseStore
    {
        string UsuariosCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
