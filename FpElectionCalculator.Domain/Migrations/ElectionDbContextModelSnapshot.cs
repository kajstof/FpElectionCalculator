﻿// <auto-generated />
using FpElectionCalculator.Domain.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace FpElectionCalculator.Domain.Migrations
{
    [DbContext(typeof(ElectionDbContext))]
    partial class ElectionDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FpElectionCalculator.Domain.DbModels.Candidate", b =>
                {
                    b.Property<int>("CandidateID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.Property<int>("PartyID");

                    b.HasKey("CandidateID");

                    b.HasIndex("PartyID");

                    b.ToTable("Candidates");
                });

            modelBuilder.Entity("FpElectionCalculator.Domain.DbModels.Party", b =>
                {
                    b.Property<int>("PartyID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("PartyID");

                    b.ToTable("Parties");
                });

            modelBuilder.Entity("FpElectionCalculator.Domain.DbModels.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CandidateID");

                    b.Property<string>("Comment");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .HasMaxLength(50);

                    b.Property<int>("PartyID");

                    b.Property<string>("Pesel");

                    b.HasKey("UserID");

                    b.HasIndex("CandidateID");

                    b.HasIndex("PartyID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FpElectionCalculator.Domain.DbModels.Candidate", b =>
                {
                    b.HasOne("FpElectionCalculator.Domain.DbModels.Party", "Party")
                        .WithMany("Candidates")
                        .HasForeignKey("PartyID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FpElectionCalculator.Domain.DbModels.User", b =>
                {
                    b.HasOne("FpElectionCalculator.Domain.DbModels.Candidate", "Candidate")
                        .WithMany()
                        .HasForeignKey("CandidateID");

                    b.HasOne("FpElectionCalculator.Domain.DbModels.Party", "Party")
                        .WithMany()
                        .HasForeignKey("PartyID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
