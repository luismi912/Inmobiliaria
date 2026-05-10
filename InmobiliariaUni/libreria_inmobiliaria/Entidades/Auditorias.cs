namespace libreria_inmobiliaria.Entidades
{
    public class Auditorias
    {
        public int Id { get; set; }
        public String? TipoAccion { get; set; }
        public String? HaceAccion { get; set; }
        public int IdEntidad { get; set; }
        public DateTime Fecha { get; set; }
        public String? Descripcion { get; set; }
    }
}
