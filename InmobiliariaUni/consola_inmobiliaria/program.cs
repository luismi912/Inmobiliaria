using libreria_presentaciones_inmobiliaria.implemtanciones;
using libreria_presentaciones_inmobiliaria.interfaces;

Console.WriteLine("presentacion_consola");

INacionalidadesNegocio iArbolesNegocio = new NacionalidadesNegocio();
var lista = iArbolesNegocio.Consultar();

foreach (var elemento in lista)
    Console.WriteLine(elemento.Nombre);

Console.WriteLine("Presiona una tecla para cerrar...");
Console.ReadKey();