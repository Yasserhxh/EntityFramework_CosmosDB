﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repository.Data;

namespace Repository.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220621093103_CosmosDb")]
    partial class CosmosDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Entities.Declaration", b =>
                {
                    b.Property<int>("Declaration_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Declaration_Adresse")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Declaration_Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Declaration_DateValidation")
                        .HasColumnType("datetime2");

                    b.Property<string>("Declaration_Localisation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Declaration_LongLat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Declaration_Photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Declaration_Statut")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Declaration_Validateur")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Declaration_Ville")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Declaration_ID");

                    b.ToTable("declarations");

                    b
                        .HasAnnotation("Cosmos:ContainerName", "Declaration");
                });

            modelBuilder.Entity("Domain.Entities.Declaration", b =>
                {
                    b.OwnsMany("Domain.Entities.Intervention", "Interventions", b1 =>
                        {
                            b1.Property<int>("Intervention_ID")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<int>("Declaration_ID")
                                .HasColumnType("int");

                            b1.Property<int>("Intervention_Commentaire")
                                .HasColumnType("int");

                            b1.Property<int>("Intervention_Date")
                                .HasColumnType("int");

                            b1.Property<int>("Intervention_DeclarationID")
                                .HasColumnType("int");

                            b1.Property<int>("Intervention_Equipe")
                                .HasColumnType("int");

                            b1.Property<int>("Intervention_Resultat")
                                .HasColumnType("int");

                            b1.HasKey("Intervention_ID");

                            b1.HasIndex("Declaration_ID");

                            b1.ToTable("Intervention");

                            b1.WithOwner("Declaration")
                                .HasForeignKey("Declaration_ID");

                            b1.Navigation("Declaration");
                        });

                    b.Navigation("Interventions");
                });
#pragma warning restore 612, 618
        }
    }
}
