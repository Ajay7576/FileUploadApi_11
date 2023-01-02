using Microsoft.VisualStudio.Shell.TableControl;
using System.ComponentModel.DataAnnotations;

namespace FileUploadApi_11.Models
{
    public class Patient
    {
        public int Id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string address { get; set; }
        [Required]
        public string city { get; set; }
        public float age { get; set; }
        public string gender { get; set; }

    }
}
