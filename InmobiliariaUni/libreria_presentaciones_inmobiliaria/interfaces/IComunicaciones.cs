namespace libreria_presentaciones_inmobiliaria.interfaces
{
    public interface IComunicaciones
    {
        Task<Dictionary<string, object>> EjecutarGuardar(Dictionary<string, object> datos);
        Task<Dictionary<string, object>> EjecutarEliminar(Dictionary<string, object> datos);
        Task<Dictionary<string, object>> EjecutarModificar(Dictionary<string, object> datos);
        Task<Dictionary<string, object>> EjecutarConsultar(Dictionary<string, object> datos);
    }
}
