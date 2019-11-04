using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SnackTrackDataAccessLayer
{
    public class DALMap<T> where T : class, new()
    {
        /// <summary>
        /// Takes a DataTable and returns a List of <typeparamref name="T"/>. Class <typeparamref name="T"/> must have a public parameterless constructor.
        /// </summary>
        /// <param name="queryResultTable"></param>
        /// <returns></returns>
        public List<T> Map(DataTable queryResultTable)
        {
            if (queryResultTable == null)
                throw new ArgumentNullException("Cannot map null DataTable.", "queryResultTable");


            List<T> mappedObjects = new List<T>();


            // If no query results, return empty list.
            if (queryResultTable.Rows.Count == 0)
                return mappedObjects;


            List<string> queryResultColumnNames = queryResultTable.Columns.Cast<DataColumn>()
                                                                          .Select(x => x.ColumnName)
                                                                          .ToList();


            // Only try to map properties of class T that have a list of MatchingDatabaseField.
            //      The default GetProperties() is equivalent to GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static). 
            //      Needed to add BindingFlags.NonPublic to this list.
            List<PropertyInfo> propertiesToMap = (typeof(T)).GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                                                    .Where(x => x.GetCustomAttributes(typeof(MatchingDatabaseField), true).Any())
                                                    .ToList();


            Dictionary<PropertyInfo, string> propertyToDatabaseColumn = MatchPropertyToColumn(propertiesToMap, queryResultColumnNames);


            // If didn't find any properties to map, return empty list.
            // Perhaps unlikely, but would prevent running the subsequent foreach loop for every record returned (potentially many records).
            if (propertyToDatabaseColumn.Keys.Count == 0)
                return mappedObjects;


            // Map query results to list of T.
            foreach (DataRow row in queryResultTable.Rows)
            {
                T mappedObject = new T();
                foreach (PropertyInfo property in propertyToDatabaseColumn.Keys)
                {
                    string mappedColumn = propertyToDatabaseColumn[property];
                    var resultValue = row[mappedColumn];

                    if (resultValue == DBNull.Value)
                        resultValue = null;

                    // Try to set value. If data types do not match, throws InvalidCastException.
                    try
                    {
                        CheckTypeConversion(resultValue, property.PropertyType);
                        property.SetValue(mappedObject, resultValue, null);
                    }
                    catch (ArgumentException argumentException)
                    {
                        argumentException.Data.Add("DebugMessage", "Property: " + property.Name + ", " + property.PropertyType.ToString() + ". Column: " + mappedColumn + ". Attempted to insert value: " + resultValue.ToString() + ".");
                        throw argumentException;
                    }

                }
                mappedObjects.Add(mappedObject);
            }

            return mappedObjects;
        }



        /// <summary>
        /// Build a dictionary that tries to map each property's possible names to the query result column names. If multiple found, take the first one.
        /// <property>, <found 'column name>
        /// 
        /// Throws error if any required property doesn't find a match.
        /// 
        /// <param name="propertiesToMap"></param>
        /// <param name="queryResultColumnNames"></param>
        /// <returns></returns>
        private Dictionary<PropertyInfo, string> MatchPropertyToColumn(List<PropertyInfo> propertiesToMap, List<string> queryResultColumnNames)
        {
            Dictionary<PropertyInfo, string> propertyToDatabaseColumn = new Dictionary<PropertyInfo, string>();

            foreach (PropertyInfo property in propertiesToMap)
            {
                List<string> possibleColumnNames = MatchingDatabaseFieldHelper.GetMatchingDatabaseFieldColumnNames(property);
                string foundColumnName = queryResultColumnNames.Intersect(possibleColumnNames).FirstOrDefault();    // Must intersect like this to preserve priority order in the possibleColumnNames list.

                if (foundColumnName != null)
                {
                    propertyToDatabaseColumn.Add(property, foundColumnName);
                }
                else
                {
                    if (MatchingDatabaseFieldHelper.GetMatchingDatabaseFieldIsRequired(property))
                    {
                        // We want to know if an expected property could not be mapped.
                        throw new ValueNotFoundException("An expected property could not be mapped: " + property.Name);
                    }
                }
            }

            return propertyToDatabaseColumn;
        }


        private void CheckTypeConversion(dynamic someValue, Type propertyType)
        {
            bool TypeIsNullable = DALHelper.TypeIsNullable(propertyType);
            Type ConvertToType = propertyType;

            if (TypeIsNullable)
            {
                if (someValue == null)
                    return;     // Assignment of null to a Nullable type does not need to be checked.
                else
                    ConvertToType = Nullable.GetUnderlyingType(propertyType);
            }

            // Throws an error if the someValue cannot be converted to the ConvertToType. (InvalidCastException)
            dynamic convertedValue = Convert.ChangeType(someValue, ConvertToType);
        }

    }
}
