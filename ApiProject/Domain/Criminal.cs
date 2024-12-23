using System.ComponentModel.DataAnnotations;

namespace ApiProject.Domain
{
    public class Criminal
    {
        public Guid Id { get; set; }
        [EmailAddress]
        public string personalEmail { get; set; }
        public string nameCriminal { get; set; }
        public string nickName { get; set; }
        public string fatherName { get; set; }
        public string MotherName { get; set; }
        public string Adjective { get; set; }
        public string imageCriminalURL { get; set; }
        public string phoneNumberCriminal { get; set; }
        public string facebookCriminalURL { get; set; }
        public string DescriptionOfCriminal {  get; set; }
 

        public Address address {  get; set; }
    }
}
