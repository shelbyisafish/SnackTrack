using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnackTrackDataAccessLayer
{
    /// <summary>
    /// An attribute to contain the list of possible database column names a property could be mapped to.
    /// Names are attempted to be mapped consecutively so the name you want most should go first.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MatchingDatabaseField : Attribute
    {
        /// <summary>
        /// The list of strings representing possible database column names to map this property to.
        /// Tries to find a column in the result dataset to map to. If more than one, maps to the first one.
        /// 
        /// Named parameter, i.e. required.
        /// </summary>
        public List<string> ColumnNames { get; protected set; }

        /// <summary>
        /// The boolean value representing whether or not this property must be mapped.
        /// Throws an exception (ValueNotFoundException) if IsRequired = true and the property was not mapped.
        /// 
        ///  Default value: true.
        /// Positional parameter, i.e. optional.
        /// </summary>
        public bool IsRequired { get; set; }


        /*
         * Removed as unnecessary. Could be implemented later, if it would be used.
            /// <summary>
            /// The boolean value representing whether or not this property can be mapped to a column with a type that can be implicitly converted to its own type.
            /// Throws an exception () if AllowImplicitConversion = false and the column data type does not exactly match the property's data type.
            /// 
            /// For example, a data column of type int would be implicitly mapped to a property of type double.
            /// 
            /// Default value: true;
            /// Positional parameter, i.e. optional.
            /// </summary>
            public bool AllowImplicitConversion { get; set; }         
        */





        public MatchingDatabaseField(string Name)
        {
            if (String.IsNullOrEmpty(Name))
                throw new ArgumentNullException("Must provide at least one column name for MatchingDatabaseField.", "Name");

            ColumnNames = new List<string>() { Name };
            IsRequired = true;
        }

        public MatchingDatabaseField(params string[] Names)
        {
            // Clean invalid inputs
            List<string> NamesList = Names.ToList();
            NamesList.RemoveAll(x => String.IsNullOrEmpty(x));

            if (NamesList.Count == 0)
                throw new ArgumentNullException("Must provide at least one column name for MatchingDatabaseField.", "Names");

            ColumnNames = new List<string>(Names);
            IsRequired = true;
        }
    }
}
