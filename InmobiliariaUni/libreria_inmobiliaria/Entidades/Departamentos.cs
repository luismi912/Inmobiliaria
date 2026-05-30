namespace libreria_inmobiliaria.Entidades
{
    public class Departamentos
    {
        public int Id { get; set; }
        public String? Nombre { get; set; }
        public double? Latitud { get; set; }
        public double? Longitud { get; set; }
        public bool Estado { get; set; }

        public List<Ciudades>? Ciudades { get; set; }
        public List<AdministradoresDepartamentos>? AdministradoresDepartamentos { get; set; }
    }
}
