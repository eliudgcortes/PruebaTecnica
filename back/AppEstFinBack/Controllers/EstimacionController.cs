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
        // CORRECCION:
        // Se cambio de [HttpPost] a [HttpGet] porque este metodo
        // solamente CONSULTA informacion y no modifica datos.
        [HttpGet]
        [Route("ConsultarConsumo")]
        public async Task<ActionResult<List<sp_CalcularTotalAPagarPorTarjeta>>> ConsultarConsumo(
            int idUsuario)
        {
            var result = await _estimacionService.ObtenerDatosConsumo(idUsuario);

            return Ok(result);
        }

        // INSERTAR GASTO
        // CORRECCION:
        // Se cambio de [HttpGet] a [HttpPost].
        //
        // GET debe utilizarse normalmente para consultar informacion.
        // POST se utiliza cuando queremos crear o insertar informacion.
        [HttpPost]
        [Route("InsertarGasto")]
        public async Task<ActionResult<EjecutaAccionDTO>> InsertarGasto(
            decimal monto,
            string descripcion,
            string categoriaGasto,
            int idTarjeta,
            int idUsuario)
        {
            // Se inserta el gasto utilizando el servicio.
            var result = await _estimacionService.InsertarGasto(
                monto,
                descripcion,
                DateTime.Now,
                categoriaGasto,
                idTarjeta,
                idUsuario);

            // CORRECCION:
            // En lugar de crear primero un objeto vacio y despues, asignar cada propiedad, podemos inicializarlo directamente.
            var accion = new EjecutaAccionDTO
            {
                valida = result,
                mensaje = "Proceso terminado"
            };

            return Ok(accion);
        }

        // ACTUALIZAR GASTO
        // CORRECCION:
        // Se cambio de [HttpPost] a [HttpPut] porque PUT se utiliza para actualizar informacion existente.
        // Si el servicio necesita, por ejemplo, idGasto, monto, descripcion, o algo mas, tendremos que agregarlos despues.
        [HttpPut]
        [Route("ActualizarGasto")]
        public async Task<IActionResult> ActualizarGasto()
        {
            await _estimacionService.ActualizarGasto();

            return Ok("Actualizado");
        }

        // ELIMINAR GASTO
        // CORRECCION:
        // Se cambio de [HttpGet] a [HttpDelete] porque este metodo elimina informacion.
        [HttpDelete]
        [Route("EliminarGasto")]
        public async Task<IActionResult> EliminarGasto()
        {
            await _estimacionService.EliminarGasto();

            return Ok();
        }

        // CORRECCION:
        // Se cambiaron los nombres de id_usuario e id_tarjeta a idUsuario e idTarjeta para mantener una convencion normal.
        [HttpGet]
        [Route("ConsultarConsumoXPeriodo")]
        public async Task<ActionResult<List<sp_CalcularTotalAPagarPorTarjeta>>> ConsultarConsumoXPeriodo(
            int idUsuario,
            int idTarjeta,
            int? mes,
            int? anio)
        {
            var result = await _estimacionService.ObtenerConsumoPorPeriodo(
                idUsuario,
                idTarjeta,
                mes,
                anio);

            return Ok(result);
        }

        // DETALLE DE GASTO
        [HttpGet]
        [Route("ObtenerGasto")]
        public async Task<IActionResult> ObtenerGasto(int idGasto)
        {
            // CORRECCION:
            // No es necesario "?? null"...
            // Antes teniamos:
            // var gasto = await _estimacionService.ObtenerGasto(idGasto) ?? null;
            // No es necesario porque si el servicio devuelve null, la variable gasto ya tendra el valor null.
            var gasto = await _estimacionService.ObtenerGasto(idGasto);

            // CORRECCION:
            // Si el gasto no existe, devolvemos HTTP 404 (Not Found), porque le indica al cliente que el recurso solicitado realmente no fue encontrado.
            if (gasto == null)
            {
                return NotFound();
            }

            return Ok(gasto);
        }
    }
}
