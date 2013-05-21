using System.ComponentModel.DataAnnotations;
using System.Web;

namespace AnagramFinder.MVC.Models
{
    public class SubmitViewModel
    {
        [Required(ErrorMessage = "Select a file to upload.")]
        public HttpPostedFileBase SubmittedFile { get; set; }
    }
}