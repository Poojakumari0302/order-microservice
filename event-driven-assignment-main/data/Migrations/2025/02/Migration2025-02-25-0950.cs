using FluentMigrator;

namespace Migrations._2025._02
{
    [Migration(202502250950, "Initial orders table")]
    public class Migration202502250950 : Migration
    {
        public override void Up()
        {
            Create.Table("Orders")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("OrderNumber").AsString();
        }

        public override void Down()
        {
            Delete.Table("Orders");
        }
    }
}