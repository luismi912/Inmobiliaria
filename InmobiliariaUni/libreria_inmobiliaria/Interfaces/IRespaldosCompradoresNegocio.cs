using libreria_inmobiliaria.Entidades;

namespace libreria_inmobiliaria.Interfaces
{
    public interface IRespaldosCodeudoresNegocio
    {
        List<RespaldosCodeudores> Consultar();
        RespaldosCodeudores Guardar(RespaldosCodeudores entidad);
        string Eliminar(int Id);
        RespaldosCodeudores Modificar(RespaldosCodeudores entidad);
    }
}
