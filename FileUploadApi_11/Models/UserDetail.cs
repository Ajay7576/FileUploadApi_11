using System.ComponentModel.DataAnnotations;

namespace FileUploadApi_11.Models
{
    public class UserDetail
    {
        public Guid Id  { get; set; }
        [Required]
        public string AccountCode { get; set; }

    }
}
