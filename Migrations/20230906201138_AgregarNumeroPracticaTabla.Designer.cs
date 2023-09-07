﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Practica_API.Datos;

#nullable disable

namespace Practica_API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230906201138_AgregarNumeroPracticaTabla")]
    partial class AgregarNumeroPracticaTabla
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Practica_API.Modelos.NumeroPractica", b =>
                {
                    b.Property<int>("PracticaNo")
                        .HasColumnType("int");

                    b.Property<string>("DetalleEspecial")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaActualizacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<int>("PracticaId")
                        .HasColumnType("int");

                    b.HasKey("PracticaNo");

                    b.HasIndex("PracticaId");

                    b.ToTable("NumeroPracticas");
                });

            modelBuilder.Entity("Practica_API.Modelos.Practica", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Amenidad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Detalle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Espacio")
                        .HasColumnType("float");

                    b.Property<DateTime>("FechaActualizacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImagenUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Ocupantes")
                        .HasColumnType("int");

                    b.Property<double>("Tarifa")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Practicas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amenidad = "",
                            Detalle = "Detalle de Practica uno",
                            Espacio = 10.0,
                            FechaActualizacion = new DateTime(2023, 9, 6, 17, 11, 38, 96, DateTimeKind.Local).AddTicks(3374),
                            FechaCreacion = new DateTime(2023, 9, 6, 17, 11, 38, 96, DateTimeKind.Local).AddTicks(3325),
                            ImagenUrl = "",
                            Nombre = "Practica uno",
                            Ocupantes = 10,
                            Tarifa = 10.0
                        },
                        new
                        {
                            Id = 2,
                            Amenidad = "",
                            Detalle = "Detalle de Practica dos",
                            Espacio = 20.0,
                            FechaActualizacion = new DateTime(2023, 9, 6, 17, 11, 38, 96, DateTimeKind.Local).AddTicks(3380),
                            FechaCreacion = new DateTime(2023, 9, 6, 17, 11, 38, 96, DateTimeKind.Local).AddTicks(3379),
                            ImagenUrl = "",
                            Nombre = "Practica dos",
                            Ocupantes = 20,
                            Tarifa = 20.0
                        });
                });

            modelBuilder.Entity("Practica_API.Modelos.NumeroPractica", b =>
                {
                    b.HasOne("Practica_API.Modelos.Practica", "Practica")
                        .WithMany()
                        .HasForeignKey("PracticaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Practica");
                });
#pragma warning restore 612, 618
        }
    }
}