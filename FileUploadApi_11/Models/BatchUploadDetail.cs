using Microsoft.EntityFrameworkCore.Query;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileUploadApi_11.Models
{
    public class BatchUploadDetail 
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Guid FileUploadId { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string AccountCode  { get; set; }
        [Required]
        public string TempleteTypes { get; set; }

    }
}
