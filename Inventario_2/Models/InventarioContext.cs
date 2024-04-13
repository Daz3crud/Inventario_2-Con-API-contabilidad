using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Inventario_2.Models;

public partial class InventarioContext : DbContext
{
    public InventarioContext()
    {
    }

    public InventarioContext(DbContextOptions<InventarioContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Almacene> Almacenes { get; set; }

    public virtual DbSet<Articulo> Articulos { get; set; }

    public virtual DbSet<AsientoContable> AsientoContables { get; set; }

    public virtual DbSet<CuentaContable> CuentaContables { get; set; }

    public virtual DbSet<TiposInventario> TiposInventarios { get; set; }

    public virtual DbSet<Transaccione> Transacciones { get; set; }
    public virtual DbSet<EstadosContables> EstadosContables { get; set; }





    /*
     * 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseSqlServer("Server=.; Database=Inventario; Integrated Security=True; Trusted_Connection=True; TrustServerCertificate=True");
    */
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Almacene>(entity =>
        {
            entity.HasKey(e => e.IdAlmacenes).HasName("PK__Almacene__A56E336995113A9F");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(8)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Articulo>(entity =>
        {
            entity.HasKey(e => e.IdArticulos).HasName("PK__Articulo__A1E947759EF05868");

            entity.Property(e => e.CostoUnitario).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(8)
                .IsUnicode(false);

            entity.HasOne(d => d.IdInventarioNavigation).WithMany(p => p.Articulos)
                .HasForeignKey(d => d.IdInventario)
                .HasConstraintName("FK_TiposInventario");
        });

        // Dentro del método OnModelCreating en tu clase InventariodbbContext
        modelBuilder.Entity<AsientoContable>(entity =>
        {
            entity.HasKey(e => e.IdMovimiento).HasName("PK__AsientoC__881A6AE0DD6D7CF1");

            entity.ToTable("AsientoContable");

            entity.Property(e => e.Auxiliar).HasDefaultValueSql("((4))");
            entity.Property(e => e.CuentaCr).HasColumnName("CuentaCR");
            entity.Property(e => e.CuentaDb).HasColumnName("CuentaDB");
            entity.Property(e => e.Descripcion).HasMaxLength(50);
            entity.Property(e => e.Monto).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Fecha).HasDefaultValueSql("getdate()");
            entity.Property(e => e.Estado).HasColumnName("Estado");
            entity.Property(e => e.Moneda).HasColumnName("Moneda");
           
    {
        base.OnModelCreating(modelBuilder);

        // Evitar que la entidad AsientoContable se incluya en el modelo de base de datos
        modelBuilder.Ignore<AsientoContable>();
    }






});


        modelBuilder.Entity<CuentaContable>(entity =>
        {
            entity.HasKey(e => e.IdCuentaContable).HasName("PK__CuentaCo__458CB9B223C1455F");

            entity.ToTable("CuentaContable");

            entity.Property(e => e.IdCuentaContable).ValueGeneratedNever();
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EstadosContables>(entity =>
        {
            entity.HasKey(e => e.IdEstado).HasName("PK__EstadosC__3214EC070A2D280B"); // Corregido para que coincida con el nombre de la clave primaria en la tabla de base de datos

            entity.ToTable("EstadosContables");

            entity.Property(e => e.IdEstado).ValueGeneratedNever(); // Si el Id no es generado automáticamente, cambia ValueGeneratedNever a ValueGeneratedOnAdd o ValueGeneratedOnAddOrUpdate
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100) // Ajustado a 100 para que coincida con el script SQL
                .IsUnicode(false)
                .HasColumnName("Descripcion"); // Especifica el nombre de la columna en la tabla de base de datos si es diferente al nombre de la propiedad en la clase
        });







        modelBuilder.Entity<TiposInventario>(entity =>
        {
            entity.HasKey(e => e.IdInventario).HasName("PK__TiposInv__1927B20C54F04ACA");

            entity.ToTable("TiposInventario");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(8)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Transaccione>(entity =>
        {
            entity.HasKey(e => e.IdTransaccion).HasName("PK__Transacc__334B1F77D762842D");

            entity.ToTable(tb => tb.HasTrigger("TRG_ActualizarExistenciasArticulos"));

            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.Monto).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TipoTransaccion)
                .HasMaxLength(7)
                .IsUnicode(false);

            entity.HasOne(d => d.IdArticulosNavigation).WithMany(p => p.Transacciones)
                .HasForeignKey(d => d.IdArticulos)
                .HasConstraintName("FK_Articulo");
        });

        OnModelCreatingPartial(modelBuilder);

    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
