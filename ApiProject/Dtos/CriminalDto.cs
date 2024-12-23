namespace ApiProject.Dtos
{
    public class CriminalDto
    {
        public string personalEmail { get; set; }
        public string nameCriminal { get; set; }
        public string nickName { get; set; }
        public string fatherName { get; set; }
        public string MotherName { get; set; }
        public string Adjective { get; set; }
        public IFormFile imageCriminal { get; set; }
        public string phoneNumberCriminal { get; set; }
        public string facebookCriminalURL { get; set; }
        public string DescriptionOfCriminal { get; set; }

        public AddressDto Address { get; set; }
    }
}
