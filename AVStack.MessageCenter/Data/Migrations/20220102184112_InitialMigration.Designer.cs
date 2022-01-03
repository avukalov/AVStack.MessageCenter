﻿// <auto-generated />
using System;
using System.Text.Json;
using AVStack.MessageCenter.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AVStack.MessageCenter.Data.Migrations
{
    [DbContext(typeof(McDbContext))]
    [Migration("20220102184112_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("AVStack.MessageCenter.Data.Entities.TemplateEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasMaxLength(2147483647)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<JsonDocument>("Json")
                        .HasColumnType("jsonb");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<Guid?>("TemplateGroupId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("TemplateGroupId");

                    b.ToTable("AVTemplate");
                });

            modelBuilder.Entity("AVStack.MessageCenter.Data.Entities.TemplateGroupEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<JsonDocument>("Json")
                        .HasColumnType("jsonb");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("AVTemplateGroup");
                });

            modelBuilder.Entity("AVStack.MessageCenter.Data.Entities.TemplateEntity", b =>
                {
                    b.HasOne("AVStack.MessageCenter.Data.Entities.TemplateGroupEntity", "TemplateGroup")
                        .WithMany("Templates")
                        .HasForeignKey("TemplateGroupId");

                    b.Navigation("TemplateGroup");
                });

            modelBuilder.Entity("AVStack.MessageCenter.Data.Entities.TemplateGroupEntity", b =>
                {
                    b.Navigation("Templates");
                });
#pragma warning restore 612, 618
        }
    }
}