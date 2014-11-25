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
                new AppRole("Root"),
                new AppRole("Admin"),
                new AppRole("User"),
                new AppRole("Moderator")
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
