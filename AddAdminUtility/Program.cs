using Microsoft.AspNetCore.Identity;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AddAdminUtility;
class Program
{
    
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        string user, email, first, last, role;
        role = "Admin";
        Console.WriteLine("Enter username");
        user = Console.ReadLine();
        PassReader passReader;
        passReader = ReadPassword();
        while(!passReader.success)
        {
            Console.WriteLine("Password do not match. Enter Again");
            passReader = ReadPassword();
        }
        // Console.WriteLine("Enter email");
        // email = Console.ReadLine();
        // Console.WriteLine("Enter first name");
        // first = Console.ReadLine();
        // Console.WriteLine("Enter last name");
        // last = Console.ReadLine();

        string hashedPass;

        PasswordHasher<string> passwordHasher = new PasswordHasher<string>();
        hashedPass = passwordHasher.HashPassword(user, passReader.password);

        byte[] bytes = Encoding.Unicode.GetBytes(hashedPass);

        hashedPass = Convert.ToBase64String(bytes);
        
        Console.WriteLine("Password = " + hashedPass);
        // string conn = System.Configuration.ConfigurationManager.ConnectionStrings["local"].ConnectionString;

        // SqlCommand cmd;
        // using(SqlConnection cnn = new SqlConnection(conn))
        // {
        //     cnn.Open();
        //     cmd = cnn.CreateCommand();
        //     SqlParameter param;
        //     SqlParameter status;
        //     SqlParameter errmsg;

        //     cmd.CommandType = CommandType.StoredProcedure;
        //     cmd.CommandText = "CreateNewUser";
            
        //     param = new SqlParameter("@p_username", SqlDbType.VarChar, 50);
        //     param.Direction = ParameterDirection.Input;
        //     param.Value = user;
        //     cmd.Parameters.Add(param);

        //     param = new SqlParameter("@p_password", SqlDbType.VarChar, 100);
        //     param.Direction = ParameterDirection.Input;
        //     param.Value = hashedPass;
        //     cmd.Parameters.Add(param);

        //     param = new SqlParameter("@p_first", SqlDbType.VarChar, 50);
        //     param.Direction = ParameterDirection.Input;
        //     param.Value = first;
        //     cmd.Parameters.Add(param);
            
        //     param = new SqlParameter("@p_last", SqlDbType.VarChar, 50);
        //     param.Direction = ParameterDirection.Input;
        //     param.Value = last;
        //     cmd.Parameters.Add(param);
            
        //     param = new SqlParameter("@p_email", SqlDbType.VarChar, 255);
        //     param.Direction = ParameterDirection.Input;
        //     param.Value = email;
        //     cmd.Parameters.Add(param);
            
        //     param = new SqlParameter("@p_role", SqlDbType.VarChar, 10);
        //     param.Direction = ParameterDirection.Input;
        //     param.Value = role;
        //     cmd.Parameters.Add(param);

        //     status = new SqlParameter("@p_status", SqlDbType.Int);
        //     status.Direction = ParameterDirection.Output;
        //     cmd.Parameters.Add(status);

        //     errmsg = new SqlParameter("@p_errmsg", SqlDbType.VarChar, 500);
        //     errmsg.Direction = ParameterDirection.Output;
        //     cmd.Parameters.Add(errmsg);
        //     cmd.ExecuteNonQuery();
        // }
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
