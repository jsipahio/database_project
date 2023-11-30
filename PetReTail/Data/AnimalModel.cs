namespace PetReTail.Data
{
    public struct AnimalModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Breed { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public DateTime DateReceived { get; set; }
        public bool IsVaxed { get; set; }
        public bool IsFixed { get; set; }
        public Decimal Fee { get; set; }
        public string ShelterID { get; set; }
        public string Description { get; set; }
    }
}