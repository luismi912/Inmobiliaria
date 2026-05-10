using libreria_inmobiliaria.Entidades;

namespace libreria_inmobiliaria.Interfaces
{
    public interface IContratosCuotasNegocio
    {
        ContratosCuotas Guardar(ContratosCuotas entidad);
        List<ContratosCuotas> Consultar();
        string Eliminar(int Id);
        ContratosCuotas Modificar(ContratosCuotas entidad);
    }
}
