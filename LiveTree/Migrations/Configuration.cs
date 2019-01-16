namespace LiveTree.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using LiveTree.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<LiveTree.Models.LiveTreeContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LiveTree.Models.LiveTreeContext context)
        {
            context.Factories.AddOrUpdate(x => x.Id,
                new Factory() { Id=1,Name="Chocolate",Minimum=1,Maximum=10,NumberOfNodes=3}
                );
            context.Nodes.AddOrUpdate(x => x.Id,
                new Node() { Id=1, FactoryId =1, Name="3"},
                 new Node() { Id = 1, FactoryId = 1, Name = "4" },
                  new Node() { Id = 1, FactoryId = 1, Name = "5" }
                );
        }
    }
}
