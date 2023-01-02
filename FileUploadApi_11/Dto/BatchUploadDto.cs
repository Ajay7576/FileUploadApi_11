using System.ComponentModel.DataAnnotations;

namespace FileUploadApi_11.Dto
{
    public class BatchUploadDto 
    {
        //public int Id { get; set; }
        [Required]
        public Guid FileUploadId { get; set; }
        [Required]
        //public string FileName { get; set; }
        //[Required]
        public Guid UserId { get; set; }
        public string TempleteTypes { get; set; }
    }
}
