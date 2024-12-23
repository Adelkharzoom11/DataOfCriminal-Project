using System.ComponentModel.DataAnnotations.Schema;

namespace ApiProject.Domain
{
    public class Address
    {
        public Guid Id { get; set; }
        public string Governorate { get; set; } // المحافظة
        public string city { get; set; }
        public string village { get; set; } // القرية
        public string district { get; set; } // الحي


        public Guid CriminalId { get; set; }
        public Criminal Criminal { get; set; }
    }
}
