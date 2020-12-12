using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace BugTracker.Controllers
{
    /// <summary>
    /// The IDataProcess interface provides us with the framework
    /// for how we should handle interactions with the database.
    /// </summary>
    /// <example>
    /// <code>
    /// public class User : IDataProcess User
    /// {
    ///     public MySqlConnectionStringBuilder ConnectionString { get; set; }
    ///     public MySqlConnection Connection { get; set; }
    ///     public bool Insert(int personId, string address)
    ///     {
    ///         etc...
    ///     }
    /// }
    /// </code>
    /// </example>
    public interface IDataProcess<T>
    {
        public MySqlConnectionStringBuilder ConnectionString { get; set; }
        public MySqlConnection Connection { get; set; }

        /// <summary>
        /// Preps the connection and collects information for the connection string. 
        /// </summary>
        /// <returns> True if the connection is successful or false.</returns>
        /// <param name="connectionString"> A MySqlConnectionStringBuilder</param>
        /// <param name="connection"> A MySqlConnection</param>
        public bool Init(MySqlConnectionStringBuilder connectionString, MySqlConnection connection);

        /// <summary>
        /// Inserts an element into the table specified by the class.
        /// </summary>
        /// <returns> Returns true if the insert is successful and false if not. </returns>
        /// <param name="connectionString"> A MySqlConnectionStringBuilder</param>
        /// <param name="connection"> A MySqlConnection</param> 
        public bool Insert(MySqlConnectionStringBuilder connectionString, MySqlConnection connection);

        ///<summary>
        /// Updates a row in the database with the information specified.
        /// </summary>
        /// <returns> Returns true if the update is successful and false if not.</returns>
        /// <param name="connectionString"> A MySqlConnectionStringBuilder</param>
        /// <param name="connection"> A MySqlConnection</param>
        public bool Update(MySqlConnectionStringBuilder connectionString, MySqlConnection connection);
        
        ///<summary>
        /// Deletes the selected row in the database with the information specified.
        /// </summary>
        /// <returns> Returns true if the delete is successful and false if not.</returns>
        /// <param name="connectionString"> A MySqlConnectionStringBuilder</param>
        /// <param name="connection"> A MySqlConnection</param>
        public bool Delete(MySqlConnectionStringBuilder connectionString, MySqlConnection connection);
        
        /// <summary>
        /// Selects the information from the database and adds it to a list. This method will not work
        /// with SQL functions, it has to return at least 1 or 0 rows.
        /// </summary>
        /// <returns> Returns a list of the select objects or an empty list if unsuccessful.</returns>
        /// <param name="connectionString"> A MySqlConnectionStringBuilder</param>
        /// <param name="connection"> A MySqlConnection</param>
        /// <typeparam name="T"> This is the type of the implementing class.</typeparam>
        public IList<T> Select(MySqlConnectionStringBuilder connectionString, MySqlConnection connection);
    }
}