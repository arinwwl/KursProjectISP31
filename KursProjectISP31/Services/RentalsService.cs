using KursProjectISP31.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace KursProjectISP31.Services
{
    public class RentalService : BaseService<Rental>
    {
        public RentalService() : base()
        {
        }

        public override bool Add(Rental obj)
        {
            try
            {
                objSqlCommand.Parameters.Clear();
                objSqlCommand.CommandText = "udp_InsertRental";

                objSqlCommand.Parameters.Add("@IssueDate", System.Data.SqlDbType.Date).Value = obj.IssueDate;
                objSqlCommand.Parameters.Add("@RentalPeriod", System.Data.SqlDbType.Int).Value = obj.RentalPeriod;
                objSqlCommand.Parameters.Add("@ReturnDate", System.Data.SqlDbType.Date).Value = obj.ReturnDate ?? (object)DBNull.Value;
                objSqlCommand.Parameters.Add("@CarID", System.Data.SqlDbType.Int).Value = obj.CarID;
                objSqlCommand.Parameters.Add("@ClientID", System.Data.SqlDbType.Int).Value = obj.ClientID;
                objSqlCommand.Parameters.Add("@Services", System.Data.SqlDbType.NVarChar).Value = obj.Services ?? (object)DBNull.Value;
                objSqlCommand.Parameters.Add("@RentalPrice", System.Data.SqlDbType.Decimal).Value = obj.RentalPrice;
                objSqlCommand.Parameters.Add("@IsPaid", System.Data.SqlDbType.Bit).Value = obj.IsPaid;
                objSqlCommand.Parameters.Add("@EmployeeID", System.Data.SqlDbType.Int).Value = obj.EmployeeID;

                objSqlconnection.Open();
                int rowsAffected = objSqlCommand.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            finally
            {
                if (objSqlconnection.State == System.Data.ConnectionState.Open)
                    objSqlconnection.Close();
            }
        }

        public override bool Delete(int id)
        {
            try
            {
                objSqlCommand.Parameters.Clear();
                objSqlCommand.CommandText = "udp_DeleteRental";
                objSqlCommand.Parameters.Add("@RentalID", System.Data.SqlDbType.Int).Value = id;

                objSqlconnection.Open();
                int rowsAffected = objSqlCommand.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            finally
            {
                if (objSqlconnection.State == System.Data.ConnectionState.Open)
                    objSqlconnection.Close();
            }
        }

        public override List<Rental> GetAll()
        {
            var list = new List<Rental>();
            try
            {
                objSqlCommand.Parameters.Clear();
                objSqlCommand.CommandText = "udp_SelectAllRentals";
                objSqlconnection.Open();

                using (var reader = objSqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var rental = new Rental
                        {
                            RentalID = reader.GetInt32("RentalID"),
                            IssueDate = reader.GetDateTime("IssueDate"),
                            RentalPeriod = reader.GetInt32("RentalPeriod"),
                            ReturnDate = reader.IsDBNull("ReturnDate") ? null : reader.GetDateTime("ReturnDate"),
                            CarID = reader.GetInt32("CarID"),
                            ClientID = reader.GetInt32("ClientID"),
                            Services = reader.IsDBNull("Services") ? string.Empty : reader.GetString("Services"),
                            RentalPrice = reader.GetDecimal("RentalPrice"),
                            IsPaid = reader.GetBoolean("IsPaid"),
                            EmployeeID = reader.GetInt32("EmployeeID")
                        };
                        list.Add(rental);
                    }
                }
                return list;
            }
            finally
            {
                if (objSqlconnection.State == System.Data.ConnectionState.Open)
                    objSqlconnection.Close();
            }
        }

        public override bool Update(Rental obj)
        {
            try
            {
                objSqlCommand.Parameters.Clear();
                objSqlCommand.CommandText = "udp_UpdateRental";

                objSqlCommand.Parameters.Add("@RentalID", System.Data.SqlDbType.Int).Value = obj.RentalID;
                objSqlCommand.Parameters.Add("@IssueDate", System.Data.SqlDbType.Date).Value = obj.IssueDate;
                objSqlCommand.Parameters.Add("@RentalPeriod", System.Data.SqlDbType.Int).Value = obj.RentalPeriod;
                objSqlCommand.Parameters.Add("@ReturnDate", System.Data.SqlDbType.Date).Value = obj.ReturnDate ?? (object)DBNull.Value;
                objSqlCommand.Parameters.Add("@CarID", System.Data.SqlDbType.Int).Value = obj.CarID;
                objSqlCommand.Parameters.Add("@ClientID", System.Data.SqlDbType.Int).Value = obj.ClientID;
                objSqlCommand.Parameters.Add("@Services", System.Data.SqlDbType.NVarChar).Value = obj.Services ?? (object)DBNull.Value;
                objSqlCommand.Parameters.Add("@RentalPrice", System.Data.SqlDbType.Decimal).Value = obj.RentalPrice;
                objSqlCommand.Parameters.Add("@IsPaid", System.Data.SqlDbType.Bit).Value = obj.IsPaid;
                objSqlCommand.Parameters.Add("@EmployeeID", System.Data.SqlDbType.Int).Value = obj.EmployeeID;

                objSqlconnection.Open();
                int rowsAffected = objSqlCommand.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            finally
            {
                if (objSqlconnection.State == System.Data.ConnectionState.Open)
                    objSqlconnection.Close();
            }
        }
    }
}