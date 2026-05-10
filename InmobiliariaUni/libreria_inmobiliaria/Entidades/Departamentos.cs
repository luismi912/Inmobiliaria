namespace libreria_inmobiliaria.Entidades
{
    public class Departamentos
    {
        public int Id { get; set; }
        public String? Nombre { get; set; }

        public List<Ciudades>? Ciudades { get; set; }
    }
}
