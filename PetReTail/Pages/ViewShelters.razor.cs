using PetReTail.Data;
namespace PetReTail.Pages
{
    public partial class ViewShelters
    {
        private List<ShelterModel> _shelters = null;

        protected override void OnInitialized()
        {
            _shelters = DBMgr.GetAllShelters();
        }
        private string Gethref(string id)
        {
            return "shelter/" + id;
        }
    }
}