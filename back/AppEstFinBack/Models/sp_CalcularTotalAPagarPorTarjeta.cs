using System;

namespace AppEstFin.Models;

public class sp_CalcularTotalAPagarPorTarjeta
{
    public int id_tarjeta { get; set; }
    public string? nombre_tarjeta { get; set; }
    public decimal TotalGastos { get; set; }
    public decimal TotalCargosFijos { get; set; }
    public decimal TotalAPagar { get; set; }
}
