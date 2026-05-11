using libreria_inmobiliaria.Entidades;

namespace libreria_inmobiliaria.Interfaces
{
    public interface IActivosFinancierosNegocio
    {
        List<ActivosFinancieros> Consultar();
        string Eliminar(ActivosFinancieros entidad);
        ActivosFinancieros Modificar(ActivosFinancieros entidad);
        ActivosFinancieros Guardar(ActivosFinancieros entidad);
    }
}
