using PetReTail.Data;
namespace PetReTail.Pages
{
    public partial class Admin
    {
        private bool _loggedIn = false;
        private string _username = string.Empty;
        private string _password = string.Empty;
        private List<AnimalModel> _animals = null;
        private List<ShelterModel> _shelters = null;
        private ShelterModel _editShelter = new ShelterModel();
        private AnimalModel _editAnimal = new AnimalModel();
        private ShelterModel _newShelter = new ShelterModel();
        private AnimalModel _newAnimal = new AnimalModel();
        bool _showForm = false;
        bool _showShelterForm = false;
        bool _showNewAnimalForm = false;
        bool _showNewShelterForm = false;
        bool _showShelterError = false;
        bool _showAnimalError = false;
        bool _errmsg;
        private void TryLogin()
        {
            if(DBMgr.TryLogin("admin", _username, _password))
            {
                _loggedIn = true;
            }
            else
            {
                throw new Exception("cannot log in as admin");
            }
        }
        protected override void OnInitialized()
        {
            _shelters = DBMgr.GetAllShelters();
            _animals = new List<AnimalModel>();
            foreach (ShelterModel shelter in _shelters)
            {
                _animals.AddRange(DBMgr.GetAllAnimals(shelter.ID));
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
            //_editAnimal = _animals.Find(x => x.Contains(y => y.ID == id));
            _showForm = true;
        }
        private void DeleteShelter(string id)
        {
            Console.WriteLine(id);
        }
        private void EditShelter(string id)
        {
            Console.WriteLine(id);
            _editShelter = _shelters.Find(x => x.ID == id);
            _showShelterForm = true;
        }
        private void CancelEdit()
        {
            _editAnimal = new AnimalModel();
            _showForm = false;
        }
        private void ConfirmEdit()
        {
            DBMgr.EditAnimal(_editAnimal);
            _showForm = false;
        }
        private void CancelShelterEdit()
        {
            _showShelterForm = false;
            _editShelter = new ShelterModel();
        }
        private void ConfirmShelterEdit()
        {
            DBMgr.EditShelter(_editShelter);
            
            _shelters = DBMgr.GetAllShelters();
            _showShelterForm = false;
        }
    }
}