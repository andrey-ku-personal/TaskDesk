namespace TaskDesk.Migrations.Migrations;

[CustomMigration(1, 3, "Add column 'Status' in table 'Project'")]
[Tags("TaskDesk")]
public class AddProjectStatus : Migration
{
    public override void Up()
    {
        Create.Column("Status").OnTable("Project").AsInt32().NotNullable().WithDefaultValue((int)SharedModel.Enums.ProjectStatus.Active);
    }

    public override void Down()
    {
        Delete.Column("Status").FromTable("Project");
    }
}