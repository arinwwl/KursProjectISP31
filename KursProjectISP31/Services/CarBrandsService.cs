using KursProjectISP31.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursProjectISP31.Services
{
    public class CarBrandsService : BaseService<CarBrands>
    {
        public CarBrandsService() : base()
        {
        }

        public override bool Add(CarBrands obj)
        {
            bool IsAdded = false;
            try
            {
                objSqlCommand.Parameters.Clear();
                objSqlCommand.CommandText = "udp_InsertCarBrand";
                objSqlCommand.Parameters.AddWithValue("@BrandName", obj.BrandName);
                objSqlCommand.Parameters.AddWithValue("@TechnicalSpecs", obj.TechnicalSpecs ?? (object)DBNull.Value);
                objSqlCommand.Parameters.AddWithValue("@Description", obj.Description ?? (object)DBNull.Value);
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
                objSqlCommand.CommandText = "udp_DeleteCarBrand";
                objSqlCommand.Parameters.AddWithValue("@BrandID", id);
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

        public override List<CarBrands> GetAll()
        {
            List<CarBrands> list = new List<CarBrands>();
            try
            {
                objSqlCommand.Parameters.Clear();
                objSqlCommand.CommandText = "udp_SelectAllCarBrands";
                objSqlconnection.Open();
                var ObjSqlDataReader = objSqlCommand.ExecuteReader();
                if (ObjSqlDataReader.HasRows)
                {
                    CarBrands objBrand = null;
                    while (ObjSqlDataReader.Read())
                    {
                        objBrand = new CarBrands();
                        objBrand.BrandID = ObjSqlDataReader.GetInt32(0);
                        objBrand.BrandName = ObjSqlDataReader.GetString(1);
                        objBrand.TechnicalSpecs = ObjSqlDataReader.IsDBNull(2) ? null : ObjSqlDataReader.GetString(2);
                        objBrand.Description = ObjSqlDataReader.IsDBNull(3) ? null : ObjSqlDataReader.GetString(3);
                        list.Add(objBrand);
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

        public override bool Update(CarBrands obj)
        {
            bool IsUpdate = false;
            try
            {
                objSqlCommand.Parameters.Clear();
                objSqlCommand.CommandText = "udp_UpdateCarBrand";
                objSqlCommand.Parameters.AddWithValue("@BrandID", obj.BrandID);
                objSqlCommand.Parameters.AddWithValue("@BrandName", obj.BrandName);
                objSqlCommand.Parameters.AddWithValue("@TechnicalSpecs", obj.TechnicalSpecs ?? (object)DBNull.Value);
                objSqlCommand.Parameters.AddWithValue("@Description", obj.Description ?? (object)DBNull.Value);
                objSqlconnection.Open();
                int updateRows = objSqlCommand.ExecuteNonQuery();
                IsUpdate = updateRows > 0;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                objSqlconnection.Close();
            }
            return IsUpdate;
        }

    }
}