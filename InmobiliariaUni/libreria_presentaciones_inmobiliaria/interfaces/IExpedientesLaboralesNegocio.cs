using libreria_inmobiliaria.Entidades;

namespace libreria_presentaciones_inmobiliaria.interfaces
{
    public interface IExpedientesLaboralesNegocio
    {
        List<ExpedientesLaborales> Consultar();
        ExpedientesLaborales Guardar(ExpedientesLaborales entidad);
        string Eliminar(ExpedientesLaborales entidad);
        ExpedientesLaborales Modificar(ExpedientesLaborales entidad);
    }
}
