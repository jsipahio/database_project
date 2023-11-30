using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using PetReTail.Data;

namespace PetReTail.Pages
{
    public partial class ViewPets
    {
        [Inject]
        private NavigationManager _nav {get; set;}
        private class SearchModel
        {
            public string ShelterID = "";
            public string Type = "";
            public string Age = "";
        }
        private List<ShelterModel> _shelters = new List<ShelterModel>();
        private List<AnimalModel> _animals = new List<AnimalModel>();
        private List<string> _ages = new List<string>() {"", "Young", "Adult", "Senior"};
        private List<string> _types = new List<string>();
        private SearchModel _searchModel = new SearchModel();
        protected override void OnInitialized()
        {
            bool shouldQry = false;
            var uri = _nav.ToAbsoluteUri(_nav.Uri);
            if(QueryHelpers.ParseQuery(uri.Query).TryGetValue("shelter", out var _shelter))
            {
                _searchModel.ShelterID = _shelter;
                shouldQry = true;
                Console.WriteLine(_searchModel.ShelterID);
            }
            if(QueryHelpers.ParseQuery(uri.Query).TryGetValue("type", out var _type))
            {
                _searchModel.Type = _type;
                shouldQry = true;
                Console.WriteLine(_searchModel.Type);
            }
            if(QueryHelpers.ParseQuery(uri.Query).TryGetValue("age", out var _age))
            {
                _searchModel.Age = _age;
                shouldQry = true;
                Console.WriteLine(_searchModel.Age);
            }
            if (shouldQry)
                _animals = DBMgr.GetAnimalsQry(_searchModel.ShelterID, _searchModel.Type, _searchModel.Age);
            else
                _animals = DBMgr.GetAnimalsQry();
            _shelters = DBMgr.GetAllShelters();
            _shelters.Add(new ShelterModel());
            _types = DBMgr.GetTypes();
            _types.Add("");
            
            base.OnInitialized();
        }
        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            
            return base.OnAfterRenderAsync(firstRender);
        }
        private void Filter()
        {
            var uri = _nav.ToAbsoluteUri(_nav.Uri);
            string path = uri.GetLeftPart(UriPartial.Path);
            path += "?shelter=" + _searchModel.ShelterID + "&type=" + _searchModel.Type + "&age=" + _searchModel.Age;
            _nav.NavigateTo(path, true);
        }
    }
}