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
        private ShelterModel _editShelter = new ShelterModel();
        private AnimalModel _editAnimal = new AnimalModel();
        bool _showForm = false;
        bool _showShelterForm = false;
        private void TryLogin()
        {

        }
        protected override void OnInitialized()
        {
            _shelters = DBMgr.GetAllShelters();
            _animals = new List<List<AnimalModel>>();
            foreach (ShelterModel shelter in _shelters)
            {
                _animals.Add(DBMgr.GetAllAnimals(shelter.ID));
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
            //_editAnimal = _animals.Find(x => x.Contains(y => y.ID == id));
            _showForm = true;
        }
        private void DeleteShelter(string id)
        {
            // DBMgr.DeleteAnimal(id);
            Console.WriteLine(id);
        }
        private void EditShelter(string id)
        {
            // DBMgr.DeleteAnimal(id);
            Console.WriteLine(id);
            //_editShelter = _shelters.Find(x => x.ID == id);
            _showShelterForm = true;
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