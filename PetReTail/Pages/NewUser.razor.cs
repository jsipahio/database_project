using PetReTail.Data;

namespace PetReTail.Pages
{
    public partial class NewUser
    {
        private List<ShelterModel> _shelters = new List<ShelterModel>();
        private UserModel _userModel = new UserModel();
        private string _password;
        protected override void OnInitialized()
        {
            _shelters = DBMgr.GetAllShelters();
            base.OnInitialized();
        }
        private void AddUser()
        {
            
            Console.WriteLine("Shelter id = " + _userModel.ShelterID);
            if(DBMgr.CreateNewUser(_userModel.UserName, _password, _userModel.Email, _userModel.FirstName, _userModel.LastName, _userModel.ShelterID))
            {

            }
            else
            {
                throw new Exception("account creation failed");
            }
        }
    }
}