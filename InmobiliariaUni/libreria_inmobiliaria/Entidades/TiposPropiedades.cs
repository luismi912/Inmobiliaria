namespace libreria_inmobiliaria.Entidades
{
    public class TiposPropiedades
    {
        public int Id { get; set; }
        public String? Nombre { get; set; }
        
        public List<Propiedades>? _Propiedad { get; set; }
    }
}
