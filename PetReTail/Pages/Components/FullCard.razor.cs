using System.Configuration;
using Microsoft.AspNetCore.Components;
using PetReTail.Data;

namespace PetReTail.Pages.Components
{
    public partial class FullCard
    {
        private AnimalModel _animal;
        private string _imgPath;
        private string _shelterPath;
        //wut frl
        protected override void OnInitialized()
        {
            try
            {
                _animal = DBMgr.GetSingleAnimal(ID);
                _imgPath = "images/" + _animal.ID + ".png";
                _shelterPath = "shelter/" + _animal.ShelterID;
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