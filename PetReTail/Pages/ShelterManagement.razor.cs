using PetReTail.Data;
namespace PetReTail.Pages
{
    public partial class ShelterManagement
    {
        private bool _loggedIn = false;
        public UserModel User { get; set; }
        private string _username = string.Empty;
        private string _password = string.Empty;
        private List<AnimalModel> _animals = null;
        private AnimalModel _editAnimal = new AnimalModel();
        private ShelterModel _shelter;
        bool _showForm = false;
        protected override void OnInitialized()
        {
            _animals = DBMgr.GetAllAnimals("shel01");
            _shelter = DBMgr.GetSingleShelter("shel01");
        }
        private void TryLogin()
        {
            Console.WriteLine(_username);
            Console.WriteLine(_password);
            if(DBMgr.TryLogin("admin", _username, _password))
            {
                _loggedIn = true;
            }
            else
            {
                _loggedIn = false;
            }
        }
        private void DeleteAnimal(int id)
        {
            // DBMgr.DeleteAnimal(id);
            Console.WriteLine(id);
        }
        private void EditAnimal(int id)
        {
            // DBMgr.DeleteAnimal(id);
            Console.WriteLine(id);
            _editAnimal = _animals.Find(x => x.ID == id);
            _showForm = true;
        }
        private void CancelEdit()
        {
            _editAnimal = new AnimalModel();
            _showForm = false;
        }
        private void ConfirmEdit()
        {
            _showForm = false;
        }
    }
}