using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace Pruebas_Unitarias
{
    [TestClass]
    public class CiudadesUT
    {
        private IConexion? conexion { get; set; }
        private Ciudades? ciudad { get; set; }
        private EntidadesApoyoUT? apoyo { get; set; }
        private Departamentos? departamento { get; set; }


        public CiudadesUT()
        {
            this.conexion = new Conexion();
            this.conexion.StringConexion = Configuraciones.Obtener("Clave");
            this.apoyo = new EntidadesApoyoUT();
        }

        [TestMethod]
        public void Ejecutar()
        {
            Guardar();
            Consultar();
            Modificar();
            Eliminar();
        }

        private void Guardar()
        {
            //LLAMAMOS DE LA CLASE AUXILIAR EL METODO PARA CREAR UN DEPARTAMENTO
            this.departamento = this.apoyo!.GuardarDepartamento();

            this.ciudad = new Ciudades()
            {
                Nombre = "Medellin",
                Poblacion = 232323232,
                FechaCreacion = DateTime.Now,
                CodigoPostal = "+57",
                Estado = true,
                Departamento = this.departamento.Id
            };

            this.conexion!.Ciudades!.Add(this.ciudad);
            this.conexion!.SaveChanges();
        }

        private void Consultar()
        {
            var lista = this.conexion!.Ciudades.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("No hay forma de hacer consultas");
        }

        private void Modificar()
        {
            this.ciudad!.Estado = false;
            this.conexion!.Entry(this.ciudad).State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        public void Eliminar()
        {
            //EL DEPARTAMENTO CREADO EN GUARDAR PARA LA CIUDAD LO ELIMINAMOS ACA
            this.conexion!.Ciudades.Remove(this.ciudad!);
            this.conexion!.Departamentos.Remove(this.departamento!);
            this.conexion.SaveChanges();
        }
    }
}
