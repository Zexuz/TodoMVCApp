using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Todo.Domain.Repository;

namespace Todo.Domain.Migrations
{
    [DbContext(typeof(TodoContext))]
    partial class TodoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Todo.Domain.Todo.Checklist.TodoChecklist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("LastEdit");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("TodoChecklists");
                });

            modelBuilder.Entity("Todo.Domain.Todo.Checklist.TodoCheckListItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Checked");

                    b.Property<string>("Text");

                    b.Property<int?>("TodoChecklistId");

                    b.HasKey("Id");

                    b.HasIndex("TodoChecklistId");

                    b.ToTable("TodoCheckListItem");
                });

            modelBuilder.Entity("Todo.Domain.Todo.Note.TodoNote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("LastEdit");

                    b.Property<string>("Note");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("TodoNotes");
                });

            modelBuilder.Entity("Todo.Domain.Todo.Checklist.TodoCheckListItem", b =>
                {
                    b.HasOne("Todo.Domain.Todo.Checklist.TodoChecklist")
                        .WithMany("CheckList")
                        .HasForeignKey("TodoChecklistId");
                });
        }
    }
}
