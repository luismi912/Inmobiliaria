using libreria_inmobiliaria.Entidades;

namespace libreria_inmobiliaria.Interfaces
{
    public interface IRespaldosCompradoresNegocio
    {
        List<RespaldosCompradores> Consultar();
        RespaldosCompradores Guardar(RespaldosCompradores entidad);
        string Eliminar(int Id);
        RespaldosCompradores Modificar(RespaldosCompradores entidad);
    }
}
