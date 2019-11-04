using System;
using SnackTrackDataAccessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SnackTrackDALTests
{
    [TestClass]
    public class MatchingDatabaseFieldTest
    {
        // Notes on testing an Attribute:
        //
        // Per stackoverflow (https://stackoverflow.com/a/25590752):
        // The attribute is constructed only when you retrieve it (using the GetCustomAttribute function). 
        // Otherwise, its construction recipe (constructor overload + positional parameters + properties values) is only stored in the assembly metadata.

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MatchingDatabaseFieldAttribute_ShouldThrowException_WhenNoNameProvided()
        {
            typeof(SimpleClassWithEmptyAttribute).GetProperty("PropertyWithAttribute").GetCustomAttributes(typeof(MatchingDatabaseField), false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MatchingDatabaseFieldAttribute_ShouldThrowException_WhenEmptyStringProvided()
        {
            typeof(SimpleClassWithEmptyStringAttribute).GetProperty("PropertyWithAttribute").GetCustomAttributes(typeof(MatchingDatabaseField), false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MatchingDatabaseFieldAttribute_ShouldThrowException_WhenNullProvided()
        {
            typeof(SimpleClassWithNullAttribute).GetProperty("PropertyWithAttribute").GetCustomAttributes(typeof(MatchingDatabaseField), false);
        }

        [TestMethod]
        public void MatchingDatabaseFieldAttribute_ShouldNotAllowMultiple()
        {
            object[] attributeUsageAttribute = typeof(MatchingDatabaseField).GetCustomAttributes(typeof(AttributeUsageAttribute), false);

            Assert.IsFalse(((AttributeUsageAttribute)attributeUsageAttribute[0]).AllowMultiple);
        }
    }
}
