using Microsoft.AspNetCore.Identity;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;


namespace PetReTail.Data
{
    public static class DBMgr
    {
        private struct StoredProcs
        {
            public static string GET_SINGLE_ANIMAL = "GetSingleAnimal";
            public static string GET_SINGLE_SHELTER = "GetSingleShelter";
            public static string GET_USER_INFO = "GetUserInfo";
            public static string ADD_USER = "CreateNewUser";
            public static string ADD_ANIMAL = "AddAnimal";
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
                    string cmdText = "select @p_dbpass = password from [User] where username = @p_user";
                    cnn.Open();
                    cmd = cnn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = cmdText;

                    param = new SqlParameter("@p_user", SqlDbType.VarChar, 50);
                    param.Direction = ParameterDirection.Input;
                    param.Value = user;
                    cmd.Parameters.Add(param);

                    dbpass = new SqlParameter("@p_dbpass", SqlDbType.VarChar, 100);
                    dbpass.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(dbpass);

                    cmd.ExecuteNonQuery();

                    string hashedPass;

                    using (SHA256 hashObj = SHA256.Create())
                    {
                        byte[] bytes = Encoding.UTF8.GetBytes(pass);
                        byte[] data = hashObj.ComputeHash(bytes);

                        var sBuilder = new StringBuilder();

                        // Loop through each byte of the hashed data
                        // and format each one as a hexadecimal string.
                        for (int i = 0; i < data.Length; i++)
                        {
                            sBuilder.Append(data[i].ToString("x2"));
                        }

                        // Return the hexadecimal string.
                        hashedPass =  sBuilder.ToString();
                    }

                    // Console.WriteLine(hashedPass);
                    // Console.WriteLine(dbpass.Value.ToString());

                    if (hashedPass == dbpass.Value.ToString())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch(Exception)
            {
                throw;
            }
        }
        public static UserModel GetUserDetails(string user)
        {
            try
            {
                UserModel details;
                SqlCommand cmd;
                SqlParameter param;
                SqlParameter first, last, email, phone, shelter;

                using (SqlConnection cnn = new SqlConnection(_connectionString))
                {
                    string cmdText = "select @p_first = first_name, @p_last = last_name, @p_email = email_address, @p_shelter = shelter_id from [User] where username = @p_user";
                    cnn.Open();
                    cmd = cnn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = cmdText;

                    param = new SqlParameter("@p_user", SqlDbType.VarChar, 50);
                    param.Direction = ParameterDirection.Input;
                    param.Value = user;
                    cmd.Parameters.Add(param);

                    first = new SqlParameter("@p_first", SqlDbType.VarChar, 100);
                    first.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(first);
                    
                    last = new SqlParameter("@p_last", SqlDbType.VarChar, 100);
                    last.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(last);
                    
                    email = new SqlParameter("@p_email", SqlDbType.VarChar, 100);
                    email.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(email);

                    shelter = new SqlParameter("@p_shelter", SqlDbType.VarChar, 100);
                    shelter.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(shelter);

                    cmd.ExecuteNonQuery();

                    return new UserModel() {
                        UserName = user,
                        FirstName = first.Value.ToString(),
                        LastName = last.Value.ToString(),
                        Email = email.Value.ToString(),
                        ShelterID = shelter.Value.ToString()
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static AnimalModel GetSingleAnimal(String id)
        {
            try
            {
                SqlCommand cmd;
                SqlParameter animal_id, name, type, breed, gender, age, is_vaxed, is_fixed, fees, shelter_id, description, status, errmsg;

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
                    
                    description = new SqlParameter("@p_description", SqlDbType.VarChar, 500);
                    description.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(description);
                    
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
                                ShelterID = shelter_id.Value.ToString(),
                                Description = description.Value.ToString()
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
        public static void AddAnimal(AnimalModel animal)
        {
            try
            {
                SqlCommand cmd;
                SqlParameter datereceived, name, type, breed, gender, age, is_vaxed, is_fixed, fees, shelter_id, description, status, errmsg;

                using (SqlConnection cnn = new SqlConnection(_connectionString))
                {
                    cnn.Open();
                    cmd = cnn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = StoredProcs.GET_SINGLE_ANIMAL;


                    name = new SqlParameter("@p_name", SqlDbType.VarChar, 50);
                    name.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(name);

                    type = new SqlParameter("@p_type", SqlDbType.VarChar, 50);
                    type.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(type);

                    breed = new SqlParameter("@p_breed", SqlDbType.VarChar, 50);
                    breed.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(breed);

                    gender = new SqlParameter("@p_gender", SqlDbType.VarChar, 1);
                    gender.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(gender);

                    age = new SqlParameter("@p_age", SqlDbType.Int);
                    age.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(age);

                    is_vaxed = new SqlParameter("@p_is_vaxed", SqlDbType.VarChar, 10);
                    is_vaxed.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(is_vaxed);

                    is_fixed = new SqlParameter("@p_is_fixed", SqlDbType.VarChar, 10);
                    is_fixed.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(is_fixed);
                    
                    fees = new SqlParameter("@p_fees", SqlDbType.Decimal);
                    fees.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(fees);

                    shelter_id = new SqlParameter("@p_shelter_id", SqlDbType.VarChar, 6);
                    shelter_id.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(shelter_id);
                    
                    description = new SqlParameter("@p_description", SqlDbType.VarChar, 500);
                    description.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(description);

                    if(animal.DateReceived == null)
                    {
                        animal.DateReceived = DateTime.Now;
                    }

                    datereceived = new SqlParameter("@p_datereceived", SqlDbType.DateTime);
                    datereceived.Direction = ParameterDirection.Input;
                    datereceived.Value = animal.DateReceived;
                    cmd.Parameters.Add(datereceived);
                    
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
        public static List<AnimalModel> GetMostRecentAnimals(int qty = 5)
        {
            try
            {
                List<AnimalModel> animals = new List<AnimalModel>();

                string sqlqry = "select top 5 animal_id, animal_name, type, breed, age from Animal order by date_received desc";

                SqlCommand cmd;
                SqlParameter pqty = new SqlParameter("@p_qty", SqlDbType.Int);

                using(SqlConnection cnn = new SqlConnection(_connectionString))
                {
                    cnn.Open();
                    cmd = cnn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sqlqry;

                    pqty.Direction = ParameterDirection.Input;
                    pqty.Value = qty;
                    cmd.Parameters.Add(pqty);

                    DataSet ds = new DataSet();

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);

                    DataTableReader dtr;
                    dtr = ds.CreateDataReader();

                    if (dtr.HasRows)
                    {
                        while (dtr.Read())
                        {
                            animals.Add(new AnimalModel() {
                                Name = dtr["animal_name"].ToString(),
                                ID = Convert.ToInt32(dtr["animal_id"].ToString()),
                                Type = dtr["type"].ToString(),
                                Breed = dtr["breed"].ToString(),
                                Age = Convert.ToInt32(dtr["age"].ToString())
                            });
                        }
                    }
                    else 
                    {
                        throw new Exception("No animals found or issue with database");
                    }
                }

                return animals;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static List<AnimalModel> GetAllAnimals(string shelterID)
        {
            try
            {
                List<AnimalModel> animals = new List<AnimalModel>();
                string sqlqry = "select a.description, a.animal_id, a.animal_name, a.type, a.breed, a.age, a.date_received, a.age, case when a.is_vaxed = 0 then 'False' else 'True' end as is_vaxed, case when a.is_fixed = 0 then 'False' else 'True' end as is_fixed, a.fees, a.shelter_id, case when a.gender = 'M' then 'Male' else 'Female' end as gender from Animal a left join AdoptionDetails ad on a.animal_id = ad.animal_id where a.shelter_id = @p_shelter and isnull(ad.animal_id, 0) = 0";

                SqlCommand cmd;
                SqlParameter pqty = new SqlParameter("@p_shelter", SqlDbType.VarChar, 6);

                using(SqlConnection cnn = new SqlConnection(_connectionString))
                {
                    cnn.Open();
                    cmd = cnn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sqlqry;

                    pqty.Direction = ParameterDirection.Input;
                    pqty.Value = shelterID;
                    cmd.Parameters.Add(pqty);

                    DataSet ds = new DataSet();

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);

                    DataTableReader dtr;
                    dtr = ds.CreateDataReader();

                    if (dtr.HasRows)
                    {
                        while (dtr.Read())
                        {
                            animals.Add(new AnimalModel() {
                                Name = dtr["animal_name"].ToString(),
                                ID = Convert.ToInt32(dtr["animal_id"].ToString()),
                                Type = dtr["type"].ToString(),
                                Breed = dtr["breed"].ToString(),
                                Gender = dtr["gender"].ToString(),
                                Age = Convert.ToInt32(dtr["age"].ToString()),
                                DateReceived = Convert.ToDateTime(dtr["date_received"].ToString()),
                                IsFixed = Convert.ToBoolean(dtr["is_fixed"]),
                                IsVaxed = Convert.ToBoolean(dtr["is_vaxed"].ToString()),
                                ShelterID = dtr["shelter_id"].ToString(),
                                Description = dtr["description"].ToString()
                            });
                        }
                    }
                    else 
                    {
                        throw new Exception("No animals found or issue with database");
                    }
                }
                return animals;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static List<AnimalModel> GetAnimalsQry(string shelter = "", string type = "", string age = "")
        {
            try
            {
                List<AnimalModel> animals = new List<AnimalModel>();

                string sqlqry = "select a.animal_id, a.animal_name, a.type, a.breed, a.age, a.date_received, a.age, case when a.is_vaxed = 0 then 'False' else 'True' end as is_vaxed, case when a.is_fixed = 0 then 'False' else 'True' end as is_fixed, a.fees, a.shelter_id, case when a.gender = 'M' then 'Male' else 'Female' end as gender from Animal a left join AdoptionDetails ad on a.animal_id = ad.animal_id where isnull(ad.animal_id, 0) = 0";

                SqlCommand cmd;

                SqlParameter param;

                using(SqlConnection cnn = new SqlConnection(_connectionString))
                {
                    cnn.Open();
                    cmd = cnn.CreateCommand();

                    if(!string.IsNullOrEmpty(type))
                    {
                        sqlqry += " and a.type = @p_type";
                        param = new SqlParameter("@p_type", SqlDbType.VarChar, 50);
                        param.Direction = ParameterDirection.Input;
                        param.Value = type;
                        cmd.Parameters.Add(param);
                    }

                    if(!string.IsNullOrEmpty(shelter))
                    {
                        sqlqry += " and a.shelter_id = @p_shelter";
                        param = new SqlParameter("@p_shelter", SqlDbType.VarChar, 6);
                        param.Direction = ParameterDirection.Input;
                        param.Value = shelter;
                        cmd.Parameters.Add(param);
                    }

                    if(!string.IsNullOrEmpty(age))
                    {
                        sqlqry += " and a.age = @p_age";
                        param = new SqlParameter("@p_age", SqlDbType.Int);
                        param.Direction = ParameterDirection.Input;
                        int ageInt;
                        switch (age.ToLower())
                        {
                            case "young":
                                ageInt = 0;
                                break;
                            case "adult":
                                ageInt = 1;
                                break;
                            case "senior":
                                ageInt = 2;
                                break;
                            default:
                                throw new Exception("Invalid age specifier");
                        }
                        param.Value = ageInt;
                        cmd.Parameters.Add(param);
                    }

                    Console.WriteLine(sqlqry);
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sqlqry;

                    DataSet ds = new DataSet();

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);

                    DataTableReader dtr;
                    dtr = ds.CreateDataReader();

                    if (dtr.HasRows)
                    {
                        while (dtr.Read())
                        {
                            animals.Add(new AnimalModel() {
                                Name = dtr["animal_name"].ToString(),
                                ID = Convert.ToInt32(dtr["animal_id"].ToString()),
                                Type = dtr["type"].ToString(),
                                Breed = dtr["breed"].ToString(),
                                Gender = dtr["gender"].ToString(),
                                Age = Convert.ToInt32(dtr["age"].ToString()),
                                DateReceived = Convert.ToDateTime(dtr["date_received"].ToString()),
                                IsFixed = Convert.ToBoolean(dtr["is_fixed"]),
                                IsVaxed = Convert.ToBoolean(dtr["is_vaxed"].ToString()),
                                ShelterID = dtr["shelter_id"].ToString()
                            });
                        }
                    }
                    else 
                    {
                        throw new Exception("No animals found or issue with database");
                    }
                }


                return animals;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static List<string> GetTypes()
        {
            try
            {
                List<string> types = new List<string>();
                
                string sqlqry = "select distinct type from Animal";

                SqlCommand cmd;
                SqlParameter pqty = new SqlParameter("@p_shelter", SqlDbType.VarChar, 6);

                using(SqlConnection cnn = new SqlConnection(_connectionString))
                {
                    cnn.Open();
                    cmd = cnn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sqlqry;

                    DataSet ds = new DataSet();

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);

                    DataTableReader dtr;
                    dtr = ds.CreateDataReader();

                    if (dtr.HasRows)
                    {
                        while (dtr.Read())
                        {
                            types.Add(dtr["type"].ToString());
                        }
                    }
                    else 
                    {
                        throw new Exception("No animals found or issue with database");
                    }
                }

                return types;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool DeleteAnimal(int id)
        {
            try
            {
                string sqlqry = "delete from Animal where animal_id = @p_id";

                SqlCommand cmd;
                SqlParameter animal_id = new SqlParameter("@p_id", SqlDbType.Int);

                using(SqlConnection cnn = new SqlConnection(_connectionString))
                {
                    cnn.Open();
                    cmd = cnn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sqlqry;

                    animal_id.Direction = ParameterDirection.Input;
                    animal_id.Value = id;
                    cmd.Parameters.Add(animal_id);

                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool EditAnimal(AnimalModel editModel)
        {
            try
            {
                string sqlqry = "update Animal set animal_name = @p_name, breed = @p_breed, age = @p_age, is_vaxed = @p_vaxed, is_fixed = @p_fixed, [description] = @p_description where animal_id = @p_id";

                SqlCommand cmd;
                SqlParameter animal_id = new SqlParameter("@p_id", SqlDbType.Int);
                SqlParameter param;

                using(SqlConnection cnn = new SqlConnection(_connectionString))
                {
                    cnn.Open();
                    cmd = cnn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sqlqry;

                    animal_id.Direction = ParameterDirection.Input;
                    animal_id.Value = editModel.ID;
                    cmd.Parameters.Add(animal_id);

                    param = new SqlParameter("@p_name", SqlDbType.VarChar, 50);
                    param.Direction = ParameterDirection.Input;
                    param.Value = editModel.Name;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@p_breed", SqlDbType.VarChar, 50);
                    param.Direction = ParameterDirection.Input;
                    param.Value = editModel.Breed;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@p_age", SqlDbType.Int);
                    param.Direction = ParameterDirection.Input;
                    param.Value = editModel.Age;
                    cmd.Parameters.Add(param);
                    
                    param = new SqlParameter("@p_vaxed", SqlDbType.Bit);
                    param.Direction = ParameterDirection.Input;
                    param.Value = editModel.IsVaxed;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@p_fixed", SqlDbType.Bit);
                    param.Direction = ParameterDirection.Input;
                    param.Value = editModel.IsFixed;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@p_description", SqlDbType.VarChar, 500);
                    param.Direction = ParameterDirection.Input;
                    param.Value = editModel.Description;
                    cmd.Parameters.Add(param);

                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static ShelterModel GetSingleShelter(String id)
        {
            try
            {
                SqlCommand cmd;
                SqlParameter shelter_id, shelter_name, street_address, city, state, zip, phone_num, email_address, description, status, errmsg;

                using (SqlConnection cnn = new SqlConnection(_connectionString))
                {
                    cnn.Open();
                    cmd = cnn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = StoredProcs.GET_SINGLE_SHELTER;

                    shelter_id = new SqlParameter("@p_shelter_id", SqlDbType.VarChar, 6);
                    shelter_id.Direction = ParameterDirection.Input;
                    shelter_id.Value = id;
                    cmd.Parameters.Add(shelter_id);

                    shelter_name = new SqlParameter("@p_name", SqlDbType.VarChar, 50);
                    shelter_name.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(shelter_name);

                    street_address = new SqlParameter("@p_street", SqlDbType.VarChar, 50);
                    street_address.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(street_address);

                    city = new SqlParameter("@p_city", SqlDbType.VarChar, 50);
                    city.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(city);

                    state = new SqlParameter("@p_state", SqlDbType.VarChar, 2);
                    state.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(state);

                    zip = new SqlParameter("@p_zip", SqlDbType.VarChar, 5);
                    zip.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(zip);

                    phone_num = new SqlParameter("@p_phone", SqlDbType.VarChar, 10);
                    phone_num.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(phone_num);

                    email_address = new SqlParameter("@p_email", SqlDbType.VarChar, 50);
                    email_address.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(email_address);

                    description = new SqlParameter("@p_description", SqlDbType.VarChar, 500);
                    description.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(description);
                    
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
                            ShelterModel shelterModel = new ShelterModel(){
                                ID = shelter_id.Value.ToString(),
                                Name = shelter_name.Value.ToString(),
                                StreetAddress = street_address.Value.ToString(),
                                City = city.Value.ToString(),
                                State = state.Value.ToString(),
                                Zip = zip.Value.ToString(),
                                Phone = phone_num.Value.ToString(),
                                Email = email_address.Value.ToString(),
                                Description = description.Value.ToString()
                            };
                            return shelterModel;
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
        public static List<ShelterModel> GetAllShelters()
        {
            try
            {
                List<ShelterModel> shelters = new List<ShelterModel>();

                string sqlqry = "select * from Shelter";

                SqlCommand cmd;

                using(SqlConnection cnn = new SqlConnection(_connectionString))
                {
                    cnn.Open();
                    cmd = cnn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sqlqry;


                    DataSet ds = new DataSet();

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);

                    DataTableReader dtr;
                    dtr = ds.CreateDataReader();

                    if (dtr.HasRows)
                    {
                        while (dtr.Read())
                        {
                            shelters.Add(new ShelterModel() {
                                Name = dtr["shelter_name"].ToString(),
                                ID = (dtr["shelter_id"].ToString()),
                                StreetAddress = dtr["street_address"].ToString(),
                                City = dtr["city"].ToString(),
                                State = dtr["state"].ToString(),
                                Zip = (dtr["zip"].ToString()),
                                Phone = (dtr["phone_num"].ToString()),
                                Email = (dtr["email_address"]).ToString(),
                                Description = (dtr["description"].ToString())
                            });
                        }
                    }
                    else 
                    {
                        throw new Exception("No animals found or issue with database");
                    }
                }

                return shelters;
            }
            catch(Exception)
            {
                throw;
            }
        }
        public static bool DeleteShelter(string id)
        {
            try
            {
                string sqlqry = "delete from Shelter where shelter_id = @p_id";

                SqlCommand cmd;
                SqlParameter shelter_id = new SqlParameter("@p_id", SqlDbType.VarChar, 6);

                using(SqlConnection cnn = new SqlConnection(_connectionString))
                {
                    cnn.Open();
                    cmd = cnn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sqlqry;

                    shelter_id.Direction = ParameterDirection.Input;
                    shelter_id.Value = id;
                    cmd.Parameters.Add(shelter_id);

                   
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool EditShelter(ShelterModel editModel)
        {
            try
            {
                string sqlqry = "update Shelter set shelter_name = @p_name, street_address = @p_street, city = @p_city, state = @p_state, zip = @p_zip, phone_num = @p_phone, email_address = @p_email, description = @p_description where shelter_id = @p_id";

                SqlCommand cmd;
                SqlParameter shelter_id = new SqlParameter("@p_id", SqlDbType.VarChar, 6);
                SqlParameter param;

                using(SqlConnection cnn = new SqlConnection(_connectionString))
                {
                    cnn.Open();
                    cmd = cnn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sqlqry;

                    shelter_id.Direction = ParameterDirection.Input;
                    shelter_id.Value = editModel.ID;
                    cmd.Parameters.Add(shelter_id);

                    param = new SqlParameter("@p_name", SqlDbType.VarChar, 50);
                    param.Direction = ParameterDirection.Input;
                    param.Value = editModel.Name;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@p_street", SqlDbType.VarChar, 255);
                    param.Direction = ParameterDirection.Input;
                    param.Value = editModel.StreetAddress;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@p_city", SqlDbType.VarChar, 100);
                    param.Direction = ParameterDirection.Input;
                    param.Value = editModel.City;
                    cmd.Parameters.Add(param);
                    
                    param = new SqlParameter("@p_state", SqlDbType.VarChar, 2);
                    param.Direction = ParameterDirection.Input;
                    param.Value = editModel.State;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@p_zip", SqlDbType.VarChar, 5);
                    param.Direction = ParameterDirection.Input;
                    param.Value = editModel.Zip;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@p_phone", SqlDbType.VarChar, 10);
                    param.Direction = ParameterDirection.Input;
                    param.Value = editModel.Phone;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@p_email", SqlDbType.VarChar, 50);
                    param.Direction = ParameterDirection.Input;
                    param.Value = editModel.Email;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@p_description", SqlDbType.VarChar, 500);
                    param.Direction = ParameterDirection.Input;
                    param.Value = editModel.Description;
                    cmd.Parameters.Add(param);

                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool CreateNewUser(string user, string pass, string email, string first, string last, string shelterID)
        {
            try
            {
                string hashedPass;

                // PasswordHasher<string> passwordHasher = new PasswordHasher<string>();
                // hashedPass = passwordHasher.HashPassword(user, pass);

                using (SHA256 hashObj = SHA256.Create())
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(pass);
                    byte[] data = hashObj.ComputeHash(bytes);

                    var sBuilder = new StringBuilder();

                    // Loop through each byte of the hashed data
                    // and format each one as a hexadecimal string.
                    for (int i = 0; i < data.Length; i++)
                    {
                        sBuilder.Append(data[i].ToString("x2"));
                    }

                    // Return the hexadecimal string.
                    hashedPass =  sBuilder.ToString();
                }
                
                SqlCommand cmd;
                using(SqlConnection cnn = new SqlConnection(_connectionString))
                {
                    cnn.Open();
                    cmd = cnn.CreateCommand();
                    SqlParameter param;
                    SqlParameter status;
                    SqlParameter errmsg;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = StoredProcs.ADD_USER;
                    
                    param = new SqlParameter("@p_username", SqlDbType.VarChar, 50);
                    param.Direction = ParameterDirection.Input;
                    param.Value = user;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@p_password", SqlDbType.VarChar, 100);
                    param.Direction = ParameterDirection.Input;
                    param.Value = hashedPass;
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@p_first", SqlDbType.VarChar, 50);
                    param.Direction = ParameterDirection.Input;
                    param.Value = first;
                    cmd.Parameters.Add(param);
                    
                    param = new SqlParameter("@p_last", SqlDbType.VarChar, 50);
                    param.Direction = ParameterDirection.Input;
                    param.Value = last;
                    cmd.Parameters.Add(param);
                    
                    param = new SqlParameter("@p_email", SqlDbType.VarChar, 255);
                    param.Direction = ParameterDirection.Input;
                    param.Value = email;
                    cmd.Parameters.Add(param);
                    
                    param = new SqlParameter("@p_role", SqlDbType.VarChar, 10);
                    param.Direction = ParameterDirection.Input;
                    param.Value = "shelter";
                    cmd.Parameters.Add(param);
                    
                    param = new SqlParameter("@p_shelter", SqlDbType.VarChar, 10);
                    param.Direction = ParameterDirection.Input;
                    param.Value = shelterID;
                    cmd.Parameters.Add(param);

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
                            return true;
                        default:
                            return false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}