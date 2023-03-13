using FluentMigrator;

namespace SocialNetwork.Migrations
{
    [Migration(210520221339)]
    public class AddTestTable : Migration
    {
        public override void Down()
        {
            //Delete.Table("Test");
        }

        public override void Up()
        {
            //Create.Table("Test")
            //    .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            //    .WithColumn("SomeStringColumn").AsString()
            //    .WithColumn("SomeBoolColumn").AsBoolean();
        }
    }
}
