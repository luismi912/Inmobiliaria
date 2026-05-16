using libreria_inmobiliaria.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace libreria_inmobiliaria.Interfaces
{
    public interface IConexion
    {
        String? StringConexion { get; set; }
        int SaveChanges();
        EntityEntry<T> Entry<T>(T entidad) where T : class;
        DbSet<UsuarioRoles> UsuariosRoles { get; set; }
        DbSet<Auditorias> Auditorias { get; set; }
        DbSet<Personas> Personas { get; set; }
        DbSet<Nacionalidades> Nacionalidades { get; set; }
        DbSet<Departamentos> Departamentos { get; set; }
        DbSet<Ciudades> Ciudades { get; set; }
        DbSet<Direcciones> Direcciones { get; set; }
        DbSet<Sectores> Sectores { get; set; }
        DbSet<Telefonos> Telefonos { get; set; }
        DbSet<ExpedientesLaborales> ExpedientesLaborales { get; set; }
        DbSet<AdministradoresDepartamentos> AdministradoresDepartamentos { get; set; }
        DbSet<JefesSectores> JefesSectores { get; set; }
        DbSet<EmpleadosSectores> EmpleadosSectores { get; set; }
        DbSet<Clientes> Clientes { get; set; }
        DbSet<Compradores> Compradores { get; set; }
        DbSet<Codeudores> Codeudores { get; set; }
        DbSet<TiposPropiedades> TiposPropiedades { get; set; }
        DbSet<Propiedades> Propiedades { get; set; }
        DbSet<Contratos> Contratos { get; set; }
        DbSet<ContratosArriendos> ContratosArriendos { get; set; }
        DbSet<ContratosContados> ContratosContados { get; set; }
        DbSet<ContratosCuotas> ContratosCuotas { get; set; }
        DbSet<Bienes> Bienes { get; set; }
        DbSet<ActivosFinancieros> ActivosFinancieros { get; set; }
        DbSet<RespaldosFinancieros> RespaldosFinancieros { get; set; }
        DbSet<RespaldosCodeudores> RespaldosCodeudores { get; set; }
        DbSet<RespaldosCompradores> RespaldosCompradores { get; set; }
    }
}
