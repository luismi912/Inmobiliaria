using libreria_inmobiliaria.crearDTOS;
using libreria_inmobiliaria.Entidades;

namespace libreria_presentaciones_inmobiliaria.interfaces
{
    public interface IClientesNegocio
    {
        List<Clientes> Consultar();
        Clientes Guardar(CrearUsuariosClientesDtos clienteDto);
        Clientes Eliminar(Clientes entidad);
        Clientes Modificar(Clientes entidad);
    }
}
