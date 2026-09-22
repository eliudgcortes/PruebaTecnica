using AppEstFin.Models;
using AppEstFin.DTO;

namespace AppEstFin.Services;

public interface IEstimacionService
{
    Task<List<sp_CalcularTotalAPagarPorTarjeta>> ObtenerDatosConsumo(int id_usuario);

    Task<bool> InsertarGasto(
        decimal monto,
        string descripcion,
        DateTime fechaMovimiento,
        string categoriaGasto,
        int idTarjeta,
        int idUsuario);

    Task<List<sp_ObtenerGastosPorPeriodo>> ObtenerConsumoPorPeriodo(
        int id_usuario,
        int id_tarjeta,
        int? mes,
        int? anio);

    Task ActualizarGasto();

    Task EliminarGasto();

    Task<GastoDTO?> ObtenerGasto(int idGasto);
}
