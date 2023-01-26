﻿// <auto-generated />
using System;
using FWScan.EventTrackerService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FWScan.EventTrackerService.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230126053052_Initialcreate")]
    partial class Initialcreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.13");

            modelBuilder.Entity("FWScan.EventTrackerService.Models.ScanEvent", b =>
                {
                    b.Property<long>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedDateTimeUtc")
                        .HasColumnType("TEXT");

                    b.Property<long>("ParcelId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("StatusCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("EventId");

                    b.ToTable("ScanEvents");
                });
#pragma warning restore 612, 618
        }
    }
}
