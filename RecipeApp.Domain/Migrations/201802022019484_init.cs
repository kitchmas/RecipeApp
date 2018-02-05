namespace RecipeApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        IngredientID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.IngredientID);
            
            CreateTable(
                "dbo.RecipeIngredients",
                c => new
                    {
                        RecipeIngredientID = c.Int(nullable: false, identity: true),
                        RecipeID = c.Int(nullable: false),
                        IngredientID = c.Int(nullable: false),
                        UnitOfMeasurementID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RecipeIngredientID)
                .ForeignKey("dbo.Ingredients", t => t.IngredientID, cascadeDelete: true)
                .ForeignKey("dbo.Recipes", t => t.RecipeID, cascadeDelete: true)
                .ForeignKey("dbo.UnitOfMeasurements", t => t.UnitOfMeasurementID, cascadeDelete: true)
                .Index(t => t.RecipeID)
                .Index(t => t.IngredientID)
                .Index(t => t.UnitOfMeasurementID);
            
            CreateTable(
                "dbo.Recipes",
                c => new
                    {
                        RecipeID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Method = c.String(),
                    })
                .PrimaryKey(t => t.RecipeID);
            
            CreateTable(
                "dbo.UnitOfMeasurements",
                c => new
                    {
                        UnitOfMeasurementID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.UnitOfMeasurementID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RecipeIngredients", "UnitOfMeasurementID", "dbo.UnitOfMeasurements");
            DropForeignKey("dbo.RecipeIngredients", "RecipeID", "dbo.Recipes");
            DropForeignKey("dbo.RecipeIngredients", "IngredientID", "dbo.Ingredients");
            DropIndex("dbo.RecipeIngredients", new[] { "UnitOfMeasurementID" });
            DropIndex("dbo.RecipeIngredients", new[] { "IngredientID" });
            DropIndex("dbo.RecipeIngredients", new[] { "RecipeID" });
            DropTable("dbo.UnitOfMeasurements");
            DropTable("dbo.Recipes");
            DropTable("dbo.RecipeIngredients");
            DropTable("dbo.Ingredients");
        }
    }
}
