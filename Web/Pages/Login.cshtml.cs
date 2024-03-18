using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repo;

namespace Web.Pages;

public class Login : PageModel
{
    [BindProperty]
    public string Username { get; set; }

    [BindProperty]
    public string Password { get; set; }

    public string ErrorMessage { get; set; }

    private UnitOfWork _uow = new UnitOfWork(new Asm2Context());
    public void OnGet()
    {
        
    }
    
    public IActionResult OnPost()
    {
        var user = _uow.AccountRepository.Get(x => x.UserName == Username && x.Password == Password, null, "", 1, 1).FirstOrDefault();

        if (user != null)
        {
            HttpContext.Session.SetString("Username", user.UserName);
            
            return RedirectToPage("/index");
        }
        
        ErrorMessage = "Invalid username or password.";
        return Page();
    }
}