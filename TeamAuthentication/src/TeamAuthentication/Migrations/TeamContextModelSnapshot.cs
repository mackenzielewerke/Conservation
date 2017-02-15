using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TeamAuthentication.Models;

namespace TeamAuthentication.Migrations
{
    [DbContext(typeof(TeamContext))]
    partial class TeamContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TeamAuthentication.Models.Animal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Gender");

                    b.Property<string>("Name");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.ToTable("Animals");
                });

            modelBuilder.Entity("TeamAuthentication.Models.Circus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AnimalsId");

                    b.Property<string>("Name");

                    b.Property<int?>("PerformersId");

                    b.Property<int?>("TicketsId");

                    b.HasKey("Id");

                    b.HasIndex("AnimalsId");

                    b.HasIndex("PerformersId");

                    b.HasIndex("TicketsId");

                    b.ToTable("Circuses");
                });

            modelBuilder.Entity("TeamAuthentication.Models.Performer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Act");

                    b.Property<string>("Gender");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Performers");
                });

            modelBuilder.Entity("TeamAuthentication.Models.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Price");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("TeamAuthentication.Models.Circus", b =>
                {
                    b.HasOne("TeamAuthentication.Models.Animal", "Animals")
                        .WithMany()
                        .HasForeignKey("AnimalsId");

                    b.HasOne("TeamAuthentication.Models.Performer", "Performers")
                        .WithMany()
                        .HasForeignKey("PerformersId");

                    b.HasOne("TeamAuthentication.Models.Ticket", "Tickets")
                        .WithMany()
                        .HasForeignKey("TicketsId");
                });
        }
    }
}
