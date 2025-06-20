using KursProjectISP31.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace KursProjectISP31.Services
{
    public class CarsService : BaseService<Cars>
    {
        public CarsService() : base()
        {
        }

        public override bool Add(Cars obj)
        {
            bool IsAdded = false;
            try
            {
                objSqlCommand.Parameters.Clear();
                objSqlCommand.CommandText = "udp_InsertCar";
                objSqlCommand.Parameters.AddWithValue("@BrandID", obj.BrandID);
                objSqlCommand.Parameters.AddWithValue("@RegistrationNumber", obj.RegistrationNumber);
                objSqlCommand.Parameters.AddWithValue("@VIN", obj.VIN);
                objSqlCommand.Parameters.AddWithValue("@EngineNumber", obj.EngineNumber);
                objSqlCommand.Parameters.AddWithValue("@ManufactureYear", obj.ManufactureYear);
                objSqlCommand.Parameters.AddWithValue("@Mileage", obj.Mileage);
                objSqlCommand.Parameters.AddWithValue("@CarPrice", obj.CarPrice);
                objSqlCommand.Parameters.AddWithValue("@DailyRentalPrice", obj.DailyRentalPrice);
                objSqlCommand.Parameters.AddWithValue("@LastInspectionDate", obj.LastInspectionDate);
                objSqlCommand.Parameters.AddWithValue("@EmployeeID", obj.EmployedD);
                objSqlCommand.Parameters.AddWithValue("@SpecialNotes", obj.SpecialNotes ?? (object)DBNull.Value);
                objSqlCommand.Parameters.AddWithValue("@IsReturned", obj.IsReturned);

                objSqlconnection.Open();
                int addRows = objSqlCommand.ExecuteNonQuery();
                IsAdded = addRows > 0;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                objSqlconnection.Close();
            }
            return IsAdded;
        }

        public override bool Delete(int id)
        {
            bool IsDeleted = false;
            try
            {
                objSqlCommand.Parameters.Clear();
                objSqlCommand.CommandText = "udp_DeleteCar";
                objSqlCommand.Parameters.AddWithValue("@CarID", id);
                objSqlconnection.Open();
                int delRows = objSqlCommand.ExecuteNonQuery();
                IsDeleted = delRows > 0;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                objSqlconnection.Close();
            }
            return IsDeleted;
        }

        public override List<Cars> GetAll()
        {
            List<Cars> list = new List<Cars>();
            try
            {
                objSqlCommand.Parameters.Clear();
                objSqlCommand.CommandText = "udp_SelectAllCars";
                objSqlconnection.Open();
                var ObjSqlDataReader = objSqlCommand.ExecuteReader();
                if (ObjSqlDataReader.HasRows)
                {
                    Cars objCar = null;
                    while (ObjSqlDataReader.Read())
                    {
                        objCar = new Cars();
                        objCar.CarID = ObjSqlDataReader.GetInt32(0);
                        objCar.BrandID = ObjSqlDataReader.GetInt32(1);
                        objCar.RegistrationNumber = ObjSqlDataReader.GetString(2);
                        objCar.VIN = ObjSqlDataReader.GetString(3);
                        objCar.EngineNumber = ObjSqlDataReader.GetString(4);
                        objCar.ManufactureYear = ObjSqlDataReader.GetInt32(5);
                        objCar.Mileage = ObjSqlDataReader.GetInt32(6);
                        objCar.CarPrice = ObjSqlDataReader.GetDecimal(7);
                        objCar.DailyRentalPrice = ObjSqlDataReader.GetDecimal(8);
                        objCar.LastInspectionDate = ObjSqlDataReader.GetDateTime(9);
                        objCar.EmployedD = ObjSqlDataReader.GetInt32(10);
                        objCar.SpecialNotes = ObjSqlDataReader.IsDBNull(11) ? null : ObjSqlDataReader.GetString(11);
                        objCar.IsReturned = ObjSqlDataReader.GetBoolean(12);
                        list.Add(objCar);
                    }
                }
                ObjSqlDataReader.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                objSqlconnection.Close();
            }
            return list;
        }

        public override bool Update(Cars obj)
        {
            bool IsUpdated = false;
            try
            {
                objSqlCommand.Parameters.Clear();
                objSqlCommand.CommandText = "udp_UpdateCar";
                objSqlCommand.Parameters.AddWithValue("@CarID", obj.CarID);
                objSqlCommand.Parameters.AddWithValue("@BrandID", obj.BrandID);
                objSqlCommand.Parameters.AddWithValue("@RegistrationNumber", obj.RegistrationNumber);
                objSqlCommand.Parameters.AddWithValue("@VIN", obj.VIN);
                objSqlCommand.Parameters.AddWithValue("@EngineNumber", obj.EngineNumber);
                objSqlCommand.Parameters.AddWithValue("@ManufactureYear", obj.ManufactureYear);
                objSqlCommand.Parameters.AddWithValue("@Mileage", obj.Mileage);
                objSqlCommand.Parameters.AddWithValue("@CarPrice", obj.CarPrice);
                objSqlCommand.Parameters.AddWithValue("@DailyRentalPrice", obj.DailyRentalPrice);
                objSqlCommand.Parameters.AddWithValue("@LastInspectionDate", obj.LastInspectionDate);
                objSqlCommand.Parameters.AddWithValue("@EmployeeID", obj.EmployedD);
                objSqlCommand.Parameters.AddWithValue("@SpecialNotes", obj.SpecialNotes ?? (object)DBNull.Value);
                objSqlCommand.Parameters.AddWithValue("@IsReturned", obj.IsReturned);

                objSqlconnection.Open();
                int updateRows = objSqlCommand.ExecuteNonQuery();
                IsUpdated = updateRows > 0;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                objSqlconnection.Close();
            }
            return IsUpdated;
        }
    }
}