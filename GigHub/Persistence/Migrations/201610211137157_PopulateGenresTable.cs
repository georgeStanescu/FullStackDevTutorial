namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenresTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO GENRES(ID, NAME) VALUES (1, 'Jazz')");
            Sql("INSERT INTO GENRES(ID, NAME) VALUES (2, 'Blues')");
            Sql("INSERT INTO GENRES(ID, NAME) VALUES (3, 'Rock')");
            Sql("INSERT INTO GENRES(ID, NAME) VALUES (4, 'Country')");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM GENRES WHERE ID IN (1,2,3,4)");
        }
    }
}
