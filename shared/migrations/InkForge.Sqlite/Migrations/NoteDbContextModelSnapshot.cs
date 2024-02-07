﻿// <auto-generated />
using System;
using InkForge.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InkForge.Sqlite.Migrations
{
    [DbContext(typeof(NoteDbContext))]
    partial class NoteDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.1");

            modelBuilder.Entity("InkForge.Data.Blob", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Content")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.HasKey("Id");

                    b.ToTable("Blobs");
                });

            modelBuilder.Entity("InkForge.Data.Infrastructure.NoteEntity", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("InkForge.Data.Infrastructure.NoteVersionEntity", b =>
                {
                    b.Property<int?>("Version")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Id")
                        .HasColumnType("INTEGER");

                    b.HasKey("Version");

                    b.HasIndex("Id", "Version")
                        .IsUnique();

                    b.ToTable("NoteVersions");
                });

            modelBuilder.Entity("InkForge.Data.Infrastructure.NoteEntity", b =>
                {
                    b.OwnsOne("InkForge.Data.Domain.Note", "Value", b1 =>
                        {
                            b1.Property<int>("ParentId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("ContentId")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<DateTimeOffset>("Created")
                                .HasColumnType("TEXT");

                            b1.Property<DateTimeOffset?>("Deleted")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<DateTimeOffset>("Updated")
                                .HasColumnType("TEXT");

                            b1.HasKey("ParentId");

                            b1.HasIndex("ContentId");

                            b1.ToTable("Notes");

                            b1.HasOne("InkForge.Data.Blob", "Content")
                                .WithMany()
                                .HasForeignKey("ContentId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.WithOwner("Parent")
                                .HasForeignKey("ParentId");

                            b1.Navigation("Content");

                            b1.Navigation("Parent");
                        });

                    b.Navigation("Value")
                        .IsRequired();
                });

            modelBuilder.Entity("InkForge.Data.Infrastructure.NoteVersionEntity", b =>
                {
                    b.OwnsOne("InkForge.Data.Domain.Note", "Value", b1 =>
                        {
                            b1.Property<int>("NoteVersionEntityVersion")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("ContentId")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<DateTimeOffset>("Created")
                                .HasColumnType("TEXT");

                            b1.Property<DateTimeOffset?>("Deleted")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<int?>("ParentId")
                                .HasColumnType("INTEGER");

                            b1.Property<DateTimeOffset>("Updated")
                                .HasColumnType("TEXT");

                            b1.HasKey("NoteVersionEntityVersion");

                            b1.HasIndex("ContentId");

                            b1.HasIndex("ParentId");

                            b1.ToTable("NoteVersions");

                            b1.HasOne("InkForge.Data.Blob", "Content")
                                .WithMany()
                                .HasForeignKey("ContentId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.WithOwner()
                                .HasForeignKey("NoteVersionEntityVersion");

                            b1.HasOne("InkForge.Data.Infrastructure.NoteEntity", "Parent")
                                .WithMany()
                                .HasForeignKey("ParentId");

                            b1.Navigation("Content");

                            b1.Navigation("Parent");
                        });

                    b.Navigation("Value")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}