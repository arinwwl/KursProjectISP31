using KursProjectISP31.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace KursProjectISP31.Services
{
    public class CarService : BaseService<Car>
    {
        public CarService() : base()
        {
        }

        public override bool Add(Car obj)
        {
            try
            {
                objSqlCommand.Parameters.Clear();
                objSqlCommand.CommandText = "udp_InsertCar";

                objSqlCommand.Parameters.Add("@BrandID", System.Data.SqlDbType.Int).Value = obj.BrandID;
                objSqlCommand.Parameters.Add("@RegistrationNumber", System.Data.SqlDbType.NVarChar).Value = obj.RegistrationNumber ?? (object)DBNull.Value;
                objSqlCommand.Parameters.Add("@VIN", System.Data.SqlDbType.NVarChar).Value = obj.VIN ?? (object)DBNull.Value;
                objSqlCommand.Parameters.Add("@EngineNumber", System.Data.SqlDbType.NVarChar).Value = obj.EngineNumber ?? (object)DBNull.Value;
                objSqlCommand.Parameters.Add("@ManufactureYear", System.Data.SqlDbType.VarChar, 5).Value = obj.ManufactureYear ?? (object)DBNull.Value;
                objSqlCommand.Parameters.Add("@Mileage", System.Data.SqlDbType.VarChar, 50).Value = obj.Mileage ?? (object)DBNull.Value;
                objSqlCommand.Parameters.Add("@CarPrice", System.Data.SqlDbType.Decimal).Value = obj.CarPrice;
                objSqlCommand.Parameters.Add("@DailyRentalPrice", System.Data.SqlDbType.Decimal).Value = obj.DailyRentalPrice;
                objSqlCommand.Parameters.Add("@LastInspectionDate", System.Data.SqlDbType.Date).Value = obj.LastInspectionDate ?? (object)DBNull.Value;
                objSqlCommand.Parameters.Add("@EmployeeID", System.Data.SqlDbType.Int).Value = obj.EmployeeID;
                objSqlCommand.Parameters.Add("@SpecialNotes", System.Data.SqlDbType.NVarChar).Value = obj.SpecialNotes ?? (object)DBNull.Value;
                objSqlCommand.Parameters.Add("@IsReturned", System.Data.SqlDbType.Bit).Value = obj.IsReturned;

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
                objSqlCommand.CommandText = "udp_DeleteCar";
                objSqlCommand.Parameters.Add("@CarID", System.Data.SqlDbType.Int).Value = id;

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

        public override List<Car> GetAll()
        {
            var list = new List<Car>();
            try
            {
                objSqlCommand.Parameters.Clear();
                objSqlCommand.CommandText = "udp_SelectAllCars";
                objSqlconnection.Open();

                using (var reader = objSqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var car = new Car
                        {
                            CarID = reader.GetInt32("CarID"),
                            BrandID = reader.GetInt32("BrandID"),
                            RegistrationNumber = reader.IsDBNull("RegistrationNumber") ? string.Empty : reader.GetString("RegistrationNumber"),
                            VIN = reader.IsDBNull("VIN") ? string.Empty : reader.GetString("VIN"),
                            EngineNumber = reader.IsDBNull("EngineNumber") ? string.Empty : reader.GetString("EngineNumber"),
                            ManufactureYear = reader.IsDBNull("ManufactureYear") ? string.Empty : reader.GetString("ManufactureYear"),
                            Mileage = reader.IsDBNull("Mileage") ? string.Empty : reader.GetString("Mileage"),
                            CarPrice = reader.GetDecimal("CarPrice"),
                            DailyRentalPrice = reader.GetDecimal("DailyRentalPrice"),
                            LastInspectionDate = reader.IsDBNull("LastInspectionDate") ? null : reader.GetDateTime("LastInspectionDate"),
                            EmployeeID = reader.GetInt32("EmployeeID"),
                            SpecialNotes = reader.IsDBNull("SpecialNotes") ? string.Empty : reader.GetString("SpecialNotes"),
                            IsReturned = reader.GetBoolean("IsReturned")
                        };
                        list.Add(car);
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

        public override bool Update(Car obj)
        {
            try
            {
                objSqlCommand.Parameters.Clear();
                objSqlCommand.CommandText = "udp_UpdateCar";

                objSqlCommand.Parameters.Add("@CarID", System.Data.SqlDbType.Int).Value = obj.CarID;
                objSqlCommand.Parameters.Add("@BrandID", System.Data.SqlDbType.Int).Value = obj.BrandID;
                objSqlCommand.Parameters.Add("@RegistrationNumber", System.Data.SqlDbType.NVarChar).Value = obj.RegistrationNumber ?? (object)DBNull.Value;
                objSqlCommand.Parameters.Add("@VIN", System.Data.SqlDbType.NVarChar).Value = obj.VIN ?? (object)DBNull.Value;
                objSqlCommand.Parameters.Add("@EngineNumber", System.Data.SqlDbType.NVarChar).Value = obj.EngineNumber ?? (object)DBNull.Value;
                objSqlCommand.Parameters.Add("@ManufactureYear", System.Data.SqlDbType.VarChar, 5).Value = obj.ManufactureYear ?? (object)DBNull.Value;
                objSqlCommand.Parameters.Add("@Mileage", System.Data.SqlDbType.VarChar, 50).Value = obj.Mileage ?? (object)DBNull.Value;
                objSqlCommand.Parameters.Add("@CarPrice", System.Data.SqlDbType.Decimal).Value = obj.CarPrice;
                objSqlCommand.Parameters.Add("@DailyRentalPrice", System.Data.SqlDbType.Decimal).Value = obj.DailyRentalPrice;
                objSqlCommand.Parameters.Add("@LastInspectionDate", System.Data.SqlDbType.Date).Value = obj.LastInspectionDate ?? (object)DBNull.Value;
                objSqlCommand.Parameters.Add("@EmployeeID", System.Data.SqlDbType.Int).Value = obj.EmployeeID;
                objSqlCommand.Parameters.Add("@SpecialNotes", System.Data.SqlDbType.NVarChar).Value = obj.SpecialNotes ?? (object)DBNull.Value;
                objSqlCommand.Parameters.Add("@IsReturned", System.Data.SqlDbType.Bit).Value = obj.IsReturned;

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