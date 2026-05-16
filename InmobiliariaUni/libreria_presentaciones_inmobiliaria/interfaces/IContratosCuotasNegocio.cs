using libreria_inmobiliaria.Entidades;

namespace libreria_presentaciones_inmobiliaria.interfaces
{
    public interface IContratosCuotasNegocio
    {
        List<ContratosCuotas> Consultar();
        ContratosCuotas Guardar(ContratosCuotas entidad);
        string Eliminar(ContratosCuotas entidad);
        ContratosCuotas Modificar(ContratosCuotas entidad);
    }
}
