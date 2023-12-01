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
        private AnimalModel _newAnimal = new AnimalModel();
        private ShelterModel _shelter;
        private bool _showForm = false;
        private bool _showShelterForm = false;
        private bool _showNewAnimalForm = false;
        protected override void OnInitialized()
        {
            _newAnimal.DateReceived = DateTime.Now;
        }
        private void TryLogin()
        {
            Console.WriteLine(_username);
            Console.WriteLine(_password);
            if(DBMgr.TryLogin("admin", _username, _password))
            {
                _loggedIn = true;
                User = DBMgr.GetUserDetails(_username);
                _animals = DBMgr.GetAllAnimals(User.ShelterID);
                _shelter = DBMgr.GetSingleShelter(User.ShelterID);
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
            //Console.WriteLine(id);
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
            DBMgr.EditAnimal(_editAnimal);
            _animals = DBMgr.GetAllAnimals(_shelter.ID);
            _showForm = false;
        }
        private void StartShelterEdit()
        {
            _showShelterForm = true;
        }
        private void CancelShelterEdit()
        {
            _showShelterForm = false;
        }
        private void ConfirmShelterEdit()
        {
            DBMgr.EditShelter(_shelter);
            _showShelterForm = false;
        }
        private void StartNewAnimal()
        {
            _showNewAnimalForm = true;
        }
        private void CancelNewAnimal()
        {
            _showNewAnimalForm = false;
        }
        private void ConfirmNewAnimal()
        {
            _newAnimal.ShelterID = _shelter.ID;
            DBMgr.AddAnimal(_newAnimal);
            _animals = DBMgr.GetAllAnimals(_shelter.ID);
            _showNewAnimalForm = false;
        }
    }
}