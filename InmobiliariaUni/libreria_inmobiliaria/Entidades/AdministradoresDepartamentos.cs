using System.ComponentModel.DataAnnotations.Schema;

namespace libreria_inmobiliaria.Entidades
{
    public class AdministradoresDepartamentos : Personas   
    {
        public decimal PresupuestoDepartamento { get; set; }
        public String? HorarioTrabajo { get; set; }
        public decimal Sueldo { get; set; }
        public int Departamento { get; set; }

        [ForeignKey("Departamento")] public Departamentos? _Departamento { get; set; }   //UNO O MAS ADMINS PUEDEN ADMINISTRAR UN DEPARTAMENTO
        public List<JefesSectores>? _JefeSector { get; set; }    //PUEDE MANEJAR A MUCHOS JEFES
    }
}
