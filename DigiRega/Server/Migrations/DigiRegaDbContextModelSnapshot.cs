﻿// <auto-generated />
using System;
using DigiRega.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DigiRega.Server.Migrations
{
    [DbContext(typeof(DigiRegaDbContext))]
    partial class DigiRegaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("DigiRega.Server.Model.Club", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Clubs");
                });

            modelBuilder.Entity("DigiRega.Server.Model.EmailMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Priority")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("SentAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("EmailMessages");
                });

            modelBuilder.Entity("DigiRega.Server.Model.Entry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("ClubId")
                        .HasColumnType("integer");

                    b.Property<string>("EntryType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RaceId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("SentAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("SentById")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ClubId");

                    b.HasIndex("RaceId");

                    b.HasIndex("SentById");

                    b.ToTable("Entries");

                    b.HasDiscriminator<string>("EntryType").HasValue("Entry");
                });

            modelBuilder.Entity("DigiRega.Server.Model.Race", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Races");
                });

            modelBuilder.Entity("DigiRega.Server.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Type").HasValue("User");
                });

            modelBuilder.Entity("DigiRega.Server.Model.CrewChange", b =>
                {
                    b.HasBaseType("DigiRega.Server.Model.Entry");

                    b.Property<int>("BowNumber")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("integer")
                        .HasColumnName("BowNumber");

                    b.HasDiscriminator().HasValue("CrewChange");
                });

            modelBuilder.Entity("DigiRega.Server.Model.LateEntry", b =>
                {
                    b.HasBaseType("DigiRega.Server.Model.Entry");

                    b.HasDiscriminator().HasValue("LateEntry");
                });

            modelBuilder.Entity("DigiRega.Server.Model.Withdrawal", b =>
                {
                    b.HasBaseType("DigiRega.Server.Model.Entry");

                    b.Property<int>("BowNumber")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("integer")
                        .HasColumnName("BowNumber");

                    b.HasDiscriminator().HasValue("Withdrawal");
                });

            modelBuilder.Entity("DigiRega.Server.Model.Manager", b =>
                {
                    b.HasBaseType("DigiRega.Server.Model.User");

                    b.Property<int>("ClubId")
                        .HasColumnType("integer");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Secret")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasIndex("ClubId");

                    b.HasDiscriminator().HasValue("Manager");
                });

            modelBuilder.Entity("DigiRega.Server.Model.StaffMember", b =>
                {
                    b.HasBaseType("DigiRega.Server.Model.User");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasDiscriminator().HasValue("StaffMember");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            PasswordHash = new byte[] { 91, 244, 113, 71, 192, 65, 116, 26, 224, 75, 68, 206, 57, 252, 195, 130, 92, 97, 233, 154, 136, 154, 246, 187, 170, 58, 122, 75, 111, 220, 240, 174, 145, 21, 77, 126, 170, 183, 80, 87, 59, 65, 192, 144, 20, 57, 125, 5, 113, 254, 250, 193, 164, 82, 64, 109, 193, 24, 220, 77, 104, 125, 245, 173 },
                            PasswordSalt = new byte[] { 138, 189, 26, 38, 34, 109, 222, 1, 85, 35, 222, 162, 39, 128, 123, 174, 189, 201, 146, 7, 80, 78, 233, 28, 23, 3, 249, 188, 237, 171, 88, 242, 18, 228, 251, 57, 234, 136, 31, 220, 54, 183, 42, 120, 30, 116, 63, 241, 47, 120, 14, 64, 113, 42, 85, 90, 175, 25, 248, 147, 46, 183, 47, 230, 254, 173, 88, 45, 201, 137, 6, 169, 27, 210, 204, 44, 186, 127, 21, 237, 197, 10, 112, 1, 23, 155, 236, 231, 29, 184, 16, 32, 217, 119, 150, 185, 173, 105, 60, 46, 206, 101, 221, 59, 220, 236, 0, 255, 59, 89, 141, 197, 16, 111, 22, 151, 120, 203, 192, 30, 203, 77, 63, 175, 74, 40, 242, 122 },
                            Username = "admin",
                            Role = "Admin"
                        });
                });

            modelBuilder.Entity("DigiRega.Server.Model.EmailMessage", b =>
                {
                    b.OwnsOne("DigiRega.Server.Model.EmailRecipient", "Recipient", b1 =>
                        {
                            b1.Property<int>("EmailMessageId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer")
                                .UseIdentityByDefaultColumn();

                            b1.Property<string>("EmailAddress")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("EmailMessageId");

                            b1.ToTable("EmailMessages");

                            b1.WithOwner()
                                .HasForeignKey("EmailMessageId");
                        });

                    b.Navigation("Recipient")
                        .IsRequired();
                });

            modelBuilder.Entity("DigiRega.Server.Model.Entry", b =>
                {
                    b.HasOne("DigiRega.Server.Model.Club", "Club")
                        .WithMany()
                        .HasForeignKey("ClubId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DigiRega.Server.Model.Race", "Race")
                        .WithMany()
                        .HasForeignKey("RaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DigiRega.Server.Model.Manager", "SentBy")
                        .WithMany()
                        .HasForeignKey("SentById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Club");

                    b.Navigation("Race");

                    b.Navigation("SentBy");
                });

            modelBuilder.Entity("DigiRega.Server.Model.User", b =>
                {
                    b.OwnsMany("DigiRega.Server.Model.RefreshToken", "RefreshTokens", b1 =>
                        {
                            b1.Property<int>("UserId")
                                .HasColumnType("integer");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer")
                                .UseIdentityByDefaultColumn();

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("timestamp without time zone");

                            b1.Property<DateTime>("ExpiresAt")
                                .HasColumnType("timestamp without time zone");

                            b1.Property<string>("Token")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("UserId", "Id");

                            b1.ToTable("Users_RefreshTokens");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("RefreshTokens");
                });

            modelBuilder.Entity("DigiRega.Server.Model.CrewChange", b =>
                {
                    b.OwnsMany("DigiRega.Server.Model.Substitution", "Substitutions", b1 =>
                        {
                            b1.Property<int>("CrewChangeId")
                                .HasColumnType("integer");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer")
                                .UseIdentityByDefaultColumn();

                            b1.HasKey("CrewChangeId", "Id");

                            b1.ToTable("Substitution");

                            b1.WithOwner()
                                .HasForeignKey("CrewChangeId");

                            b1.OwnsOne("DigiRega.Server.Model.Athlete", "New", b2 =>
                                {
                                    b2.Property<int>("SubstitutionCrewChangeId")
                                        .HasColumnType("integer");

                                    b2.Property<int>("SubstitutionId")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer")
                                        .UseIdentityByDefaultColumn();

                                    b2.Property<string>("FirstName")
                                        .IsRequired()
                                        .HasColumnType("text");

                                    b2.Property<string>("LastName")
                                        .IsRequired()
                                        .HasColumnType("text");

                                    b2.Property<int>("YearOfBirth")
                                        .HasColumnType("integer");

                                    b2.HasKey("SubstitutionCrewChangeId", "SubstitutionId");

                                    b2.ToTable("Substitution");

                                    b2.WithOwner()
                                        .HasForeignKey("SubstitutionCrewChangeId", "SubstitutionId");
                                });

                            b1.OwnsOne("DigiRega.Server.Model.Athlete", "Old", b2 =>
                                {
                                    b2.Property<int>("SubstitutionCrewChangeId")
                                        .HasColumnType("integer");

                                    b2.Property<int>("SubstitutionId")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer")
                                        .UseIdentityByDefaultColumn();

                                    b2.Property<string>("FirstName")
                                        .IsRequired()
                                        .HasColumnType("text");

                                    b2.Property<string>("LastName")
                                        .IsRequired()
                                        .HasColumnType("text");

                                    b2.Property<int>("YearOfBirth")
                                        .HasColumnType("integer");

                                    b2.HasKey("SubstitutionCrewChangeId", "SubstitutionId");

                                    b2.ToTable("Substitution");

                                    b2.WithOwner()
                                        .HasForeignKey("SubstitutionCrewChangeId", "SubstitutionId");
                                });

                            b1.Navigation("New")
                                .IsRequired();

                            b1.Navigation("Old")
                                .IsRequired();
                        });

                    b.Navigation("Substitutions");
                });

            modelBuilder.Entity("DigiRega.Server.Model.LateEntry", b =>
                {
                    b.OwnsMany("DigiRega.Server.Model.CrewMember", "Crew", b1 =>
                        {
                            b1.Property<int>("LateEntryId")
                                .HasColumnType("integer");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer")
                                .UseIdentityByDefaultColumn();

                            b1.Property<int>("Position")
                                .HasColumnType("integer");

                            b1.HasKey("LateEntryId", "Id");

                            b1.ToTable("CrewMember");

                            b1.WithOwner()
                                .HasForeignKey("LateEntryId");

                            b1.OwnsOne("DigiRega.Server.Model.Athlete", "Athlete", b2 =>
                                {
                                    b2.Property<int>("CrewMemberLateEntryId")
                                        .HasColumnType("integer");

                                    b2.Property<int>("CrewMemberId")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer")
                                        .UseIdentityByDefaultColumn();

                                    b2.Property<string>("FirstName")
                                        .IsRequired()
                                        .HasColumnType("text");

                                    b2.Property<string>("LastName")
                                        .IsRequired()
                                        .HasColumnType("text");

                                    b2.Property<int>("YearOfBirth")
                                        .HasColumnType("integer");

                                    b2.HasKey("CrewMemberLateEntryId", "CrewMemberId");

                                    b2.ToTable("CrewMember");

                                    b2.WithOwner()
                                        .HasForeignKey("CrewMemberLateEntryId", "CrewMemberId");
                                });

                            b1.Navigation("Athlete")
                                .IsRequired();
                        });

                    b.Navigation("Crew");
                });

            modelBuilder.Entity("DigiRega.Server.Model.Manager", b =>
                {
                    b.HasOne("DigiRega.Server.Model.Club", "Club")
                        .WithMany("Managers")
                        .HasForeignKey("ClubId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Club");
                });

            modelBuilder.Entity("DigiRega.Server.Model.Club", b =>
                {
                    b.Navigation("Managers");
                });
#pragma warning restore 612, 618
        }
    }
}
