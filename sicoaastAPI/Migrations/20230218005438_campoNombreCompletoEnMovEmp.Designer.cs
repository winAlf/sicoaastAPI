﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using sicoaastAPI.Data;

#nullable disable

namespace sicoaastAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230218005438_campoNombreCompletoEnMovEmp")]
    partial class campoNombreCompletoEnMovEmp
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("sicoaastAPI.Models.Ccosto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("organismoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("organismoId");

                    b.ToTable("Ccostos");
                });

            modelBuilder.Entity("sicoaastAPI.Models.Departamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ccostoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ccostoId");

                    b.ToTable("Departamentos");
                });

            modelBuilder.Entity("sicoaastAPI.Models.Empresa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Empresas");
                });

            modelBuilder.Entity("sicoaastAPI.Models.MovEmp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Amaterno")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Apaterno")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("FechaActualizacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaAlta")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaBaja")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaReactivacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaVigencia")
                        .HasColumnType("datetime2");

                    b.Property<int>("Folio")
                        .HasColumnType("int");

                    b.Property<int?>("Genero")
                        .HasColumnType("int");

                    b.Property<int?>("Nip")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NombreCompleto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RutaImagen")
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<int>("activo")
                        .HasColumnType("int");

                    b.Property<int>("ccostoId")
                        .HasColumnType("int");

                    b.Property<int>("departamentoId")
                        .HasColumnType("int");

                    b.Property<int>("empresaId")
                        .HasColumnType("int");

                    b.Property<int>("numEmp")
                        .HasColumnType("int");

                    b.Property<int>("organismoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ccostoId");

                    b.HasIndex("departamentoId");

                    b.HasIndex("empresaId");

                    b.HasIndex("organismoId");

                    b.ToTable("MovEmp");
                });

            modelBuilder.Entity("sicoaastAPI.Models.Organismo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("empresaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("empresaId");

                    b.ToTable("Organismos");
                });

            modelBuilder.Entity("sicoaastAPI.Models.Ccosto", b =>
                {
                    b.HasOne("sicoaastAPI.Models.Organismo", "Organismo")
                        .WithMany()
                        .HasForeignKey("organismoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organismo");
                });

            modelBuilder.Entity("sicoaastAPI.Models.Departamento", b =>
                {
                    b.HasOne("sicoaastAPI.Models.Ccosto", "Ccosto")
                        .WithMany()
                        .HasForeignKey("ccostoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ccosto");
                });

            modelBuilder.Entity("sicoaastAPI.Models.MovEmp", b =>
                {
                    b.HasOne("sicoaastAPI.Models.Ccosto", "Ccosto")
                        .WithMany()
                        .HasForeignKey("ccostoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("sicoaastAPI.Models.Departamento", "Departamento")
                        .WithMany()
                        .HasForeignKey("departamentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("sicoaastAPI.Models.Empresa", "Empresa")
                        .WithMany()
                        .HasForeignKey("empresaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("sicoaastAPI.Models.Organismo", "Organismo")
                        .WithMany()
                        .HasForeignKey("organismoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ccosto");

                    b.Navigation("Departamento");

                    b.Navigation("Empresa");

                    b.Navigation("Organismo");
                });

            modelBuilder.Entity("sicoaastAPI.Models.Organismo", b =>
                {
                    b.HasOne("sicoaastAPI.Models.Empresa", "Empresa")
                        .WithMany()
                        .HasForeignKey("empresaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empresa");
                });
#pragma warning restore 612, 618
        }
    }
}
