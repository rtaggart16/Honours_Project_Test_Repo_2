using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Honours_Project_Test_2.Models.ViewModels.Author;
using Honours_Project_Test_2.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Honours_Project_Test_2.Controllers
{
    [Route("api/author")]
    public class AuthorsController : Controller
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [Route("get/all")]
        [HttpGet]
        public List<Basic_Author_Info> Get_All_Authors()
        {
            return _authorService.Get_All_Authors();
        }
    }
}
