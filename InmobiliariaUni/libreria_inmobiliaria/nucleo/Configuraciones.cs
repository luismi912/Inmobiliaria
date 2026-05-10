namespace libreria_inmobiliaria.Nucleo
{
    public class Configuraciones
    {
        public static string Obtener(string clave)
        {
            return "server=localhost;database=inmobiliariaUni;Integrated Security=True;TrustServerCertificate=true;";
        }
    }
}
