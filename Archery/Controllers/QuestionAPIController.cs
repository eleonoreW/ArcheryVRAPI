using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Archery.Models;
using System.Linq;

namespace Archery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionAPIController : Controller
    {
        private readonly ArcheryVRContext _context;

        public QuestionAPIController(ArcheryVRContext context)
        {
            _context = context;
        }

        // GET: api/QuestionAPI
        public JsonResult GetQuestion()
        {
            List<QuestionWithList> myList = new List<QuestionWithList>();

            foreach (Question q in _context.Question.ToList())
            {
                myList.Add(new QuestionWithList(q.GradeId, q.SujetId, q.QuestionText, q.Difficult, q.Explanation, q.GoodAnswers, q.BadAnswers));
            }
            var jsonResult = new { listQuizz = myList };
            return Json(jsonResult);
        }
    }
}
