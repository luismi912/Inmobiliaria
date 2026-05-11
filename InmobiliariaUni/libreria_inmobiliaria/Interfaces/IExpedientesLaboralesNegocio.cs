using libreria_inmobiliaria.Entidades;

namespace libreria_inmobiliaria.Interfaces
{
    public interface IExpedientesLaboralesNegocio
    {
        List<ExpedientesLaborales> Consultar();
        string Eliminar(ExpedientesLaborales entidads);
        ExpedientesLaborales Modificar(ExpedientesLaborales entidad);
        ExpedientesLaborales Guardar(ExpedientesLaborales entidad);
    }
}
