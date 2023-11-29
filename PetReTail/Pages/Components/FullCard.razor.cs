using System.Configuration;
using Microsoft.AspNetCore.Components;
using PetReTail.Data;

namespace PetReTail.Pages.Components
{
    public partial class FullCard
    {
        // [Parameter]
        // public string ID { get; set; }
        private AnimalModel _animal = new AnimalModel(){
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
    }
}