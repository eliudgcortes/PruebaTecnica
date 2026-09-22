using AppEstFin.DTO;
using System.Data;
using AppEstFin.Data;
using AppEstFin.Models;
using Microsoft.Data.SqlClient;

namespace AppEstFin.Repository;

public class EstimacionRepository : IEstimacionRepository
{
    private readonly ILogger<EstimacionRepository> _logger;
    private readonly DataBaseHelper _databaseHelper;

    public EstimacionRepository(ILogger<EstimacionRepository> logger, DataBaseHelper databasehelper)
    {
        _logger = logger;
        _databaseHelper = databasehelper;
    }

    public async Task<List<sp_CalcularTotalAPagarPorTarjeta>> CalcularTotalAPagarPorTarjeta(int id_usuario)
    {
        List<sp_CalcularTotalAPagarPorTarjeta> estimacionMes = new List<sp_CalcularTotalAPagarPorTarjeta>();
        try
        {
            using (SqlConnection connection = _databaseHelper.GetSqlConnection())
            {
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_CalcularTotalAPagarPorTarjeta", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_usuario", (object)id_usuario ?? DBNull.Value);
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        estimacionMes.Add(new sp_CalcularTotalAPagarPorTarjeta
                        {
                            id_tarjeta = (int)reader["id_tarjeta"],
                            nombre_tarjeta = (string)reader["nombre_tarjeta"],
                            TotalGastos = reader["TotalGastos"] != DBNull.Value ? (decimal)reader["TotalGastos"] : 0,
                            TotalCargosFijos = reader["TotalCargosFijos"] != DBNull.Value ? (decimal)reader["TotalCargosFijos"] : 0,
                            TotalAPagar = reader["TotalAPagar"] != DBNull.Value ? (decimal)reader["TotalAPagar"] : 0,
                        });
                    }

                    reader.Close();
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener datos de sp_CalcularTotalAPagarPorTarjeta");
            throw;
        }
        return estimacionMes;
    }

    public async Task InsertarGasto(decimal monto, string descripcion, DateTime fechaMovimiento, string categoriaGasto, int idTarjeta, int idUsuario)
    {
        using (SqlConnection connection = _databaseHelper.GetSqlConnection())
        {
            using (var command = new SqlCommand("[dbo].[sp_InsertarGasto]", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Monto", monto);
                command.Parameters.AddWithValue("@Descripcion", (object)descripcion ?? DBNull.Value);
                command.Parameters.AddWithValue("@FechaMovimiento", fechaMovimiento);
                command.Parameters.AddWithValue("@CategoriaGasto", categoriaGasto);
                command.Parameters.AddWithValue("@IdTarjeta", (object)idTarjeta ?? DBNull.Value);
                command.Parameters.AddWithValue("@IdUsuario", (object)idUsuario ?? DBNull.Value);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task<List<sp_ObtenerGastosPorPeriodo>> ObtenerGastoPorPeriodo(int id_usuario, int id_tarjeta, int? mes, int? año)
    {
        List<sp_ObtenerGastosPorPeriodo> datosGasto = new List<sp_ObtenerGastosPorPeriodo>();
        try
        {
            using (SqlConnection connection = _databaseHelper.GetSqlConnection())
            {
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_ObtenerGastosPorPeriodo", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_usuario", (object)id_usuario ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@id_tarjeta", (object)id_tarjeta ?? DBNull.Value);
                if (mes >= 1)
                {
                    cmd.Parameters.AddWithValue("@mes", (object)mes ?? DBNull.Value);
                }
                if (año >= 1)
                {
                    cmd.Parameters.AddWithValue("@año", (object)año ?? DBNull.Value);
                }
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        datosGasto.Add(new sp_ObtenerGastosPorPeriodo
                        {
                            id_gasto = (int)reader["id_gasto"],
                            monto = reader["monto"] != DBNull.Value ? (decimal)reader["monto"] : 0,
                            descripcion = (string)reader["descripcion"],
                            fecha_movimiento = (DateTime)reader["fecha_movimiento"],
                            categoria_gasto = reader["categoria_gasto"] != DBNull.Value ? (string)reader["categoria_gasto"] : "",
                            id_tarjeta = reader["id_tarjeta"] != DBNull.Value ? (int)reader["id_tarjeta"] : 0,
                            nombre_tarjeta = (string)reader["nombre_tarjeta"]
                        });
                    }

                    reader.Close();
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener datos de sp_ObtenerGastosPorPeriodo");
            throw;
        }
        return datosGasto;
    }

    public async Task<GastoDTO?> ObtenerGasto(int idGasto)
    {
        using SqlConnection connection = _databaseHelper.GetSqlConnection();

        await connection.OpenAsync();

        SqlCommand cmd = new SqlCommand("sp_ObtenerGasto", connection);

        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@id_gasto", idGasto);

        using SqlDataReader reader = await cmd.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            return new GastoDTO
            {
                Id = (int)reader["id_gasto"],
                Monto = (decimal)reader["monto"],
                Descripcion = (string)reader["descripcion"],
                CategoriaGasto = (string)reader["categoria_gasto"],
                FechaMovimiento = (DateTime)reader["fecha_movimiento"],
                IdTarjeta = (int)reader["id_tarjeta"],
                IdUsuario = (int)reader["id_usuario"]
            };
        }

        return null;
    }

    public async Task<GastoDTO?> ObtenerUltimoGasto()
    {
        using SqlConnection connection = _databaseHelper.GetSqlConnection();

        await connection.OpenAsync();

        SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM Gasto ORDER BY id_gasto DESC", connection);

        using SqlDataReader reader = await cmd.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            return new GastoDTO
            {
                Id = (int)reader["id_gasto"],
                Monto = (decimal)reader["monto"],
                Descripcion = (string)reader["descripcion"],
                CategoriaGasto = (string)reader["categoria_gasto"],
                FechaMovimiento = (DateTime)reader["fecha_movimiento"],
                IdTarjeta = (int)reader["id_tarjeta"],
                IdUsuario = (int)reader["id_usuario"]
            };
        }

        return null;
    }
    public async Task ActualizarGasto()
    {
        using SqlConnection connection = _databaseHelper.GetSqlConnection();

        await connection.OpenAsync();

        SqlCommand command = new SqlCommand("UPDATE Gasto SET descripcion='Actualizado'", connection);

        await command.ExecuteNonQueryAsync();
    }

    public async Task EliminarGasto(int idGasto)
    {
        using SqlConnection connection = _databaseHelper.GetSqlConnection();

        await connection.OpenAsync();

        SqlCommand command = new SqlCommand("DELETE FROM Gasto WHERE id_gasto = @id", connection);

        command.Parameters.AddWithValue("@id", idGasto);

        await command.ExecuteNonQueryAsync();
    }

}
