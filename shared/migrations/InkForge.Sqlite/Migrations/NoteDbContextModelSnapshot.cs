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
            modelBuilder.HasAnnotation("ProductVersion", "8.0.3");

            modelBuilder.Entity("InkForge.Data.Blob", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Value")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.HasKey("Id");

                    b.ToTable("Blobs", (string)null);
                });

            modelBuilder.Entity("InkForge.Data.MetadataEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Metadata", (string)null);
                });

            modelBuilder.Entity("InkForge.Data.NoteEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ParentId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Notes", (string)null);
                });

            modelBuilder.Entity("InkForge.Data.NoteEntity", b =>
                {
                    b.HasOne("InkForge.Data.NoteEntity", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId");

                    b.OwnsOne("InkForge.Data.NoteEntity.Value#InkForge.Data.Note", "Value", b1 =>
                        {
                            b1.Property<int>("NoteEntityId")
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

                            b1.HasKey("NoteEntityId");

                            b1.HasIndex("ContentId");

                            b1.ToTable("Notes", (string)null);

                            b1.HasOne("InkForge.Data.Blob", "Content")
                                .WithMany()
                                .HasForeignKey("ContentId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.WithOwner()
                                .HasForeignKey("NoteEntityId");

                            b1.Navigation("Content");
                        });

                    b.Navigation("Parent");

                    b.Navigation("Value")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
