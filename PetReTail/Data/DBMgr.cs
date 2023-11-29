using Microsoft.AspNetCore.Identity;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace PetReTail.Data
{
    public static class DBMgr
    {
        private struct StoredProcs
        {
            public static string GET_SINGLE_ANIMAL = "GetSingleAnimal";
        }
        private static string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["local"].ConnectionString;
        public static bool TryLogin(string role, string user, string pass)
        {
            try
            {
                SqlCommand cmd;
                SqlParameter param;
                SqlParameter dbpass;

                using (SqlConnection cnn = new SqlConnection(_connectionString))
                {
                    string cmdText = "select @p_dbpass = password from User where username = @p_user";
                    cnn.Open();
                    cmd = cnn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = cmdText;

                    param = new SqlParameter("@p_user", SqlDbType.VarChar, 50);
                    param.Direction = ParameterDirection.Input;
                    param.Value = user;
                    cmd.Parameters.Add(param);

                    dbpass = new SqlParameter("@p_dbpass", SqlDbType.VarChar, 50);
                    dbpass.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(dbpass);

                    cmd.ExecuteNonQuery();

                    string hashedPass = dbpass.Value.ToString();

                    

                }

                return true;
            }
            catch(Exception)
            {
                throw;
            }
        }
        public static AnimalModel GetSingleAnimal(String id)
        {
            try
            {
                SqlCommand cmd;
                SqlParameter animal_id, name, type, breed, gender, age, is_vaxed, is_fixed, fees, shelter_id, status, errmsg;

                using (SqlConnection cnn = new SqlConnection(_connectionString))
                {
                    cnn.Open();
                    cmd = cnn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = StoredProcs.GET_SINGLE_ANIMAL;

                    animal_id = new SqlParameter("@p_animal_id", SqlDbType.Int);
                    animal_id.Direction = ParameterDirection.Input;
                    animal_id.Value = Convert.ToInt32(id);
                    cmd.Parameters.Add(animal_id);

                    name = new SqlParameter("@p_name", SqlDbType.VarChar, 50);
                    name.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(name);

                    type = new SqlParameter("@p_type", SqlDbType.VarChar, 50);
                    type.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(type);

                    breed = new SqlParameter("@p_breed", SqlDbType.VarChar, 50);
                    breed.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(breed);

                    gender = new SqlParameter("@p_gender", SqlDbType.VarChar, 1);
                    gender.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(gender);

                    age = new SqlParameter("@p_age", SqlDbType.Int);
                    age.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(age);

                    is_vaxed = new SqlParameter("@p_is_vaxed", SqlDbType.VarChar, 10);
                    is_vaxed.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(is_vaxed);

                    is_fixed = new SqlParameter("@p_is_fixed", SqlDbType.VarChar, 10);
                    is_fixed.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(is_fixed);
                    
                    fees = new SqlParameter("@p_fees", SqlDbType.Decimal);
                    fees.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(fees);

                    shelter_id = new SqlParameter("@p_shelter_id", SqlDbType.VarChar, 6);
                    shelter_id.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(shelter_id);
                    
                    status = new SqlParameter("@p_status", SqlDbType.Int);
                    status.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(status);
                    
                    errmsg = new SqlParameter("@p_errmsg", SqlDbType.VarChar, 500);
                    errmsg.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(errmsg);

                    cmd.ExecuteNonQuery();

                    switch ((int)status.Value)
                    {
                        case 0:
                            AnimalModel animalModel = new AnimalModel(){
                                ID = Convert.ToInt32(id),
                                Name = name.Value.ToString(),
                                Type = type.Value.ToString(),
                                Breed = breed.Value.ToString(),
                                Gender = gender.Value.ToString(),
                                Age = Convert.ToInt32(age.Value.ToString()),
                                IsVaxed = Convert.ToBoolean(is_vaxed.Value.ToString()),
                                IsFixed = Convert.ToBoolean(is_fixed.Value.ToString()),
                                Fee = Convert.ToDecimal(fees.Value.ToString()),
                                ShelterID = shelter_id.Value.ToString()
                            };
                            return animalModel;
                            break;
                        default:
                            throw new Exception(errmsg.Value.ToString());
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}