using KursProjectISP31.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace KursProjectISP31.Services
{
    public class AdditionalServicesService : BaseService<AdditionalServices>
    {
        public AdditionalServicesService() : base()
        {
        }

        public override bool Add(AdditionalServices service)
        {
            bool isAdded = false;
            try
            {
                objSqlCommand.Parameters.Clear();
                objSqlCommand.CommandText = "udp_InsertAdditionalService";
                objSqlCommand.Parameters.AddWithValue("@ServiceName", service.ServiceName);
                objSqlCommand.Parameters.AddWithValue("@ServicePrice", service.ServicePrice);
                objSqlCommand.Parameters.AddWithValue("@Description", service.Description ?? (object)DBNull.Value);

                objSqlconnection.Open();
                int affectedRows = objSqlCommand.ExecuteNonQuery();
                isAdded = affectedRows > 0;
            }
            catch (SqlException ex)
            {
                throw new Exception("Ошибка при добавлении услуги", ex);
            }
            finally
            {
                objSqlconnection.Close();
            }
            return isAdded;
        }

        public override bool Delete(int id)
        {
            bool isDeleted = false;
            try
            {
                objSqlCommand.Parameters.Clear();
                objSqlCommand.CommandText = "udp_DeleteAdditionalService";
                objSqlCommand.Parameters.AddWithValue("@ServiceID", id);

                objSqlconnection.Open();
                int affectedRows = objSqlCommand.ExecuteNonQuery();
                isDeleted = affectedRows > 0;
            }
            catch (SqlException ex)
            {
                throw new Exception("Ошибка при удалении услуги", ex);
            }
            finally
            {
                objSqlconnection.Close();
            }
            return isDeleted;
        }

        public override List<AdditionalServices> GetAll()
        {
            var services = new List<AdditionalServices>();
            try
            {
                objSqlCommand.Parameters.Clear();
                objSqlCommand.CommandText = "udp_SelectAllAdditionalServices";

                objSqlconnection.Open();
                using (var reader = objSqlCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var service = new AdditionalServices
                            {
                                ServiceID = reader.GetInt32(0),
                                ServiceName = reader.GetString(1),
                                ServicePrice = reader.GetDecimal(2),
                                Description = reader.IsDBNull(3) ? null : reader.GetString(3)
                            };
                            services.Add(service);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Ошибка при получении списка услуг", ex);
            }
            finally
            {
                objSqlconnection.Close();
            }
            return services;
        }

        public override bool Update(AdditionalServices service)
        {
            bool isUpdated = false;
            try
            {
                objSqlCommand.Parameters.Clear();
                objSqlCommand.CommandText = "udp_UpdateAdditionalService";
                objSqlCommand.Parameters.AddWithValue("@ServiceID", service.ServiceID);
                objSqlCommand.Parameters.AddWithValue("@ServiceName", service.ServiceName);
                objSqlCommand.Parameters.AddWithValue("@ServicePrice", service.ServicePrice);
                objSqlCommand.Parameters.AddWithValue("@Description", service.Description ?? (object)DBNull.Value);

                objSqlconnection.Open();
                int affectedRows = objSqlCommand.ExecuteNonQuery();
                isUpdated = affectedRows > 0;
            }
            catch (SqlException ex)
            {
                throw new Exception("Ошибка при обновлении услуги", ex);
            }
            finally
            {
                objSqlconnection.Close();
            }
            return isUpdated;
        }

        public AdditionalServices GetById(int id)
        {
            AdditionalServices service = null;
            try
            {
                objSqlCommand.Parameters.Clear();
                objSqlCommand.CommandText = "udp_GetAdditionalServiceById";
                objSqlCommand.Parameters.AddWithValue("@ServiceID", id);

                objSqlconnection.Open();
                using (var reader = objSqlCommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        service = new AdditionalServices
                        {
                            ServiceID = reader.GetInt32(0),
                            ServiceName = reader.GetString(1),
                            ServicePrice = reader.GetDecimal(2),
                            Description = reader.IsDBNull(3) ? null : reader.GetString(3)
                        };
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Ошибка при получении услуги", ex);
            }
            finally
            {
                objSqlconnection.Close();
            }
            return service;
        }
    }
}