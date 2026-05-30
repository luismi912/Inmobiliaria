using ClosedXML.Excel;
using libreria_inmobiliaria.Entidades;
using libreria_presentaciones_inmobiliaria.implemtanciones;
using libreria_presentaciones_inmobiliaria.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections;

namespace presentacion_aspnetcore.Pages
{
    [Authorize(Roles = "Administrador")]
    public class AuditoriasModel : PageModel
    {
        private IAuditoriasNegocio? IAuditoriasnegocio;
        [BindProperty] public List<Auditorias>? Lista { get; set; }
        public bool enviarReporte { get; set; }

        public void OnGet()
        {
            OnPostBtRefrescar();
            enviarReporte = false;
        }

        public AuditoriasModel()
        {
            IAuditoriasnegocio = new AuditoriasNegocio();
        }

        public void OnPostBtRefrescar()
        {
            Lista = IAuditoriasnegocio!.Consultar();
            enviarReporte = false;
        }
        public IActionResult OnPostBtExcel()
        {
            var auditorias = IAuditoriasnegocio!.Consultar();

            using var workbook = new XLWorkbook();
            var hoja = workbook.Worksheets.Add("Auditorias");

            // Encabezados
            hoja.Cell(1, 1).Value = "Tipo Accion";
            hoja.Cell(1, 2).Value = "Entidad";
            hoja.Cell(1, 3).Value = "Id Entidad";
            hoja.Cell(1, 4).Value = "Fecha";
            hoja.Cell(1, 5).Value = "Descripcion";

            // Estilo encabezados
            var encabezado = hoja.Range("A1:E1");
            encabezado.Style.Font.Bold = true;
            encabezado.Style.Fill.BackgroundColor = XLColor.AliceBlue;

            // Datos
            int fila = 2;
            foreach (var auditoria in auditorias)
            {
                if (auditoria.TipoAccion == "INSERTAR")
                {
                    hoja.Cell(fila, 1).Value = "Registrar";
                }
                else if (auditoria.TipoAccion == "DELETE")
                {
                    hoja.Cell(fila, 1).Value = "Eliminar";
                }
                else if (auditoria.TipoAccion == "MODIFICAR")
                {
                    hoja.Cell(fila, 1).Value = "Modificar";
                }
                else
                {
                    hoja.Cell(fila, 1).Value = auditoria.TipoAccion;
                }
                hoja.Cell(fila, 2).Value = auditoria.Entidad;
                hoja.Cell(fila, 3).Value = auditoria.IdEntidad;
                hoja.Cell(fila, 4).Value = auditoria.Fecha;
                hoja.Cell(fila, 5).Value = auditoria.Descripcion;
                fila++;
            }

            hoja.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Auditorias.xlsx");
        }

        public void OnPostBtReporte()
        {
            Lista = IAuditoriasnegocio!.Consultar(); 
            enviarReporte = true;
        }

        public void OnPostBtCerrar()
        {
            OnPostBtRefrescar();
        }
    }
}