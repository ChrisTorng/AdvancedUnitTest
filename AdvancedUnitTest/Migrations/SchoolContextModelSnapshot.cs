﻿////// <auto-generated />
////using System;
////using Microsoft.EntityFrameworkCore;
////using Microsoft.EntityFrameworkCore.Infrastructure;
////using SchoolDatabase;

////namespace AdvancedUnitTest.Migrations
////{
////    [DbContext(typeof(SchoolContext))]
////    partial class SchoolContextModelSnapshot : ModelSnapshot
////    {
////        protected override void BuildModel(ModelBuilder modelBuilder)
////        {
////#pragma warning disable 612, 618
////            modelBuilder
////                .HasAnnotation("ProductVersion", "3.1.5");

////            modelBuilder.Entity("AdvancedUnitTest.Models.Student", b =>
////                {
////                    b.Property<int>("ID")
////                        .ValueGeneratedOnAdd()
////                        .HasColumnType("INTEGER");

////                    b.Property<DateTime>("EnrollmentDate")
////                        .HasColumnType("TEXT");

////                    b.Property<string>("FirstMidName")
////                        .IsRequired()
////                        .HasColumnName("FirstName")
////                        .HasColumnType("TEXT")
////                        .HasMaxLength(50);

////                    b.Property<string>("LastName")
////                        .IsRequired()
////                        .HasColumnType("TEXT")
////                        .HasMaxLength(50);

////                    b.HasKey("ID");

////                    b.ToTable("Students");
////                });
////#pragma warning restore 612, 618
////        }
////    }
////}
