using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ListaCursos.Interfaces;
using ListaCursos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ListaCursos.Pages
{
    public class CoursesModel : PageModel
    {
        private readonly ICoursesProvider coursesProvider;
        public List<Course> Courses { get; set; }

        [BindProperty(SupportsGet =true)]
        public string Search { get; set; }

        public CoursesModel(ICoursesProvider CoursesProvider) {
            this.coursesProvider = CoursesProvider;
        }
        public async Task<IActionResult> OnGet()
        {
            if (!string.IsNullOrEmpty(Search))
            {
                var result = await coursesProvider.SearchAsync(Search);
                if(result != null)
                {
                    Courses = new List<Course>(result);
                }
            }
            else
            {
                var result = await coursesProvider.GetAllAsync();
                if (result != null)
                {
                    Courses = new List<Course>(result);
                }
            }
            return Page();


        }
    }
}