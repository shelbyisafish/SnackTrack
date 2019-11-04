using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnackTrackDataAccessLayer;

namespace SnackTrackDALTests
{
    // "Simple" designates that the class has only one property.


    internal class SimpleClass
    {
        public int Property { get; set; }
    }

    internal class SimpleClassNullableProperty
    {
        public int? NullableProperty { get; set; }
    }

    internal class SimpleClassWithAttribute
    {
        [MatchingDatabaseField("MyColumnName")]
        public int PropertyWithAttribute { get; set; }
    }

    internal class SimpleClassWithNullableAttribute
    {
        [MatchingDatabaseField("MyColumnName")]
        public int? PropertyWithAttribute { get; set; }
    }

    internal class SimpleClassWithEmptyAttribute
    {
        [MatchingDatabaseField()]
        public int PropertyWithAttribute { get; set; }
    }

    internal class SimpleClassWithNullAttribute
    {
        [MatchingDatabaseField(null, null)]
        public int PropertyWithAttribute { get; set; }
    }

    internal class SimpleClassWithEmptyStringAttribute
    {
        [MatchingDatabaseField("")]
        public int PropertyWithAttribute { get; set; }
    }

    internal class SimpleClassWithOptionalAttribute
    {
        [MatchingDatabaseField("MyColumnName", IsRequired = false)]
        public int PropertyWithAttribute { get; set; }
    }

    //internal class SimpleClassWithMultipleAttirbute
    //{
    //    [MatchingDatabaseField("MyColumnName")]
    //    [MatchingDatabaseField("MyOtherColumnName")]
    //    public int PropertyWithAttribute { get; set; }
    //}


    internal class ExampleClass1
    {
        [MatchingDatabaseField("MyColumnName")]
        public int PropertyWithAttribute { get; set; }

        [MatchingDatabaseField("MySecondColumnName")]
        public double SecondPropertyWithAttribute { get; set; }
    }


    internal class ExampleClass2
    {
        public int Property { get; set; }

        [MatchingDatabaseField("MyColumnName")]
        public int PropertyWithAttribute { get; set; }

        [MatchingDatabaseField("SearchName1", "SearchName2", "SearchName3")]
        public string PropertyWithManyNames { get; set; }
    }

}
