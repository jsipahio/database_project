using System.Configuration;
using Microsoft.AspNetCore.Components;
using PetReTail.Data;

namespace PetReTail.Pages.Components
{
    public partial class ShelterInfo
    {
        private ShelterModel _shelter;
        //private string _imgPath;
        protected override void OnInitialized()
        {
            try
            {
                 _shelter = DBMgr.GetSingleShelter(ID);
                // _imgPath = "images/" + _animal.ID + ".png";
            }
            catch (Exception ex)
            {

            }
            base.OnInitialized();
        }
        protected override void OnAfterRender(bool firstRender)
        {
            try
            {
                if (firstRender)
                {
                }
                //_animal = DBMgr.GetSingleAnimal(ID);
                //_imgPath = "images/" + _animal.ID + ".png";
                base.OnAfterRender(firstRender);
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}