using System.Configuration;
using Microsoft.AspNetCore.Components;
using PetReTail.Data;

namespace PetReTail.Pages.Components
{
    public partial class FullCard
    {
        // [Parameter]
        // public string ID { get; set; }
        private AnimalModel _animal;// = new AnimalModel(){
        //                         ID = Convert.ToInt32("1234"),
        //                         Name = "Butters",
        //                         Type = "Cat",
        //                         Breed = "Big Chungus",
        //                         Gender = "Male",
        //                         Age = 5,
        //                         IsVaxed = true,
        //                         IsFixed = true,
        //                         Fee = 100.00M,
        //                         ShelterID = "Shltr1"
        // };
        private string _imgPath;// = "images/butters.jpg";
        //wut frl
        protected override void OnInitialized()
        {
            try
            {
                //_animal = DBMgr.GetSingleAnimal(ID);
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
                   _animal = new AnimalModel(){
                                ID = Convert.ToInt32("1234"),
                                Name = "Butters",
                                Type = "Cat",
                                Breed = "Big Chungus",
                                Gender = "Male",
                                Age = 5,
                                IsVaxed = true,
                                IsFixed = true,
                                Fee = 100.00M,
                                ShelterID = "Shltr1"
                    };
                    _imgPath = "images/butters.jpg";
                }
                _animal = DBMgr.GetSingleAnimal(ID);
                _imgPath = "images/" + _animal.ID + ".png";
                base.OnAfterRender(firstRender);
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}