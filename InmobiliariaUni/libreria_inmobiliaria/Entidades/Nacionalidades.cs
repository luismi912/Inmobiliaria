namespace libreria_inmobiliaria.Entidades
{
    public class Nacionalidades
    {
        public int Id { get; set; }
        public String? Nombre { get; set; }

        public List<Personas>? Personas { get; set; }
    }
}
