﻿using FluentMigrator.Expressions;
using FluentMigrator.Runner.Generators;
using FluentMigrator.Runner.Generators.Oracle;
using NUnit.Framework;
using NUnit.Should;

namespace FluentMigrator.Tests.Unit.Generators.Oracle
{
    public class OracleDropTableTests : BaseTableDropTests
    {
        private OracleGenerator _generator;

        [SetUp]
        public void Setup()
        {
            _generator = new OracleGenerator();
        }

        [Test]
        public override void CanDropColumn()
        {
            var expression = GeneratorTestHelper.GetDeleteColumnExpression();
            string sql = _generator.Generate(expression);
            sql.ShouldBe("ALTER TABLE TestTable1 DROP COLUMN TestColumn1");
        }

        [Test]
        public override void CanDropForeignKey()
        {
            var expression = GeneratorTestHelper.GetDeleteForeignKeyExpression();
            string sql = _generator.Generate(expression);
            sql.ShouldBe("ALTER TABLE TestTable1 DROP CONSTRAINT FK_Test");
        }

        [Test]
        public override void CanDropTable()
        {
            var expression = GeneratorTestHelper.GetDeleteTableExpression();
            string sql = _generator.Generate(expression);
            sql.ShouldBe("DROP TABLE TestTable1");
        }

        [Test]
        public override void CanDeleteIndex()
        {
            var expression = GeneratorTestHelper.GetDeleteIndexExpression();
            string sql = _generator.Generate(expression);
            sql.ShouldBe("DROP INDEX TestIndex");
        }

        [Test]
        public override void CanDeleteSchema()
        {
            var expression = new DeleteSchemaExpression();
            var result = _generator.Generate(expression);
            result.ShouldBe(string.Empty);
        }

        [Test]
        public void CanDeleteSchemaInStrictMode()
        {
            _generator.compatabilityMode = Runner.CompatabilityMode.STRICT;
            Assert.Throws<DatabaseOperationNotSupportedException>(() => _generator.Generate(new DeleteSchemaExpression()));
        }
    }
}