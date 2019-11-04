using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SnackTrackDataAccessLayer;
using System.Collections.Generic;
using System.Reflection;

namespace SnackTrackDALTests
{
    [TestClass]
    public class MatchingDatabaseFieldHelperTest
    {
        // Tests for getting MatchingDatabaseFieldAttribute from property

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetMatchingDatabaseFieldAttribute_ShouldThrowException_WhenGivenNullProperty()
        {
            try
            {
                PropertyInfo nullProperty = default;

                PrivateType pt = new PrivateType(typeof(MatchingDatabaseFieldHelper));
                pt.InvokeStatic("GetMatchingDatabaseFieldAttribute", nullProperty);
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetMatchingDatabaseFieldAttribute_ShouldThrowException_WhenGivenPropertyWithoutAttribute()
        {
            try
            {
                PropertyInfo[] simpleProperty = (typeof(SimpleClass)).GetProperties();

                PrivateType pt = new PrivateType(typeof(MatchingDatabaseFieldHelper));
                pt.InvokeStatic("GetMatchingDatabaseFieldAttribute", simpleProperty[0]);
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }

        }

        // Not possible to test
        //[TestMethod]
        //[ExpectedException(typeof(InvalidOperationException))]
        //public void GetMatchingDatabaseFieldAttribute_ShouldThrowException_WhenGivenPropertyWithMultipleAttribute()
        //{
        //    try
        //    {
        //        PropertyInfo[] simpleProperty = (typeof(SimpleClassWithMultipleAttirbute)).GetProperties();

        //        PrivateType pt = new PrivateType(typeof(MatchingDatabaseFieldHelper));
        //        pt.InvokeStatic("GetMatchingDatabaseFieldAttribute", simpleProperty[0]);
        //    }
        //    catch (TargetInvocationException ex)
        //    {
        //        throw ex.InnerException;
        //    }
        //}


        // Tests for getting ColumnNames from property

        [TestMethod]
        public void GetColumnNamesFromProperty_ShouldReturnList_WhenGivenPropertyWithAttribute()
        {
            PropertyInfo[] propertyWithAttribute = (typeof(SimpleClassWithAttribute)).GetProperties();

            List<string> result = MatchingDatabaseFieldHelper.GetMatchingDatabaseFieldColumnNames(propertyWithAttribute[0]);

            Assert.IsTrue(result.Count != 0);
        }



        // Tests for getting IsRequired from property

        [TestMethod]
        public void CheckPropertyIsRequired_ShouldReturnTrue_WhenGettingDefaultValue()
        {
            PropertyInfo[] propertyWithAttribute = typeof(SimpleClassWithAttribute).GetProperties();

            bool result = MatchingDatabaseFieldHelper.GetMatchingDatabaseFieldIsRequired(propertyWithAttribute[0]);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CheckPropertyIsRequired_ShouldReturnFalse_WhenSetToFalse()
        {
            PropertyInfo[] propertyWithAttribute = typeof(SimpleClassWithOptionalAttribute).GetProperties();

            bool result = MatchingDatabaseFieldHelper.GetMatchingDatabaseFieldIsRequired(propertyWithAttribute[0]);

            Assert.IsFalse(result);
        }

    }
}
