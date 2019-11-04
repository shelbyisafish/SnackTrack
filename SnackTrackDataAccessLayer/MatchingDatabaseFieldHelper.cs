using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SnackTrackDataAccessLayer
{
    public class MatchingDatabaseFieldHelper
    {
        /// <summary>
        /// Gets the MatchingDatabaseField.ColumnName from the given PropertyInfo.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static List<string> GetMatchingDatabaseFieldColumnNames(PropertyInfo property)
        {
            MatchingDatabaseField matchingDatabaseFieldAttribute = GetMatchingDatabaseFieldAttribute(property);

            return matchingDatabaseFieldAttribute.ColumnNames;
        }



        /// <summary>
        /// Gets the MatchingDatabaseField.IsRequired from the given PropertyInfo.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static bool GetMatchingDatabaseFieldIsRequired(PropertyInfo property)
        {
            MatchingDatabaseField matchingDatabaseFieldAttribute = GetMatchingDatabaseFieldAttribute(property);

            return matchingDatabaseFieldAttribute.IsRequired;
        }


        /// <summary>
        /// Find custom attribute that is a MatchingDatabaseField in the property.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        private static MatchingDatabaseField GetMatchingDatabaseFieldAttribute(PropertyInfo property)
        {
            if (property == null)
                throw new ArgumentNullException("The property parameter is a null reference. Expected PropertyInfo.", "property");

            object[] matchingDatabaseFieldAttributes = property.GetCustomAttributes(typeof(MatchingDatabaseField), false);

            // Note: matchingDatabaseFieldAttributes.Length is expected to be 1.
            //       Throw exception if length is 0 (i.e. attribute not found).
            //       Test MatchingDatabaseFieldTest.MatchingDatabaseFieldAttribute_ShouldNotAllowMultiple() should not pass if length is > 1.
            if (matchingDatabaseFieldAttributes.Length == 0)
                throw new ArgumentException("The PropertyInfo provided does not have a MatchingDatabaseField Attribute.", "property");
            //else if (matchingDatabaseFieldAttributes.Length > 1)
            //    throw new InvalidOperationException("The MatchingDatabaseField Attribute should not allow multiple.");

            return (MatchingDatabaseField)matchingDatabaseFieldAttributes[0];
        }
    }
}
