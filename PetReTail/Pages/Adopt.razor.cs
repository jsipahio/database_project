using PetReTail.Data;

namespace PetReTail.Pages
{
    public partial class Adopt
    {
        private AdopterModel _adopter = new AdopterModel();
        private AnimalModel _animal;
        protected override void OnInitialized()
        {
            _animal = DBMgr.GetSingleAnimal(AnimalID);
            base.OnInitialized();
        }
        private void RequestAdopt()
        {
            _adopter.ID = DBMgr.AddAdopter(_adopter);
            DBMgr.AdoptPet(_adopter.ID, _animal.ID, _animal.ShelterID);
        }
        private void RequestInfo()
        {
            DBMgr.AddAdopter(_adopter);
        }
    }
}