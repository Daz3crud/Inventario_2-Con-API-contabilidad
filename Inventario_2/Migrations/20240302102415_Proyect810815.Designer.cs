﻿using System;
using Inventario_2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Inventario_2.Migrations
{
    [DbContext(typeof(InventarioContext))]
    [Migration("20240302102415_Proyect810815")]
    partial class Proyect810815
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity<Almacene>(b =>
            {
                b.Property<int>("IdAlmacenes")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdAlmacenes"));

                b.Property<string>("Descripcion")
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnType("varchar(50)");

                b.Property<string>("Estado")
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnType("varchar(8)");

                b.HasKey("IdAlmacenes")
                    .HasName("PK__Almacene__A56E336995113A9F");

                b.ToTable("Almacenes");
            });

            modelBuilder.Entity<Articulo>(b =>
            {
                b.Property<int>("IdArticulos")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdArticulos"));

                b.Property<decimal>("CostoUnitario")
                    .HasColumnType("decimal(10, 2)");

                b.Property<string>("Descripcion")
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnType("varchar(50)");

                b.Property<string>("Estado")
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnType("varchar(8)");

                b.Property<int>("Existencia")
                    .HasColumnType("int");

                b.Property<int?>("IdInventario")
                    .HasColumnType("int");

                b.HasKey("IdArticulos")
                    .HasName("PK__Articulo__A1E947759EF05868");

                b.HasIndex("IdInventario");

                b.ToTable("Articulos");
            });

            modelBuilder.Entity<AsientoContable>(b =>
            {
                b.Property<int>("IdMovimiento")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdMovimiento"));

                b.Property<int?>("Auxiliar")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasDefaultValueSql("((4))");

                b.Property<int>("CuentaCr")
                    .HasColumnType("int")
                    .HasColumnName("CuentaCR");

                b.Property<int>("CuentaDb")
                    .HasColumnType("int")
                    .HasColumnName("CuentaDB");

                b.Property<int>("Estado")
                  .HasColumnType("int")
                  .HasColumnName("Estado");


                b.Property<string>("Descripcion")
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("nvarchar(50)");

                b.Property<decimal?>("Monto")
                    .HasColumnType("decimal(10, 2)");

                b.HasKey("IdMovimiento")
                    .HasName("PK__AsientoC__881A6AE0DD6D7CF1");

                b.ToTable("AsientoContable", (string)null);
            });

            modelBuilder.Entity<CuentaContable>(b =>
            {
                b.Property<int>("IdCuentaContable")
                    .HasColumnType("int");

                b.Property<string>("Descripcion")
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnType("varchar(50)");

                b.HasKey("IdCuentaContable")
                    .HasName("PK__CuentaCo__458CB9B223C1455F");

                b.ToTable("CuentaContable", (string)null);
            });

            modelBuilder.Entity<EstadosContables>(b =>
            {
                b.Property<int>("IdEstado")
                    .HasColumnType("int");

                b.Property<string>("Descripcion")
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnType("varchar(100)");

                b.HasKey("IdEstado")
                    .HasName("PK__EstadosC__3214EC070A2D280B");

                b.ToTable("CuentaContable", (string)null);
            });






            modelBuilder.Entity<TiposInventario>(b =>
            {
                b.Property<int>("IdInventario")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdInventario"));

                b.Property<int>("CuentaContable")
                    .HasColumnType("int");

                b.Property<string>("Descripcion")
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnType("varchar(50)");

                b.Property<string>("Estado")
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnType("varchar(8)");

                b.HasKey("IdInventario")
                    .HasName("PK__TiposInv__1927B20C54F04ACA");

                b.ToTable("TiposInventario", (string)null);
            });

            modelBuilder.Entity<Transaccione>(b =>
            {
                b.Property<int>("IdTransaccion")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTransaccion"));

                b.Property<int>("Cantidad")
                    .HasColumnType("int");

                b.Property<DateTime>("Fecha")
                    .HasColumnType("date");

                b.Property<int?>("IdArticulos")
                    .HasColumnType("int");

                b.Property<decimal?>("Monto")
                    .HasColumnType("decimal(10, 2)");

                b.Property<string>("TipoTransaccion")
                    .IsRequired()
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnType("varchar(7)");

                b.HasKey("IdTransaccion")
                    .HasName("PK__Transacc__334B1F77D762842D");

                b.HasIndex("IdArticulos");

                b.ToTable("Transacciones", t =>
                {
                    t.HasTrigger("TRG_ActualizarExistenciasArticulos");
                });
            });

            modelBuilder.Entity<Articulo>(b =>
            {
                b.HasOne("Inventario_2.Models.TiposInventario", "IdInventarioNavigation")
                    .WithMany("Articulos")
                    .HasForeignKey("IdInventario")
                    .HasConstraintName("FK_TiposInventario");

                b.Navigation("IdInventarioNavigation");
            });

            modelBuilder.Entity<Transaccione>(b =>
            {
                b.HasOne("Inventario_2.Models.Articulo", "IdArticulosNavigation")
                    .WithMany("Transacciones")
                    .HasForeignKey("IdArticulos")
                    .HasConstraintName("FK_Articulo");

                b.Navigation("IdArticulosNavigation");
            });

            modelBuilder.Entity<Articulo>(b =>
            {
                b.Navigation("Transacciones");
            });

            modelBuilder.Entity<TiposInventario>(b =>
            {
                b.Navigation("Articulos");
            });
#pragma warning restore 612, 618
        }
    }
}
