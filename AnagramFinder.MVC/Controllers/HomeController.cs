using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using AnagramFinder.MVC.Helpers;
using AnagramFinder.MVC.Models;

namespace AnagramFinder.MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var content = Session["content"] as string ?? "";
            var results = Session["results"] as List<ResultViewModel> ?? new List<ResultViewModel>();
            var model = new ContentViewModel() { Content = content, Results = results };
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(SubmitViewModel submitViewModel)
        {
            if (ModelState.IsValid)
            {
                var reader = new BinaryReader(submitViewModel.SubmittedFile.InputStream); // Stream from file
                byte[] data = reader.ReadBytes((int)submitViewModel.SubmittedFile.InputStream.Length); // read the raw data
                Session["content"] = Encoding.UTF8.GetString(data); // put into session
                Session["results"] = new List<ResultViewModel>(); // new results 
            }

            var model = new ContentViewModel()
                {
                    Content = Session["content"] as string,
                    Results = Session["results"] as List<ResultViewModel>
                };

            return View(model);
        }
        
        [HttpGet, AjaxOnly]
        public ActionResult Search(string term)
        {
            var contents = Session["content"] as string;

            if (term.HasValue())
            {
                // Search for anagrams of a particular search term

                var model = new ResultViewModel {Anagrams = new List<string>(), Term = term};

                Stopwatch startNew = Stopwatch.StartNew();

                var orderedTerm = term.ToLower().OrderBy(x => x);

                var anagrams = contents.Split(new[] { " ", "\n", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                                       .Distinct()
                                       .Where(word => term.Length == word.Length && !string.Equals(word, term, StringComparison.CurrentCultureIgnoreCase))
                                       .Where(word => orderedTerm.SequenceEqual(word.ToLower().OrderBy(character => character)))
                                       .ToList();
                startNew.Stop();
                model.Anagrams.AddRange(anagrams);
                model.Elapsed = startNew.ElapsedMilliseconds;

                return PartialView("_Result", model);
            }
            else
            {
                // Find everything we can which may have an anagram
                Stopwatch startNew = Stopwatch.StartNew();

                var models = new List<ResultViewModel>();

                var groups = from word in contents.Split(new[] {" ", "\n", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                             group word by new String(word.ToCharArray().OrderBy(x => x).ToArray()) into collection
                             where collection.Count() > 1
                             orderby collection.Count() descending
                             select new { collection, collection.Key };

                var results = groups.ToList();
                
                startNew.Stop();
                
                results.ForEach(x =>
                    {
                        var model = new ResultViewModel()
                            {
                                Anagrams = new List<string>(x.collection.ToArray()),
                                Term = x.Key, 
                                Elapsed = startNew.ElapsedMilliseconds
                            };

                        models.Add(model);
                    });

                return PartialView("_Results", models);
            }
        }
    }
}
