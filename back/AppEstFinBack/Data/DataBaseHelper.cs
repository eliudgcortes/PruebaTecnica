using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace AppEstFin.Data;

public class DataBaseHelper
{
    private readonly string _connectionString;

    public DataBaseHelper(IConfiguration configuration)
    {
        // Obtenemos la cadena de conexion desde appsettings.json.
        _connectionString = configuration.GetConnectionString("DefaultConnection")
		// Si no existe, mostramos un error claro, agregando esto como una ligera mejora como senial.
            ?? throw new InvalidOperationException("No se encontró la cadena de conexión 'DefaultConnection'.");
    }

    public SqlConnection GetSqlConnection()
    {
        try
        {
            // Creamos la conexion con la base de datos.
            return new SqlConnection(_connectionString);
        }
        catch (Exception ex)
        {
            // Mensaje mostrando que ocurrio un error.
            throw new Exception("Error al conectar con la base de datos.", ex);
        }
    }
}
