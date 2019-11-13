using System.ComponentModel.DataAnnotations;

namespace SWHRMS.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}