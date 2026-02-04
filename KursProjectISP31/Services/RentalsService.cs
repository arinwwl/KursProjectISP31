using KursProjectISP31.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace KursProjectISP31.Services
{
    public class RentalsService : BaseService<Rentals>
    {
        public RentalsService() : base() { }

        public override bool Add(Rentals obj)
        {
            bool isAdded = false;

            // Валидация данных
            if (obj.CarID <= 0) throw new ArgumentException("Не указан автомобиль для аренды");
            if (obj.ClientID <= 0) throw new ArgumentException("Не указан клиент");
            if (obj.RentalPeriod <= 0) throw new ArgumentException("Период аренды должен быть больше 0");

            try
            {
                objSqlCommand.Parameters.Clear();
                objSqlCommand.CommandText = "udp_InsertRental";

                // Обязательные параметры
                objSqlCommand.Parameters.AddWithValue("@IssueDate", obj.IssueDate);
                objSqlCommand.Parameters.AddWithValue("@RentalPeriod", obj.RentalPeriod);
                objSqlCommand.Parameters.AddWithValue("@CarID", obj.CarID);
                objSqlCommand.Parameters.AddWithValue("@ClientID", obj.ClientID);
                objSqlCommand.Parameters.AddWithValue("@RentalPrice", obj.RentalPrice);
                objSqlCommand.Parameters.AddWithValue("@IsPaid", obj.IsPaid);
                objSqlCommand.Parameters.AddWithValue("@EmployeeID", obj.EmployeeID);

                // Необязательные параметры (могут быть NULL)
                objSqlCommand.Parameters.AddWithValue("@ReturnDate", obj.ReturnDate == default(DateTime) ? (object)DBNull.Value : obj.ReturnDate);
                objSqlCommand.Parameters.AddWithValue("@Service1ID", obj.Services ?? (object)DBNull.Value);
                

                objSqlconnection.Open();
                isAdded = objSqlCommand.ExecuteNonQuery() > 0;
            }
            catch (SqlException ex)
            {
                throw new Exception("Ошибка при добавлении аренды", ex);
            }
            finally
            {
                if (objSqlconnection.State == System.Data.ConnectionState.Open)
                    objSqlconnection.Close();
            }

            return isAdded;
        }

        public override bool Update(Rentals obj)
        {
            bool isUpdated = false;

            // Валидация данных
            if (obj.RentalID <= 0) throw new ArgumentException("Неверный ID аренды");
            if (obj.RentalPeriod <= 0) throw new ArgumentException("Период аренды должен быть больше 0");

            try
            {
                objSqlCommand.Parameters.Clear();
                objSqlCommand.CommandText = "udp_UpdateRental";

                // Обязательные параметры
                objSqlCommand.Parameters.AddWithValue("@RentalID", obj.RentalID);
                objSqlCommand.Parameters.AddWithValue("@IssueDate", obj.IssueDate);
                objSqlCommand.Parameters.AddWithValue("@RentalPeriod", obj.RentalPeriod);
                objSqlCommand.Parameters.AddWithValue("@CarID", obj.CarID);
                objSqlCommand.Parameters.AddWithValue("@ClientID", obj.ClientID);
                objSqlCommand.Parameters.AddWithValue("@RentalPrice", obj.RentalPrice);
                objSqlCommand.Parameters.AddWithValue("@IsPaid", obj.IsPaid);
                objSqlCommand.Parameters.AddWithValue("@EmployeeID", obj.EmployeeID);

                // Необязательные параметры
                objSqlCommand.Parameters.AddWithValue("@ReturnDate", obj.ReturnDate == default(DateTime) ? (object)DBNull.Value : obj.ReturnDate);
                objSqlCommand.Parameters.AddWithValue("@Service1ID", obj.Services ?? (object)DBNull.Value);
               

                objSqlconnection.Open();
                isUpdated = objSqlCommand.ExecuteNonQuery() > 0;
            }
            catch (SqlException ex)
            {
                throw new Exception("Ошибка при обновлении аренды", ex);
            }
            finally
            {
                if (objSqlconnection.State == System.Data.ConnectionState.Open)
                    objSqlconnection.Close();
            }

            return isUpdated;
        }

        public override List<Rentals> GetAll()
        {
            List<Rentals> rentals = new List<Rentals>();

            try
            {
                objSqlCommand.Parameters.Clear();
                objSqlCommand.CommandText = "udp_SelectAllRentals";
                objSqlconnection.Open();

                using (var reader = objSqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rentals.Add(new Rentals
                        {
                            RentalID = reader.GetInt32(reader.GetOrdinal("RentalID")),
                            IssueDate = reader.GetDateTime(reader.GetOrdinal("IssueDate")),
                            RentalPeriod = reader.GetInt32(reader.GetOrdinal("RentalPeriod")),
                            ReturnDate = reader.IsDBNull(reader.GetOrdinal("ReturnDate")) ? default : reader.GetDateTime(reader.GetOrdinal("ReturnDate")),
                            CarID = reader.GetInt32(reader.GetOrdinal("CarID")),
                            ClientID = reader.GetInt32(reader.GetOrdinal("ClientID")),
                            Services = reader.IsDBNull(reader.GetOrdinal("Services")) ? (string?)null : reader.GetString(reader.GetOrdinal("Services")),
                            
                            RentalPrice = reader.GetDecimal(reader.GetOrdinal("RentalPrice")),
                            IsPaid = reader.GetBoolean(reader.GetOrdinal("IsPaid")),
                            EmployeeID = reader.GetInt32(reader.GetOrdinal("EmployeeID"))
                        });
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Ошибка при получении списка аренд", ex);
            }
            finally
            {
                if (objSqlconnection.State == System.Data.ConnectionState.Open)
                    objSqlconnection.Close();
            }

            return rentals;
        }

        public override bool Delete(int id)
        {
            bool isDeleted = false;

            try
            {
                objSqlCommand.Parameters.Clear();
                objSqlCommand.CommandText = "udp_DeleteRental";
                objSqlCommand.Parameters.AddWithValue("@RentalID", id);

                objSqlconnection.Open();
                isDeleted = objSqlCommand.ExecuteNonQuery() > 0;
            }
            catch (SqlException ex)
            {
                throw new Exception("Ошибка при удалении аренды", ex);
            }
            finally
            {
                if (objSqlconnection.State == System.Data.ConnectionState.Open)
                    objSqlconnection.Close();
            }

            return isDeleted;
        }

        public List<Rentals> GetActiveRentals()
        {
            List<Rentals> activeRentals = new List<Rentals>();

            try
            {
                objSqlCommand.Parameters.Clear();
                objSqlCommand.CommandText = "udp_GetActiveRentals";
                objSqlconnection.Open();

                using (var reader = objSqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        activeRentals.Add(new Rentals
                        {
                            RentalID = reader.GetInt32(reader.GetOrdinal("RentalID")),
                            IssueDate = reader.GetDateTime(reader.GetOrdinal("IssueDate")),
                            RentalPeriod = reader.GetInt32(reader.GetOrdinal("RentalPeriod")),
                            ReturnDate = reader.IsDBNull(reader.GetOrdinal("ReturnDate")) ? default : reader.GetDateTime(reader.GetOrdinal("ReturnDate")),
                            CarID = reader.GetInt32(reader.GetOrdinal("CarID")),
                            ClientID = reader.GetInt32(reader.GetOrdinal("ClientID")),
                            Services = reader.IsDBNull(reader.GetOrdinal("Services")) ? (string?)null : reader.GetString(reader.GetOrdinal("Service1ID")),
                            RentalPrice = reader.GetDecimal(reader.GetOrdinal("RentalPrice")),
                            IsPaid = reader.GetBoolean(reader.GetOrdinal("IsPaid")),
                            EmployeeID = reader.GetInt32(reader.GetOrdinal("EmployeeID"))
                        });
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Ошибка при получении активных аренд", ex);
            }
            finally
            {
                if (objSqlconnection.State == System.Data.ConnectionState.Open)
                    objSqlconnection.Close();
            }

            return activeRentals;
        }
    }
}