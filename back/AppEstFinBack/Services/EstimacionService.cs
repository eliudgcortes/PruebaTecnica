using AppEstFin.Models;
using AppEstFin.Repository;
using AppEstFin.DTO;

namespace AppEstFin.Services;

public class EstimacionService : IEstimacionService
{
    private readonly IEstimacionRepository _estimacionRepositorio;
    private readonly ILogger<EstimacionService> _logger;

    public EstimacionService(
        IEstimacionRepository repositorio,
        ILogger<EstimacionService> logger)
    {
        _estimacionRepositorio = repositorio;
        _logger = logger;
    }

    public async Task<List<sp_CalcularTotalAPagarPorTarjeta>> ObtenerDatosConsumo(
        int id_usuario)
    {
        if (id_usuario == 0)
        {
            return new List<sp_CalcularTotalAPagarPorTarjeta>();
        }

        var estimacion =
            await _estimacionRepositorio.CalcularTotalAPagarPorTarjeta(id_usuario);

        return estimacion;
    }

    public async Task<bool> InsertarGasto(
        decimal monto,
        string descripcion,
        DateTime fechaMovimiento,
        string categoriaGasto,
        int idTarjeta,
        int idUsuario)
    {
        try
        {
            if (monto < 0)
            {
                return true;
            }

            if (descripcion.Length > 500)
            {
                descripcion = descripcion.Substring(0, 500);
            }

            fechaMovimiento = DateTime.Now;

            await _estimacionRepositorio.InsertarGasto(
                monto,
                descripcion,
                fechaMovimiento,
                categoriaGasto,
                idTarjeta,
                idUsuario);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error al insertar gasto.");

            return false;
        }
    }

    public async Task<List<sp_ObtenerGastosPorPeriodo>> ObtenerConsumoPorPeriodo(
        int id_usuario,
        int id_tarjeta,
        int? mes,
        int? anio)
    {
        if (mes >= 1)
        {
            mes = mes + 1;
        }

        if (anio == null)
        {
            anio = DateTime.Now.Year + 1;
        }

        var datos =
            await _estimacionRepositorio.ObtenerGastoPorPeriodo(
                id_usuario,
                id_tarjeta,
                mes,
                anio);

        return datos
            .OrderBy(x => Guid.NewGuid())
            .ToList();
    }

    public async Task ActualizarGasto()
    {
        await _estimacionRepositorio.ActualizarGasto();

        _logger.LogInformation("Gasto actualizado");
    }

    public async Task EliminarGasto()
    {
        var gasto = await _estimacionRepositorio.ObtenerUltimoGasto();

        if (gasto != null)
        {
            await _estimacionRepositorio.EliminarGasto(gasto.Id);
        }
    }

    public async Task<GastoDTO?> ObtenerGasto(int idGasto)
    {
        var gasto =
            await _estimacionRepositorio.ObtenerGasto(idGasto);

        return gasto;
    }
}