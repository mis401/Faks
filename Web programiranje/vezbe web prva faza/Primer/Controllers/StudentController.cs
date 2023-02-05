using Microsoft.AspNetCore.Mvc;
using Models;

namespace Primer.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase
{
    public Context Context { get; set; }

    public StudentController(Context context)
    {
        Context = context;
    }
}
