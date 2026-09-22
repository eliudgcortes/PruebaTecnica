using AppEstFin.DTO;
using AppEstFin.Models;

namespace AppEstFin.Repository;

public interface IEstimacionRepository
{
    Task<List<sp_CalcularTotalAPagarPorTarjeta>> CalcularTotalAPagarPorTarjeta(int id_usuario);
    Task InsertarGasto(decimal monto, string descripcion, DateTime fechaMovimiento, string categoriaGasto, int idTarjeta, int idUsuario);
    Task<List<sp_ObtenerGastosPorPeriodo>> ObtenerGastoPorPeriodo(int id_usuario, int id_tarjeta, int? mes, int? año);
    Task ActualizarGasto();
    Task EliminarGasto(int idGasto);
    Task<GastoDTO?> ObtenerGasto(int idGasto);
    Task<GastoDTO?> ObtenerUltimoGasto();
}
