using Dapper.Linq.Configuration;
using Dapper.Linq.Mappers;
using Dapper.Linq.Queries;
using Dapper.Linq.Tests.Stubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dapper.Linq.Tests
{
	[TestClass]
	public class QueryTests
	{
		[TestInitialize]
		public void Setup()
		{
			DapperConfiguration.Create()
				.UseMapper<AttributeEntityMapper>();
		}

		[TestMethod]
		public void GenerateInsertQuery()
		{
			var entity = new FakeEntity();
			var query = new InsertQuery<FakeEntity>(entity);

			var actual = query.Token.Value;
			var expected =
"INSERT INTO schema.table (id, name, valid) " +
"VALUES (@id, @name, @valid)";

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void GenerateUpdateQuery()
		{
			var entity = new FakeEntity { Id = 1 };
			var query = new UpdateQuery<FakeEntity>(entity);

			var actual = query.Token.Value;
			var expected =
"UPDATE schema.table " +
"SET id = @id, name = @name, valid = @valid " +
"WHERE (id = 1)";

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void GenerateDeleteQuery()
		{
			var entity = new FakeEntity();
			var query = new DeleteQuery<FakeEntity>(entity);

			var actual = query.Token.Value;
			var expected =
"DELETE FROM schema.table " +
"WHERE (id = @id AND name = @name AND valid = @valid)";

			Assert.AreEqual(expected, actual);
		}
	}
}
