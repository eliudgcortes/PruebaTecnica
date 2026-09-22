namespace AppEstFin.DTO
{
    public class GastoDTO
    {
        public int Id { get; set; }

        public decimal Monto { get; set; }

        public string? Descripcion { get; set; }

        public string? CategoriaGasto { get; set; }

        public DateTime FechaMovimiento { get; set; }

        public int IdTarjeta { get; set; }

        public int IdUsuario { get; set; }

        public string? Observaciones { get; set; }
    }
}