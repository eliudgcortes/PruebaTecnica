using System;

namespace AppEstFin.Models;

public class sp_ObtenerGastosPorPeriodo
{
    public int id_gasto { get; set; }
    public decimal monto { get; set; }
    public string? descripcion { get; set; }
    public DateTime fecha_movimiento { get; set; }
    public string? categoria_gasto { get; set; }
    public int id_tarjeta { get; set; }
    public string? nombre_tarjeta { get; set; }
}
