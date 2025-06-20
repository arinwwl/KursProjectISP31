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
    public class ClientsService : BaseService<Clients>
    {
        public ClientsService() : base()
        {
        }

        public override bool Add(Clients obj)
        {
            bool IsAdded = false;
            try
            {
                objSqlCommand.Parameters.Clear();
                objSqlCommand.CommandText = "udp_InsertClient";
                objSqlCommand.Parameters.AddWithValue("@FullName", obj.FullName);
                objSqlCommand.Parameters.AddWithValue("@Gender", obj.Gender);
                objSqlCommand.Parameters.AddWithValue("@BirthDate", obj.BirthDate);
                objSqlCommand.Parameters.AddWithValue("@Address", obj.Address);
                objSqlCommand.Parameters.AddWithValue("@Phone", obj.Phone);
                objSqlCommand.Parameters.AddWithValue("@PassportData", obj.PassportData);

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
                objSqlCommand.CommandText = "udp_DeleteClient";
                objSqlCommand.Parameters.AddWithValue("@ClientID", id);
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

        public override List<Clients> GetAll()
        {
            List<Clients> list = new List<Clients>();
            try
            {
                objSqlCommand.Parameters.Clear();
                objSqlCommand.CommandText = "udp_SelectAllClients";
                objSqlconnection.Open();
                var ObjSqlDataReader = objSqlCommand.ExecuteReader();
                if (ObjSqlDataReader.HasRows)
                {
                    Clients objClient = null;
                    while (ObjSqlDataReader.Read())
                    {
                        objClient = new Clients();
                        objClient.ClientID = ObjSqlDataReader.GetInt32(0);
                        objClient.FullName = ObjSqlDataReader.GetString(1);
                        objClient.Gender = ObjSqlDataReader.GetString(2);
                        objClient.BirthDate = ObjSqlDataReader.GetDateTime(3);
                        objClient.Address = ObjSqlDataReader.GetString(4);
                        objClient.Phone = ObjSqlDataReader.GetString(5);
                        objClient.PassportData = ObjSqlDataReader.GetString(6);
                        list.Add(objClient);
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

        public override bool Update(Clients obj)
        {
            bool IsUpdated = false;
            try
            {
                objSqlCommand.Parameters.Clear();
                objSqlCommand.CommandText = "udp_UpdateClient";
                objSqlCommand.Parameters.AddWithValue("@ClientID", obj.ClientID);
                objSqlCommand.Parameters.AddWithValue("@FullName", obj.FullName);
                objSqlCommand.Parameters.AddWithValue("@Gender", obj.Gender);
                objSqlCommand.Parameters.AddWithValue("@BirthDate", obj.BirthDate);
                objSqlCommand.Parameters.AddWithValue("@Address", obj.Address);
                objSqlCommand.Parameters.AddWithValue("@Phone", obj.Phone);
                objSqlCommand.Parameters.AddWithValue("@PassportData", obj.PassportData);

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