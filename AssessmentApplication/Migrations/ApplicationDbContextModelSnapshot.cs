﻿// <auto-generated />
using AssessmentApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AssessmentApplication.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AssessmentApplication.Models.AttachmentModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("FeedbackModelId")
                        .HasColumnType("integer");

                    b.Property<byte[]>("FileData")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FeedbackModelId")
                        .IsUnique();

                    b.ToTable("Attachments", (string)null);
                });

            modelBuilder.Entity("AssessmentApplication.Models.FeebackModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AdditionalComment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TypeFeedback")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("FeedBack", (string)null);
                });

            modelBuilder.Entity("AssessmentApplication.Models.FeedbackService", b =>
                {
                    b.Property<int>("FeedbackModelId")
                        .HasColumnType("integer");

                    b.Property<int>("ServiceModelId")
                        .HasColumnType("integer");

                    b.HasKey("FeedbackModelId", "ServiceModelId");

                    b.HasIndex("ServiceModelId");

                    b.ToTable("FeedbackService");
                });

            modelBuilder.Entity("AssessmentApplication.Models.ServiceModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Services", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Service A"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Service B"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Service C"
                        });
                });

            modelBuilder.Entity("AssessmentApplication.Models.AttachmentModel", b =>
                {
                    b.HasOne("AssessmentApplication.Models.FeebackModel", "FeedbackModel")
                        .WithOne("DocumentAttachments")
                        .HasForeignKey("AssessmentApplication.Models.AttachmentModel", "FeedbackModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FeedbackModel");
                });

            modelBuilder.Entity("AssessmentApplication.Models.FeedbackService", b =>
                {
                    b.HasOne("AssessmentApplication.Models.FeebackModel", "FeedbackModel")
                        .WithMany("FeedbackServices")
                        .HasForeignKey("FeedbackModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AssessmentApplication.Models.ServiceModel", "ServiceModel")
                        .WithMany("FeedbackServices")
                        .HasForeignKey("ServiceModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FeedbackModel");

                    b.Navigation("ServiceModel");
                });

            modelBuilder.Entity("AssessmentApplication.Models.FeebackModel", b =>
                {
                    b.Navigation("DocumentAttachments")
                        .IsRequired();

                    b.Navigation("FeedbackServices");
                });

            modelBuilder.Entity("AssessmentApplication.Models.ServiceModel", b =>
                {
                    b.Navigation("FeedbackServices");
                });
#pragma warning restore 612, 618
        }
    }
}
