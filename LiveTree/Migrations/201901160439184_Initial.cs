namespace LiveTree.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Factories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Minimum = c.Int(nullable: false),
                        Maximum = c.Int(nullable: false),
                        NumberOfNodes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Nodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        FactoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Factories", t => t.FactoryId, cascadeDelete: true)
                .Index(t => t.FactoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Nodes", "FactoryId", "dbo.Factories");
            DropIndex("dbo.Nodes", new[] { "FactoryId" });
            DropTable("dbo.Nodes");
            DropTable("dbo.Factories");
        }
    }
}
