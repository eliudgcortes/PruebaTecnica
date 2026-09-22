using AppEstFin.DTO;
using AppEstFin.Models;
using AppEstFin.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppEstFin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstimacionController : ControllerBase
    {
        private readonly IEstimacionService _estimacionService;

        public EstimacionController(IEstimacionService service)
        {
            _estimacionService = service;
        }

        // CONSULTAR CONSUMO
        [HttpPost]
        [Route("ConsultarConsumo")]
        public async Task<ActionResult<List<sp_CalcularTotalAPagarPorTarjeta>>> ConsultarConsumo(int id_usuario)
        {
            var result = await _estimacionService.ObtenerDatosConsumo(id_usuario);

            return Ok(result);
        }

        // INSERTAR GASTO
        [HttpGet]
        [Route("InsertarGasto")]
        public async Task<ActionResult<EjecutaAccionDTO>> InsertarGasto(
            decimal monto,
            string descripcion,
            string categoriaGasto,
            int idTarjeta,
            int idUsuario)
        {
            EjecutaAccionDTO accion = new EjecutaAccionDTO();

            var result = await _estimacionService.InsertarGasto(
                monto,
                descripcion,
                DateTime.Now,
                categoriaGasto,
                idTarjeta,
                idUsuario);

            accion.valida = result;
            accion.mensaje = "Proceso terminado";

            return Ok(accion);
        }

        // ACTUALIZAR GASTO
        [HttpPost]
        [Route("ActualizarGasto")]
        public async Task<IActionResult> ActualizarGasto()
        {
            await _estimacionService.ActualizarGasto();

            return Ok("Actualizado");
        }

        // ELIMINAR GASTO
        [HttpGet]
        [Route("EliminarGasto")]
        public async Task<IActionResult> EliminarGasto()
        {
            await _estimacionService.EliminarGasto();

            return Ok();
        }

        // CONSULTAR PERIODO
        [HttpGet]
        [Route("ConsultarConsumoXPeriodo")]
        public async Task<ActionResult<List<sp_CalcularTotalAPagarPorTarjeta>>> ConsultarConsumoXPeriodo(
            int id_usuario,
            int id_tarjeta,
            int? mes,
            int? año)
        {
            var result = await _estimacionService.ObtenerConsumoPorPeriodo(
                id_usuario,
                id_tarjeta,
                mes,
                año);

            return Ok(result);
        }

        // DETALLE DE GASTO
        [HttpGet]
        [Route("ObtenerGasto")]
        public async Task<IActionResult> ObtenerGasto(int idGasto)
        {
            var gasto = await _estimacionService.ObtenerGasto(idGasto) ?? null;

            return Ok(gasto);
        }
    }
}