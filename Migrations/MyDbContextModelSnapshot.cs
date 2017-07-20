using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using iSnippets.Models;

namespace iSnippets.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("iSnippets.Models.Snippet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("snipDesc")
                        .IsRequired();

                    b.Property<string>("snipLang")
                        .IsRequired();

                    b.Property<string>("snipText")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("SnippetTable");
                });
        }
    }
}
