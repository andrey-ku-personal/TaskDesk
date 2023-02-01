namespace TaskDesk.Migrations.Migrations;

[CustomMigration(1, 2, "Create 'Project' table")]
[Tags("TaskDesk")]
public class CreateProjectTable : AutoReversingMigration
{
    public override void Up()
    {
        Create.Table("Project")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Name").AsString(128).NotNullable()
            .WithColumn("Description").AsString(2024).Nullable();
    }
}