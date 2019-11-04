using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SnackTrackDataAccessLayer;
using System.Data;
using System.Collections.Generic;

namespace SnackTrackDALTests
{
    [TestClass]
    public class DALMapTest
    {
        private DataTable UninitializedDataTable;
        private DataTable EmptyDataTable = new DataTable();
        private DataTable SimpleDataTable;
        private DataTable NullValueDataTable;

        [TestInitialize]
        public void PopulateDataTables()
        {
            SimpleDataTable = new DataTable();
            SimpleDataTable.Columns.Add("Property", typeof(int));
            SimpleDataTable.Rows.Add(new object[] { 1 });
            SimpleDataTable.Rows.Add(new object[] { 2 });
            SimpleDataTable.Rows.Add(new object[] { 3 });
            SimpleDataTable.Rows.Add(new object[] { 4 });

            NullValueDataTable = new DataTable();
            NullValueDataTable.Columns.Add("MyColumnName", typeof(int));
            NullValueDataTable.Rows.Add(new object[] { 1 });
            NullValueDataTable.Rows.Add(new object[] { DBNull.Value });
            NullValueDataTable.Rows.Add(new object[] { null });
        }




        // Test the DataTable (i.e. the records returned from the query).

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MapDataTableToList_ShouldThrowException_WhenGivenNull()
        {
            DALMap<SimpleClass> dALMap = new DALMap<SimpleClass>();
            List<SimpleClass> testList = dALMap.Map(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MapDataTableToList_ShouldThrowException_WhenGivenUninitializedDataTable()
        {
            DALMap<SimpleClass> dALMap = new DALMap<SimpleClass>();
            List<SimpleClass> testList = dALMap.Map(UninitializedDataTable);
        }

        [TestMethod]
        public void MapDataTableToList_ShouldReturnEmpty_WhenNoRecordsInDataTable()
        {
            DALMap<SimpleClassWithAttribute> dALMap = new DALMap<SimpleClassWithAttribute>();
            List<SimpleClassWithAttribute> simpleClassWithAttributeList = dALMap.Map(EmptyDataTable);
        }




        // Test the mapped class's properties

        [TestMethod]
        public void MapDataTableToList_ShouldReturnEmpty_WhenNoPropertiesToMap()
        {
            DALMap<SimpleClass> dALMap = new DALMap<SimpleClass>();
            List<SimpleClass> SimpleClassList = dALMap.Map(SimpleDataTable);
        }




        // Test negative runtime calculations

        [TestMethod]
        [ExpectedException(typeof(ValueNotFoundException))]
        public void MapDataTableToList_ShouldThrowException_WhenRequiredPropertyNotMapped()
        {
            // SimpleClassWithAttirbute has name "MyColumnName" (required by default), but SimpleDataTable has "Property".

            DALMap<SimpleClassWithAttribute> dALMap = new DALMap<SimpleClassWithAttribute>();
            List<SimpleClassWithAttribute> SimpleClassWithAttirbuteList = dALMap.Map(SimpleDataTable);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void MapDataTableToList_ShouldThrowException_WhenNullFoundForNonNullableProperty()
        {
            // SimpleClassWithAttirbute has name "MyColumnName" (type int), but NullValueDataTable has "MyColumnName" which contains DBNull.Value and null.

            DALMap<SimpleClassWithAttribute> dALMap = new DALMap<SimpleClassWithAttribute>();
            List<SimpleClassWithAttribute> SimpleClassWithAttirbuteList = dALMap.Map(NullValueDataTable);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void MapDataTableToList_ShouldThrowException_WhenDataTypeDoesntMatch()
        {
            DataTable MyDataTable = new DataTable();
            MyDataTable.Columns.Add("MyColumnName", typeof(int));
            MyDataTable.Columns.Add("MySecondColumnName", typeof(char));
            MyDataTable.Rows.Add(new object[] { 1, 'x' });
            MyDataTable.Rows.Add(new object[] { 2, 'x' });

            // ExampleClass has name "MySecondColumnName" (type double), but MyDataTable has "MySecondColumnName" (type char).

            DALMap<ExampleClass1> dALMap = new DALMap<ExampleClass1>();
            List<ExampleClass1> mappedList = dALMap.Map(MyDataTable);
        }




        // Test positive runtime calculations

        [TestMethod]
        public void MapDataTableToList_ShouldReturnEmpty_WhenNoMatchingColumnsFound()
        {
            // SimpleClassWithOptionalAttirbute has name "MyColumnName" (IsRequired = false), but SimpleDataTable has "Property".

            DALMap<SimpleClassWithOptionalAttribute> dALMap = new DALMap<SimpleClassWithOptionalAttribute>();
            List<SimpleClassWithOptionalAttribute> SimpleClassWithOptionalAttirbuteFalseList = dALMap.Map(SimpleDataTable);

            Assert.AreEqual(0, SimpleClassWithOptionalAttirbuteFalseList.Count);
        }

        [TestMethod]
        public void MapDataTableToList_ShouldReturnList_WhenDataDoesImplicitConversion()
        {
            DataTable MyDataTable = new DataTable();
            MyDataTable.Columns.Add("MyColumnName", typeof(int));
            MyDataTable.Columns.Add("MySecondColumnName", typeof(int));
            MyDataTable.Rows.Add(new object[] { 1, -1 });
            MyDataTable.Rows.Add(new object[] { 2, -1 });

            // ExampleClass has name "MySecondColumnName" (type double), and MyDataTable has "MySecondColumnName" (type int). Implicit conversion: int -> double.

            DALMap<ExampleClass1> dALMap = new DALMap<ExampleClass1>();
            List<ExampleClass1> mappedList = dALMap.Map(MyDataTable);

            Assert.AreEqual(2, mappedList.Count);
        }

        [TestMethod]
        public void MapDataTableToList_ShouldReturnList_WhenNullFoundForNullableProperty()
        {
            // SimpleClassWithNullableAttribute has name "MyColumnName" (type int?), and NullValueDataTable has "MyColumnName" which contains DBNull.Value and null.

            DALMap<SimpleClassWithNullableAttribute> dALMap = new DALMap<SimpleClassWithNullableAttribute>();
            List<SimpleClassWithNullableAttribute> mappedList = dALMap.Map(NullValueDataTable);

            Assert.AreEqual(3, mappedList.Count);
        }

        [TestMethod]
        public void MapDataTableToList_ShouldReturnList_WhenNameAndTypeMatch()
        {
            DataTable MyDataTable = new DataTable();
            MyDataTable.Columns.Add("MyColumnName", typeof(int));
            MyDataTable.Rows.Add(new object[] { 1 });
            MyDataTable.Rows.Add(new object[] { 2 });

            // SimpleClassWithAttirbute has name "MyColumnName", and MyDataTable has "MyColumnName".

            DALMap<SimpleClassWithAttribute> dALMap = new DALMap<SimpleClassWithAttribute>();
            List<SimpleClassWithAttribute> mappedList = dALMap.Map(MyDataTable);

            Assert.AreEqual(2, mappedList.Count);
        }

        [TestMethod]
        public void MapDataTableToList_ShouldReturnList_WhenGivenExtraColumns()
        {
            DataTable MyDataTable = new DataTable();
            MyDataTable.Columns.Add("MyColumnName", typeof(int));
            MyDataTable.Columns.Add("MySecondColumnName", typeof(int));
            MyDataTable.Columns.Add("IrrelevantColumn", typeof(bool));
            MyDataTable.Rows.Add(new object[] { 1, 10, false });
            MyDataTable.Rows.Add(new object[] { 2, 11, false });
            MyDataTable.Rows.Add(new object[] { 3, 12, false });

            // ExampleClass has extra unmapped property "Property".
            // MyDataTable has extra unmapped column "IrrelevantColumn".

            DALMap<ExampleClass1> dALMap = new DALMap<ExampleClass1>();
            List<ExampleClass1> mappedList = dALMap.Map(MyDataTable);

            Assert.AreEqual(3, mappedList.Count);
        }

        [TestMethod]
        public void MapDataTableToList_ShouldGetFirstMatchingNameOnly()
        {
            DataTable MyDataTable = new DataTable();
            MyDataTable.Columns.Add("MyColumnName", typeof(int));
            MyDataTable.Columns.Add("SearchName3", typeof(string));     // Ignore this
            MyDataTable.Columns.Add("SearchName2", typeof(string));     // Find this
            MyDataTable.Rows.Add(new object[] { 1, 10, -1 });
            MyDataTable.Rows.Add(new object[] { 2, 11, -1 });

            DALMap<ExampleClass2> dALMap = new DALMap<ExampleClass2>();
            List<ExampleClass2> mappedList = dALMap.Map(MyDataTable);

            Assert.AreEqual(2, mappedList.Count);
            Assert.AreEqual("11", mappedList[1].PropertyWithManyNames);
        }

    }
}
