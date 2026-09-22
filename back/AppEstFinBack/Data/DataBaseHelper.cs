using Microsoft.Data.SqlClient;

namespace AppEstFin.Data;

public class DataBaseHelper
{
    private readonly string _connectionString;

    public DataBaseHelper(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public SqlConnection GetSqlConnection()
    {
        try
        {
            return new SqlConnection(_connectionString);
        }
        catch(Exception ex)
        {
            //Registro de error
            throw new Exception("Error al conexion de base de datos", ex);
        }
    }
}
