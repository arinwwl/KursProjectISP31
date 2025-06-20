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
    public class EmployeesService : BaseService<Employees>
    {
        public EmployeesService() : base()
        {
        }

        public override bool Add(Employees obj)
        {
            bool IsAdded = false;

            // Валидация данных
            if (obj.Age < 18 || obj.Age > 70)
                throw new ArgumentException("Возраст сотрудника должен быть от 18 до 70 лет");

            if (string.IsNullOrWhiteSpace(obj.FullName))
                throw new ArgumentException("ФИО сотрудника обязательно для заполнения");

            try
            {
                objSqlCommand.Parameters.Clear();
                objSqlCommand.CommandText = "udp_InsertEmployee";
                objSqlCommand.Parameters.AddWithValue("@FullName", obj.FullName);
                objSqlCommand.Parameters.AddWithValue("@Age", obj.Age);
                objSqlCommand.Parameters.AddWithValue("@Gender", obj.Gender);
                objSqlCommand.Parameters.AddWithValue("@Address", obj.Address ?? (object)DBNull.Value);
                objSqlCommand.Parameters.AddWithValue("@Phone", obj.Phone);
                objSqlCommand.Parameters.AddWithValue("@PassportData", obj.PassportData);
                objSqlCommand.Parameters.AddWithValue("@PositionID", obj.PositionID);

                objSqlconnection.Open();
                int addRows = objSqlCommand.ExecuteNonQuery();
                IsAdded = addRows > 0;
            }
            catch (SqlException ex)
            {
                throw new Exception("Ошибка при добавлении сотрудника в базу данных", ex);
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
                objSqlCommand.CommandText = "udp_DeleteEmployee";
                objSqlCommand.Parameters.AddWithValue("@EmployeeID", id);
                objSqlconnection.Open();
                int delRows = objSqlCommand.ExecuteNonQuery();
                IsDeleted = delRows > 0;
            }
            catch (SqlException ex)
            {
                throw new Exception("Ошибка при удалении сотрудника", ex);
            }
            finally
            {
                objSqlconnection.Close();
            }
            return IsDeleted;
        }

        public override List<Employees> GetAll()
        {
            List<Employees> employees = new List<Employees>();
            try
            {
                objSqlCommand.Parameters.Clear();
                objSqlCommand.CommandText = "udp_SelectAllEmployees";
                objSqlconnection.Open();

                using (var reader = objSqlCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var employee = new Employees
                            {
                                EmployeeID = reader.GetInt32(0),
                                FullName = reader.GetString(1),
                                Age = reader.GetInt32(2),
                                Gender = reader.GetString(3),
                                Address = reader.IsDBNull(4) ? null : reader.GetString(4),
                                Phone = reader.GetString(5),
                                PassportData = reader.GetString(6),
                                PositionID = reader.GetInt32(7)
                            };
                            employees.Add(employee);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Ошибка при получении списка сотрудников", ex);
            }
            finally
            {
                objSqlconnection.Close();
            }
            return employees;
        }

        public override bool Update(Employees obj)
        {
            bool IsUpdated = false;

            // Валидация данных
            if (obj.Age < 18 || obj.Age > 70)
                throw new ArgumentException("Возраст сотрудника должен быть от 18 до 70 лет");

            if (string.IsNullOrWhiteSpace(obj.FullName))
                throw new ArgumentException("ФИО сотрудника обязательно для заполнения");

            try
            {
                objSqlCommand.Parameters.Clear();
                objSqlCommand.CommandText = "udp_UpdateEmployee";
                objSqlCommand.Parameters.AddWithValue("@EmployeeID", obj.EmployeeID);
                objSqlCommand.Parameters.AddWithValue("@FullName", obj.FullName);
                objSqlCommand.Parameters.AddWithValue("@Age", obj.Age);
                objSqlCommand.Parameters.AddWithValue("@Gender", obj.Gender);
                objSqlCommand.Parameters.AddWithValue("@Address", obj.Address ?? (object)DBNull.Value);
                objSqlCommand.Parameters.AddWithValue("@Phone", obj.Phone);
                objSqlCommand.Parameters.AddWithValue("@PassportData", obj.PassportData);
                objSqlCommand.Parameters.AddWithValue("@PositionID", obj.PositionID);

                objSqlconnection.Open();
                int updateRows = objSqlCommand.ExecuteNonQuery();
                IsUpdated = updateRows > 0;
            }
            catch (SqlException ex)
            {
                throw new Exception("Ошибка при обновлении данных сотрудника", ex);
            }
            finally
            {
                objSqlconnection.Close();
            }
            return IsUpdated;
        }

        // Дополнительный метод для поиска сотрудников по ФИО
        public List<Employees> SearchByName(string name)
        {
            List<Employees> employees = new List<Employees>();
            try
            {
                objSqlCommand.Parameters.Clear();
                objSqlCommand.CommandText = "udp_SearchEmployeesByName";
                objSqlCommand.Parameters.AddWithValue("@SearchName", $"%{name}%");
                objSqlconnection.Open();

                using (var reader = objSqlCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            employees.Add(new Employees
                            {
                                EmployeeID = reader.GetInt32(0),
                                FullName = reader.GetString(1),
                                // остальные поля по аналогии
                            });
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Ошибка при поиске сотрудников", ex);
            }
            finally
            {
                objSqlconnection.Close();
            }
            return employees;
        }
    }
}