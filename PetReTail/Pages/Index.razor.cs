using PetReTail.Data;

namespace PetReTail.Pages
{
    public partial class Index
    {
        private List<AnimalModel> _animals = new List<AnimalModel>();
        private string errmsg = string.Empty;
        protected override void OnInitialized()
        {
            try
            {
                _animals = DBMgr.GetMostRecentAnimals();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}