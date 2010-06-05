﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentCassandra.Types;

namespace FluentCassandra.Test.Operations
{
	[TestClass]
	public class GetColumnTest
	{
		private CassandraContext _db;
		private CassandraColumnFamily<AsciiType> _family;
		private CassandraSuperColumnFamily<AsciiType, AsciiType> _superFamily;
		private const string _testKey = "Test1";
		private const string _testName = "Test1";
		private const string _testSuperName = "SubTest1";

		[TestInitialize]
		public void TestInit()
		{
			_db = new CassandraContext("Testing", "localhost");
			_family = _db.GetColumnFamily<AsciiType>("Standard");
			_superFamily = _db.GetColumnFamily<AsciiType, AsciiType>("Super");

			_family.InsertColumn(_testKey, "Test1", Math.PI);
			_family.InsertColumn(_testKey, "Test2", Math.PI);
			_family.InsertColumn(_testKey, "Test3", Math.PI);

			_superFamily.InsertColumn(_testKey, _testSuperName, "Test1", Math.PI);
			_superFamily.InsertColumn(_testKey, _testSuperName, "Test2", Math.PI);
			_superFamily.InsertColumn(_testKey, _testSuperName, "Test3", Math.PI);
		}

		[TestCleanup]
		public void TestCleanup()
		{
			_db.Dispose();
		}

		[TestMethod]
		public void Standard_GetColumn()
		{
			// arrange
			double expected = Math.PI;

			// act
			var column = _family.GetColumn(_testKey, _testName);

			// assert
			Assert.AreEqual(_testName, (string)column.ColumnName);
			Assert.AreEqual(expected, (double)column.ColumnValue);
		}

		[TestMethod]
		public void Super_GetColumn()
		{
			// arrange
			double expected = Math.PI;

			// act
			var column = _superFamily.GetColumn(_testKey, _testSuperName, _testName);

			// assert
			Assert.AreEqual(_testName, (string)column.ColumnName);
			Assert.AreEqual(expected, (double)column.ColumnValue);
		}

		[TestMethod]
		public void Super_GetSuperColumn()
		{
			// arrange

			// act
			var column = _superFamily.GetSuperColumn(_testKey, _testSuperName);

			// assert
			Assert.AreEqual(_testSuperName, (string)column.ColumnName);
		}
	}
}
