using libreria_inmobiliaria.Entidades;

namespace libreria_presentaciones_inmobiliaria.interfaces
{
    public interface IRespaldosCodeudoresNegocio
    {
        List<RespaldosCodeudores> Consultar();
        RespaldosCodeudores Guardar(RespaldosCodeudores entidad);
        string Eliminar(RespaldosCodeudores entidad);
        RespaldosCodeudores Modificar(RespaldosCodeudores entidad);
    }
}
