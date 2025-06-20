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
    public class PositionsService : BaseService<Positions>
    {
        public PositionsService() : base()
        {
        }

        public override bool Add(Positions obj)
        {
            bool IsAdded = false;

            // Валидация данных
            if (string.IsNullOrWhiteSpace(obj.PositionName))
                throw new ArgumentException("Название должности обязательно для заполнения");

            if (obj.Salary <= 0)
                throw new ArgumentException("Зарплата должна быть больше нуля");

            try
            {
                objSqlCommand.Parameters.Clear();
                objSqlCommand.CommandText = "udp_InsertPosition";
                objSqlCommand.Parameters.AddWithValue("@PositionName", obj.PositionName);
                objSqlCommand.Parameters.AddWithValue("@Salary", obj.Salary);
                objSqlCommand.Parameters.AddWithValue("@Responsibilities", obj.Responsibilities ?? (object)DBNull.Value);
                objSqlCommand.Parameters.AddWithValue("@Requirements", obj.Requirements ?? (object)DBNull.Value);

                objSqlconnection.Open();
                int addRows = objSqlCommand.ExecuteNonQuery();
                IsAdded = addRows > 0;
            }
            catch (SqlException ex)
            {
                throw new Exception("Ошибка при добавлении должности", ex);
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
                objSqlCommand.CommandText = "udp_DeletePosition";
                objSqlCommand.Parameters.AddWithValue("@PositionID", id);
                objSqlconnection.Open();
                int delRows = objSqlCommand.ExecuteNonQuery();
                IsDeleted = delRows > 0;
            }
            catch (SqlException ex) when (ex.Number == 547) // Ошибка FK constraint
            {
                throw new Exception("Невозможно удалить должность, так как есть сотрудники с этой должностью", ex);
            }
            catch (SqlException ex)
            {
                throw new Exception("Ошибка при удалении должности", ex);
            }
            finally
            {
                objSqlconnection.Close();
            }
            return IsDeleted;
        }

        public override List<Positions> GetAll()
        {
            List<Positions> positions = new List<Positions>();
            try
            {
                objSqlCommand.Parameters.Clear();
                objSqlCommand.CommandText = "udp_SelectAllPositions";
                objSqlconnection.Open();

                using (var reader = objSqlCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var position = new Positions
                            {
                                PositionID = reader.GetInt32(0),
                                PositionName = reader.GetString(1),
                                Salary = reader.GetDecimal(2),
                                Responsibilities = reader.IsDBNull(3) ? null : reader.GetString(3),
                                Requirements = reader.IsDBNull(4) ? null : reader.GetString(4)
                            };
                            positions.Add(position);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Ошибка при получении списка должностей", ex);
            }
            finally
            {
                objSqlconnection.Close();
            }
            return positions;
        }

        public override bool Update(Positions obj)
        {
            bool IsUpdated = false;

            // Валидация данных
            if (string.IsNullOrWhiteSpace(obj.PositionName))
                throw new ArgumentException("Название должности обязательно для заполнения");

            if (obj.Salary <= 0)
                throw new ArgumentException("Зарплата должна быть больше нуля");

            try
            {
                objSqlCommand.Parameters.Clear();
                objSqlCommand.CommandText = "udp_UpdatePosition";
                objSqlCommand.Parameters.AddWithValue("@PositionID", obj.PositionID);
                objSqlCommand.Parameters.AddWithValue("@PositionName", obj.PositionName);
                objSqlCommand.Parameters.AddWithValue("@Salary", obj.Salary);
                objSqlCommand.Parameters.AddWithValue("@Responsibilities", obj.Responsibilities ?? (object)DBNull.Value);
                objSqlCommand.Parameters.AddWithValue("@Requirements", obj.Requirements ?? (object)DBNull.Value);

                objSqlconnection.Open();
                int updateRows = objSqlCommand.ExecuteNonQuery();
                IsUpdated = updateRows > 0;
            }
            catch (SqlException ex)
            {
                throw new Exception("Ошибка при обновлении данных должности", ex);
            }
            finally
            {
                objSqlconnection.Close();
            }
            return IsUpdated;
        }

        // Дополнительный метод для получения должности по ID
        public Positions GetById(int id)
        {
            Positions position = null;
            try
            {
                objSqlCommand.Parameters.Clear();
                objSqlCommand.CommandText = "udp_GetPositionById";
                objSqlCommand.Parameters.AddWithValue("@PositionID", id);
                objSqlconnection.Open();

                using (var reader = objSqlCommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        position = new Positions
                        {
                            PositionID = reader.GetInt32(0),
                            PositionName = reader.GetString(1),
                            Salary = reader.GetDecimal(2),
                            Responsibilities = reader.IsDBNull(3) ? null : reader.GetString(3),
                            Requirements = reader.IsDBNull(4) ? null : reader.GetString(4)
                        };
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Ошибка при получении должности", ex);
            }
            finally
            {
                objSqlconnection.Close();
            }
            return position;
        }
    }
}