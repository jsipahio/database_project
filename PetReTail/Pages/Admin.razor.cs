using PetReTail.Data;
namespace PetReTail.Pages
{
    public partial class Admin
    {
        private bool _loggedIn = false;
        private string _username = string.Empty;
        private string _password = string.Empty;
        private List<List<AnimalModel>> _animals = null;
        private List<ShelterModel> _shelters = null;
        private void TryLogin()
        {

        }
    }
}