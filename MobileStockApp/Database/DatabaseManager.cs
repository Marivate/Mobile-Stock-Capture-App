using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using MobileStockApp.Models;

namespace MobileStockApp.Database
{
    /// <summary>
    /// Database manager for SQLite operations
    /// </summary>
    public class DatabaseManager
    {
        private readonly string _connectionString;
        private readonly string _databasePath;

        public DatabaseManager()
        {
            _databasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MobileStock.db");
            _connectionString = $"Data Source={_databasePath};Version=3;";
            InitializeDatabase();
        }

        /// <summary>
        /// Initialize database and create tables if they don't exist
        /// </summary>
        private void InitializeDatabase()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    string createTableQuery = @"
                        CREATE TABLE IF NOT EXISTS tblMobilePhones (
                            MobileCode TEXT PRIMARY KEY,
                            Make TEXT NOT NULL,
                            Quantity INTEGER NOT NULL
                        );
                    ";

                    using (SQLiteCommand command = new SQLiteCommand(createTableQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error initializing database: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Add a new mobile phone record to the database
        /// </summary>
        public bool AddRecord(MobilePhone phone)
        {
            try
            {
                if (!phone.IsValid())
                    return false;

                using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    string insertQuery = "INSERT INTO tblMobilePhones (MobileCode, Make, Quantity) VALUES (@code, @make, @quantity)";

                    using (SQLiteCommand command = new SQLiteCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@code", phone.MobileCode);
                        command.Parameters.AddWithValue("@make", phone.Make);
                        command.Parameters.AddWithValue("@quantity", phone.Quantity);
                        command.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Delete a mobile phone record by MobileCode
        /// </summary>
        public bool DeleteRecord(string mobileCode)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    string deleteQuery = "DELETE FROM tblMobilePhones WHERE MobileCode = @code";

                    using (SQLiteCommand command = new SQLiteCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@code", mobileCode);
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Find and retrieve a mobile phone record by MobileCode
        /// </summary>
        public MobilePhone FindRecord(string mobileCode)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    string selectQuery = "SELECT MobileCode, Make, Quantity FROM tblMobilePhones WHERE MobileCode = @code";

                    using (SQLiteCommand command = new SQLiteCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@code", mobileCode);
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new MobilePhone(
                                    reader["MobileCode"].ToString(),
                                    reader["Make"].ToString(),
                                    Convert.ToInt32(reader["Quantity"])
                                );
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Get all mobile phone records
        /// </summary>
        public List<MobilePhone> GetAllRecords()
        {
            List<MobilePhone> records = new List<MobilePhone>();
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    string selectQuery = "SELECT MobileCode, Make, Quantity FROM tblMobilePhones";

                    using (SQLiteCommand command = new SQLiteCommand(selectQuery, connection))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                records.Add(new MobilePhone(
                                    reader["MobileCode"].ToString(),
                                    reader["Make"].ToString(),
                                    Convert.ToInt32(reader["Quantity"])
                                ));
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Log error if needed
            }
            return records;
        }
    }
}
