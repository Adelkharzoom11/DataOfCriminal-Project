using ApiProject.Context;
using ApiProject.Core.Interface;
using ApiProject.Domain;
using ApiProject.Dtos;

namespace ApiProject.Core.Servicers
{
    public class CriminalSrvice : ICriminalSrvice
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CriminalSrvice(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task AddCriminalAsync(CriminalDto criminal)
        {
            // تحقق من صحة الصورة (PNG أو JPG فقط)
            if (criminal.imageCriminal != null)
            {
                var extension = Path.GetExtension(criminal.imageCriminal.FileName).ToLower();

                if (extension != ".png" && extension != ".jpg" && extension != ".jpeg")
                {
                    throw new InvalidOperationException("يجب ان تكون نوع الصورة حصرا (png , jpg , jpeg)");
                }
                    
            }

            // حفظ الصورة في wwwroot/Images
            string imagePath = null;
            if (criminal.imageCriminal != null)
            {
                string imagesFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                if (!Directory.Exists(imagesFolder))
                    Directory.CreateDirectory(imagesFolder);

                string uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(criminal.imageCriminal.FileName)}";
                imagePath = Path.Combine("Images", uniqueFileName);
                string fullPath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await criminal.imageCriminal.CopyToAsync(stream);
                }
            }

            var crim = new Criminal
            {
                Id = Guid.NewGuid(),
                personalEmail = criminal.personalEmail,
                nameCriminal = criminal.nameCriminal,
                nickName = criminal.nickName,
                fatherName = criminal.fatherName,
                MotherName = criminal.MotherName,
                Adjective = criminal.Adjective,
                imageCriminalURL = imagePath,
                phoneNumberCriminal = criminal.phoneNumberCriminal,
                facebookCriminalURL = criminal.facebookCriminalURL,
                DescriptionOfCriminal = criminal.DescriptionOfCriminal,

                address = new Address
                {
                    Id = Guid.NewGuid(),
                    Governorate = criminal.Address.Governorate,
                    city = criminal.Address.city,
                    village = criminal.Address.village,
                    district = criminal.Address.district,

                }
            };

            // إضافة المستخدم والعنوان إلى السياق وحفظهما
            await _context.Criminals.AddAsync(crim);
            await _context.SaveChangesAsync();
        }

    }
    
}
