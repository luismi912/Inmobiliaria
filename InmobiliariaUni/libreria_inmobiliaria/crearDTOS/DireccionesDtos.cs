namespace libreria_inmobiliaria.crearDTOS
{   
    /*Esta clase se crea como una instancia de los dtos principales que serian como empleados, jefes y administradores
    por ese mismo motivo no tiene id princiapl, se agrega al llamar al servicio*/
    public class DireccionesDtos
    {
            public string? TipoVia { get; set; }
            public string? NumeroVia { get; set; }
            public string? Complemento { get; set; }
            public int Ciudad { get; set; }
    }
}
