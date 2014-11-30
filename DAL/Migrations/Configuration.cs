using System;

namespace DAL.Migrations
{
    using System.Data.Entity.Migrations;
    using DAL.DomainModel;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.EntityDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DAL.EntityDbContext context)
        {
            context.Rubrics.AddOrUpdate(
                new Rubric() { Id = 1, Name = "Здоровье" },
                new Rubric() { Id = 2, Name = "Мотивация" },
                new Rubric() { Id = 3, Name = "Новичку" },
                new Rubric() { Id = 4, Name = "Общее" },
                new Rubric() { Id = 5, Name = "Питание" },
                new Rubric() { Id = 6, Name = "С чего начать" },
                new Rubric() { Id = 7, Name = "Упражнения" },
                new Rubric() { Id = 8, Name = "Элементы" }
            );

            context.Roles.AddOrUpdate(
                new AppRole("Moderator") { Id = "12994707-e011-4910-9261-0bfa9820b176" },
                new AppRole("Admin") { Id = "2798a2f1-44d7-4e41-bcac-5dcc4e96ca69" },
                new AppRole("Root") { Id = "3541400f-1812-4126-9d5a-380e8a18ba80" },
                new AppRole("User") { Id = "6c66a0c3-075b-4089-b338-af857d2f49df" }
            );

            context.Products.AddOrUpdate(
                new Product
                {
                    Id = 1,
                    Cost = 100,
                    Label = "Подписка на месяц",
                    Currency = "RUB",
                    Lang = "ru-RU",
                }
            );
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
