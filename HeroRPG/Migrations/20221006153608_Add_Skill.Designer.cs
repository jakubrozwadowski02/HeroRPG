﻿// <auto-generated />
using System;
using HeroRPG.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HeroRPG.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221006153608_Add_Skill")]
    partial class Add_Skill
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("HeroRPG.Models.Hero", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Class")
                        .HasColumnType("int");

                    b.Property<int>("Defense")
                        .HasColumnType("int");

                    b.Property<int>("HitPoints")
                        .HasColumnType("int");

                    b.Property<int>("Intelligence")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Strength")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Heroes");
                });

            modelBuilder.Entity("HeroRPG.Models.Skill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Damage")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("HeroRPG.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("HeroRPG.Models.Weapon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Damage")
                        .HasColumnType("int");

                    b.Property<int>("HeroId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HeroId")
                        .IsUnique();

                    b.ToTable("Weapons");
                });

            modelBuilder.Entity("HeroSkill", b =>
                {
                    b.Property<int>("HeroesId")
                        .HasColumnType("int");

                    b.Property<int>("SkillsId")
                        .HasColumnType("int");

                    b.HasKey("HeroesId", "SkillsId");

                    b.HasIndex("SkillsId");

                    b.ToTable("HeroSkill");
                });

            modelBuilder.Entity("HeroRPG.Models.Hero", b =>
                {
                    b.HasOne("HeroRPG.Models.User", "User")
                        .WithMany("Heroes")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("HeroRPG.Models.Weapon", b =>
                {
                    b.HasOne("HeroRPG.Models.Hero", "Hero")
                        .WithOne("Weapon")
                        .HasForeignKey("HeroRPG.Models.Weapon", "HeroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hero");
                });

            modelBuilder.Entity("HeroSkill", b =>
                {
                    b.HasOne("HeroRPG.Models.Hero", null)
                        .WithMany()
                        .HasForeignKey("HeroesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HeroRPG.Models.Skill", null)
                        .WithMany()
                        .HasForeignKey("SkillsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HeroRPG.Models.Hero", b =>
                {
                    b.Navigation("Weapon")
                        .IsRequired();
                });

            modelBuilder.Entity("HeroRPG.Models.User", b =>
                {
                    b.Navigation("Heroes");
                });
#pragma warning restore 612, 618
        }
    }
}
