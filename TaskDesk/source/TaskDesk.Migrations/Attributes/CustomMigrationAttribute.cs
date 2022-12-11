namespace TaskDesk.Migrations.Attributes;

public class CustomMigrationAttribute : MigrationAttribute
{
    public CustomMigrationAttribute(int version, int changeNumber, string description)
       : base(SetVersion(version, changeNumber), description)
    {
    }

    private static long SetVersion(int version, int changeNumber)
    {
        return (version * 1000000L) + changeNumber;
    }
}