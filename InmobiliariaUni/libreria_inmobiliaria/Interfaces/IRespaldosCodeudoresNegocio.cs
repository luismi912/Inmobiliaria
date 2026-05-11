using libreria_inmobiliaria.Entidades;

namespace libreria_inmobiliaria.Interfaces
{
    public interface IRespaldosCodeudoresNegocio
    {
        List<RespaldosCodeudores> Consultar();
        RespaldosCodeudores Guardar(RespaldosCodeudores entidad);
        string Eliminar(RespaldosCodeudores entidad);
        RespaldosCodeudores Modificar(RespaldosCodeudores entidad);
    }
}
