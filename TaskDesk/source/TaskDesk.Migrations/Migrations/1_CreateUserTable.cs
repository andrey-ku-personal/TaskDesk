namespace TaskDesk.Migrations.Migrations;

[CustomMigration(1, 1, "Create 'User' table")]
[Tags("TaskDesk")]
public class CreateUserTable : Migration
{
    public override void Up()
    {
        Create.Table("User")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("UserName").AsString(128).NotNullable()
            .WithColumn("FirstName").AsString(128).NotNullable()
            .WithColumn("LastName").AsString(128).NotNullable()
            .WithColumn("Email").AsString(256).NotNullable()
            .WithColumn("PasswordHash").AsString(128).NotNullable()
            .WithColumn("CreateTime").AsDateTime().NotNullable()
            .WithColumn("LastLoginTime").AsDateTime().NotNullable()
            .WithColumn("Website").AsString(1024).Nullable()
            .WithColumn("Description").AsString(2024).Nullable();

        Create.UniqueConstraint("UX_User_UserName").OnTable("User").Column("UserName");
    }

    public override void Down()
    {
        Delete.UniqueConstraint("UX_User_UserName");
        Delete.Table("User");
    }
}