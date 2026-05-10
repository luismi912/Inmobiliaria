using System.ComponentModel.DataAnnotations.Schema;

namespace libreria_inmobiliaria.Entidades
{
    public class Ciudades
    {
        public int Id { get; set; }
        public String? Nombre { get; set; }
        public int Poblacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public String? CodigoPostal { get; set; }
        public int Departamento { get; set; }

        [ForeignKey("Departamento")] public Departamentos? _Departamento { get; set; }  //VARIAS CIUDADES PERTENECEN A UN DEPARTAMENTO
        public List<Sectores>? Sectores { get; set; }    //EN UNA CIUDAD HAY VARIOS SECTORES
        public List<Direcciones>? Direcciones { get; set; }    //EN UNA CIUDAD HAY VARIAS DIRECCIONES
    }
}
