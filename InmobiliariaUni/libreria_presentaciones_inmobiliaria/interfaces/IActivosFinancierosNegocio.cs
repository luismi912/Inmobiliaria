using libreria_inmobiliaria.Entidades;

namespace libreria_presentaciones_inmobiliaria.interfaces
{
    public interface IActivosFinancierosNegocio
    {
        List<ActivosFinancieros> Consultar();
        ActivosFinancieros Guardar(ActivosFinancieros entidad);
        ActivosFinancieros Eliminar(ActivosFinancieros entidad);
        ActivosFinancieros Modificar(ActivosFinancieros entidad);
    }
}
