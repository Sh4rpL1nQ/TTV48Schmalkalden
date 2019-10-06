﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Data.Models.CalendarTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Begin");

                    b.Property<int?>("CalendarTaskTypeId");

                    b.Property<string>("Description");

                    b.Property<DateTime>("End");

                    b.Property<bool>("IsAllDay");

                    b.Property<string>("Location");

                    b.Property<int?>("RecurrentWeekDay");

                    b.Property<string>("Subject");

                    b.HasKey("Id");

                    b.HasIndex("CalendarTaskTypeId");

                    b.ToTable("CalendarTasks");
                });

            modelBuilder.Entity("Data.Models.CalendarTaskType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ColorCode");

                    b.Property<string>("HoverCode");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("CalendarTaskType");
                });

            modelBuilder.Entity("Data.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Data.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Author");

                    b.Property<string>("Body");

                    b.Property<int?>("NewsId");

                    b.Property<DateTime>("Written");

                    b.HasKey("Id");

                    b.HasIndex("NewsId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Data.Models.HasCategory", b =>
                {
                    b.Property<int>("NewsId");

                    b.Property<int>("CategoryId");

                    b.HasKey("NewsId", "CategoryId");

                    b.HasAlternateKey("CategoryId", "NewsId");

                    b.ToTable("HasCategories");
                });

            modelBuilder.Entity("Data.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImageDescription");

                    b.Property<string>("ImageUrl");

                    b.Property<int?>("NewsId");

                    b.HasKey("Id");

                    b.HasIndex("NewsId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Data.Models.News", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Author");

                    b.Property<string>("Body");

                    b.Property<string>("ImageUrl");

                    b.Property<string>("Title");

                    b.Property<DateTime>("Written");

                    b.HasKey("Id");

                    b.ToTable("News");
                });

            modelBuilder.Entity("Data.Models.Supporter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bussines");

                    b.Property<string>("Link");

                    b.Property<string>("LinkName");

                    b.Property<string>("Owner");

                    b.Property<int?>("SupportingTypeId");

                    b.HasKey("Id");

                    b.HasIndex("SupportingTypeId");

                    b.ToTable("Supporters");
                });

            modelBuilder.Entity("Data.Models.SupportingType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TypeName");

                    b.HasKey("Id");

                    b.ToTable("SupportingType");
                });

            modelBuilder.Entity("Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FullName")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("UserName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Data.Models.CalendarTask", b =>
                {
                    b.HasOne("Data.Models.CalendarTaskType", "CalendarTaskType")
                        .WithMany()
                        .HasForeignKey("CalendarTaskTypeId");
                });

            modelBuilder.Entity("Data.Models.Comment", b =>
                {
                    b.HasOne("Data.Models.News", "News")
                        .WithMany()
                        .HasForeignKey("NewsId");
                });

            modelBuilder.Entity("Data.Models.HasCategory", b =>
                {
                    b.HasOne("Data.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Data.Models.News", "News")
                        .WithMany()
                        .HasForeignKey("NewsId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Data.Models.Image", b =>
                {
                    b.HasOne("Data.Models.News", "News")
                        .WithMany()
                        .HasForeignKey("NewsId");
                });

            modelBuilder.Entity("Data.Models.Supporter", b =>
                {
                    b.HasOne("Data.Models.SupportingType", "SupportingType")
                        .WithMany()
                        .HasForeignKey("SupportingTypeId");
                });
#pragma warning restore 612, 618
        }
    }
}
