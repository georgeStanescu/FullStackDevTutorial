using NUnit.Framework;
using System.Data.Entity.Migrations;

namespace GigHub.IntegrationTests
{
    [SetUpFixture]
    public class GlobalSetup
    {
        [SetUp]
        public void Setup()
        {
            var configuration = new Migrations.Configuration();

            var migrator = new DbMigrator(configuration);

            migrator.Update();
        }
    }
}
