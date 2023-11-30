using PetReTail.Data;

namespace PetReTail.Pages
{
    public partial class CreateAccount
    {
        private List<ShelterModel> _shelters;
        private string _username;
        private string _password;
        private string _first;
        private string _last;
        private string _email;
        private string _shelter;
        private bool _success;
        private string _message;
        private void DBCreateAccount()
        {
            try
            {
                if(DBMgr.CreateNewUser(_username, _password, _email, _first, _last, _shelter))
                {
                    _message = "Account creation successful";
                    _success = true;
                }
                else
                {
                    _message = "Acount creation failed";
                    _success = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}