using System.Linq;
using Dapper.Linq.Configuration;
using Dapper.Linq.Core;
using Dapper.Linq.Mappers;
using Dapper.Linq.Queries;
using Dapper.Linq.Tests.Stubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

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
		public void GenerateSelectQuery()
		{
			var context = Mock.Of<IStorageContext>();
			var storage = new Storage(context);
			var provider = new QueryProvider<FakeEntity>(context);

			var queryable = storage
				.Select<FakeEntity>()
				.Where(u => u.Id == 1)
				.OrderByDescending(u => u.Name)
				.Take(20);

			var query = new Query<FakeEntity>(provider, queryable.Expression);
			var actual = query.ToString();
			var expected =
"SELECT id, name, valid " +
"FROM schema.table " +
"WHERE (id = 1) " +
"ORDER BY name DESC " +
"LIMIT 20";

			Assert.AreEqual(expected, actual);
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
$"WHERE (id = {entity.Id})";

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
