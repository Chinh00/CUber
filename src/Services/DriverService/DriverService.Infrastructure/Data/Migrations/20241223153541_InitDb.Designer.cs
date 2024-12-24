﻿// <auto-generated />
using System;
using DriverService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DriverService.Infrastructure.Data.Migrations
{
    [DbContext(typeof(DriverContext))]
    [Migration("20241223153541_InitDb")]
    partial class InitDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DriverService.AppCore.Domain.Outboxs.VehicleOutbox", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("AggregateId")
                        .HasColumnType("text")
                        .HasColumnName("aggregate_id");

                    b.Property<string>("AggregateType")
                        .HasColumnType("text")
                        .HasColumnName("aggregate_type");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<byte[]>("Payload")
                        .HasColumnType("bytea")
                        .HasColumnName("payload");

                    b.Property<string>("Type")
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_vehicle_outboxes");

                    b.ToTable("vehicle_outboxes", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}