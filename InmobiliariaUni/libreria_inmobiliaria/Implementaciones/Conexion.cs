using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace libreria_inmobiliaria.Implementaciones
{
    public class Conexion : DbContext, IConexion
    {
        public String? StringConexion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.StringConexion!, p => { });
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Tabla padre Personas
            modelBuilder.Entity<Personas>().ToTable("Personas");

            // Hijos de Personas
            modelBuilder.Entity<AdministradoresDepartamentos>().ToTable("AdministradoresDepartamentos");
            modelBuilder.Entity<EmpleadosSectores>().ToTable("EmpleadosSectores");
            modelBuilder.Entity<JefesSectores>().ToTable("JefesSectores");
            modelBuilder.Entity<Clientes>().ToTable("Clientes");
            modelBuilder.Entity<Codeudores>().ToTable("Codeudores");
            modelBuilder.Entity<Compradores>().ToTable("Compradores");

            // Tabla padre RespaldoFinanciero
            modelBuilder.Entity<RespaldosFinancieros>().ToTable("RespaldosFinancieros");

            // Hijos de RespaldoFinanciero
            modelBuilder.Entity<RespaldosCompradores>().ToTable("RespaldosCompradores");
            modelBuilder.Entity<RespaldosCodeudores>().ToTable("RespaldosCodeudores");

            // Tabla padre Contratos
            modelBuilder.Entity<Contratos>().ToTable("Contratos");

            // Hijos de Contratos
            modelBuilder.Entity<ContratosArriendos>().ToTable("ContratosArriendo");
            modelBuilder.Entity<ContratosContados>().ToTable("ContratosContado");
            modelBuilder.Entity<ContratosCuotas>().ToTable("ContratosCuotas");

        }

        public DbSet<Auditorias> Auditorias { get; set; }
        public DbSet<Personas> Personas { get; set; }
        public DbSet<Nacionalidades> Nacionalidades { get; set; }
        public DbSet<Departamentos> Departamentos { get; set; }
        public DbSet<Ciudades> Ciudades { get; set; }
        public DbSet<Direcciones> Direcciones { get; set; }
        public DbSet<Sectores> Sectores { get; set; }
        public DbSet<Telefonos> Telefonos { get; set; }
        public DbSet<ExpedientesLaborales> ExpedientesLaborales { get; set; }
        public DbSet<Bienes> Bienes { get; set; }
        public DbSet<ActivosFinancieros> ActivosFinancieros { get; set; }
        public DbSet<AdministradoresDepartamentos> AdministradoresDepartamentos { get; set; }
        public DbSet<JefesSectores> JefesSectores { get; set; }
        public DbSet<EmpleadosSectores> EmpleadosSectores { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Compradores> Compradores { get; set; }
        public DbSet<Codeudores> Codeudores { get; set; }
        public DbSet<TiposPropiedades> TiposPropiedades { get; set; }
        public DbSet<Propiedades> Propiedades { get; set; }
        public DbSet<Contratos> Contratos { get; set; }
        public DbSet<ContratosArriendos> ContratosArriendos { get; set; }
        public DbSet<ContratosContados> ContratosContados { get; set; }
        public DbSet<ContratosCuotas> ContratosCuotas { get; set; }
        public DbSet<RespaldosFinancieros> RespaldosFinancieros { get; set; }
        public DbSet<RespaldosCodeudores> RespaldosCodeudores { get; set; }
        public DbSet<RespaldosCompradores> RespaldosCompradores { get; set; }
        public DbSet<UsuarioRoles> UsuariosRoles { get; set; }
    }
}
