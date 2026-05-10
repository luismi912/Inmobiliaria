namespace libreria_inmobiliaria.Entidades
{
    public class RespaldosFinancieros 
    {
        public int Id { get; set; }
        public List<Bienes>? Bienes { get; set; }
        public List<ActivosFinancieros>? ActivosFinancieros { get; set; }
    }
}
