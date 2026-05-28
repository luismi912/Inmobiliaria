using libreria_inmobiliaria.crearDTOS;
using libreria_inmobiliaria.Entidades;

namespace libreria_presentaciones_inmobiliaria.interfaces
{
    public interface IClientesNegocio
    {
        List<Clientes> Consultar();
        Clientes Guardar(ClientesDtos clienteDto);
        string Eliminar(Clientes entidad);
        Clientes Modificar(Clientes entidad);
        int ConsultarPorCedula(string cedula);
    }
}
