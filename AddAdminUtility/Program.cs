using Microsoft.AspNetCore.Identity;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace AddAdminUtility;
class Program
{
    
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        // string user, email, first, last, role;

        // role = "Admin";
        // Console.WriteLine("Enter username");
        // user = Console.ReadLine();
        // PassReader passReader;
        // passReader = ReadPassword();
        // while(!passReader.success)
        // {
        //     Console.WriteLine("Password do not match. Enter Again");
        //     passReader = ReadPassword();
        // }
        // Console.WriteLine("Enter email");
        // email = Console.ReadLine();
        // Console.WriteLine("Enter first name");
        // first = Console.ReadLine();
        // Console.WriteLine("Enter last name");
        // last = Console.ReadLine();
        try
        {
        string hashedPass = "AQAAAAEAACcQAAAAEN6LeYFbhbYAvkOBGU/Q10uvKyyyMAdqL5pCr3xP/NdbwLo3FjsS3z8r4MUWa/pcDQ==";
        string user = "oops";
        string pass = "letmein";
        string email = "lol@aol.com";
        string first = "please";
        string last = "work";
        string role = "admin";

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
            Console.WriteLine("SHA256 hash of \"letmein\" = " + hashedPass);
        }

        // PasswordHasher<string> passwordHasher = new PasswordHasher<string>();
        // string hashedPass2 = passwordHasher.HashPassword(user, pass);

        // Console.WriteLine("Password after hash = " + hashedPass2);
        // Console.WriteLine("Password in the DB  = " + hashedPass);

        // var base64EncodedBytes = System.Convert.FromBase64String(hashedPass);
        // hashedPass = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

        //var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(hashedPass);
        //hashedPass =  System.Convert.ToBase64String(base64EncodedBytes);

        //AQAAAAEAACcQAAAAEGJcPz8cBj8sPzw/P2o/bEbDrD8/Pz8/Pz8vMRE/cT8/BjY/UD8lPz9NXj9HPz8=
        //AQAAAAEAACcQAAAAEGJcuuYcBtmALIc86uBq4mxGw6yV2J2dwYTtsi8xEaRxteQGNtlQ4iWF0k1eq0fY6Q==
        
        //Console.WriteLine("Password back to base64 = " + hashedPass);
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["local"].ConnectionString;

        SqlCommand cmd;
        using(SqlConnection cnn = new SqlConnection(conn))
        {
            cnn.Open();
            cmd = cnn.CreateCommand();
            SqlParameter param;
            SqlParameter status;
            SqlParameter errmsg;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "CreateNewUser";
            
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
            param.Value = role;
            cmd.Parameters.Add(param);

            status = new SqlParameter("@p_status", SqlDbType.Int);
            status.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(status);

            errmsg = new SqlParameter("@p_errmsg", SqlDbType.VarChar, 500);
            errmsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errmsg);
            cmd.ExecuteNonQuery();
        }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    static PassReader ReadPassword()
    {
        Console.WriteLine("Enter password");
        string pass = MaskedInput();
        
        Console.WriteLine("Confirm Password");
        string conf = MaskedInput();

        if (conf == pass) return new PassReader() {password = pass, success = true};
        else return new PassReader() {password = string.Empty, success = false};
    }

    static string MaskedInput()
    {
        string pass = string.Empty;

        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            if(key.Key == ConsoleKey.Backspace && pass.Length > 1)
            {
                pass.Remove(pass.Length - 1, 1);
            }
            if(key.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                break;
            }
            pass += key.KeyChar;
        }
        return pass;
    }
    public struct PassReader
    {
        public string password;
        public bool success;
    }
}
