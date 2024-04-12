using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace QuizProgram.Pages
{
    public class IndexModel : PageModel
    {

        [BindProperty]
        public string DTU_mail { get; set; }

        [BindProperty]
        public string DTU_password { get; set; }

        public string Message { get; set; }

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public int GetAccess()
        {

                return 1; 
            
        }

    }

}
