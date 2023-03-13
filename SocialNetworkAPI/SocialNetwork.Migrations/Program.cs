using FluentMigrator.Runner;
using FluentMigrator.Runner.Processors;
using FluentMigrator.Runner.VersionTableInfo;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static void Main(string[] args)
    {
        var connString = args[0];
        var serviceProvider = CreateServices(connString);
        Migrate(serviceProvider);
    }

    private static IServiceProvider CreateServices(string connectionString)
    {
        return new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddMySql5()
                .WithGlobalConnectionString(connectionString)
                .WithGlobalCommandTimeout(TimeSpan.FromSeconds(300))
                .WithMigrationsIn(typeof(Program).Assembly)
                .WithVersionTable(new VersionTable())
            )
            .Configure<SelectingProcessorAccessorOptions>(cfg =>
            {
                cfg.ProcessorId = "MySQl5";
            })
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            .BuildServiceProvider(false);
    }

    private static void Migrate(IServiceProvider serviceProvider)
    {
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>() as MigrationRunner;
        Console.WriteLine("Aktualne migracje: ");
        runner.ListMigrations();

        Console.WriteLine("Wybierz akcje ([r]ollback / [m]igracja)");

        while (true)
        {
            var action = Console.ReadLine();

            if (action == "r")
            {
                Console.WriteLine("Podaj ilość migracji do cofnięcia: ");
                int numRollback = int.Parse(Console.ReadLine());

                var currentVersionInfo = runner.VersionLoader.VersionInfo;
                var currentVersion = currentVersionInfo.Latest();
                var migrations = currentVersionInfo.AppliedMigrations().ToList();
                runner.MigrateDown(migrations.Skip(numRollback).FirstOrDefault());
                Console.WriteLine($"Rollback udany");
                break;
            }
            else if (action == "m")
            {
                runner.MigrateUp();
                Console.WriteLine("Migracja udana");
                break;
            }
        }

        runner.ListMigrations();
        Console.Read();
    }

    #region VersionTable
    public class VersionTable : IVersionTableMetaData
    {
        public object ApplicationContext { get; set; }
        public bool OwnsSchema => false;
        public string SchemaName => "";
        public string TableName => "versioninfo";
        public string ColumnName => "Version";
        public string UniqueIndexName => "UC_Version";
        public string AppliedOnColumnName => "AppliedOn";
        public string DescriptionColumnName => "Description";
    }
    #endregion
}