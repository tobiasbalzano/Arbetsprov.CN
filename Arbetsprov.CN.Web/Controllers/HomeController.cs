using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Arbetsprov.CN.BLL.Services;
using Arbetsprov.CN.Models.Entities;
using Arbetsprov.CN.Web.ViewModels.Home;
using Newtonsoft.Json;

namespace Arbetsprov.CN.Web.Controllers
{
    public class HomeController : Controller
    {
        SpotifyService service => new SpotifyService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Select()
        {
            return View();
        }

        public ActionResult Recommend()
        {
            var artistsA = Request.Form.GetValues("artists");
            var tracksA = Request.Form.GetValues("tracks");
            List<Artist> artists = artistsA.Any() ? JsonConvert.DeserializeObject<List<Artist>>(artistsA[0]) : null;
            List<Track> tracks = tracksA.Any() ? JsonConvert.DeserializeObject<List<Track>>(tracksA[0]) : null;
            var result = service.GetRecommendations(artists, tracks);
            if (result != null)
            {
                var vm = new RecommendationViewModel
                {
                    Tracks = result
                };
                return View(vm);
            }
            return RedirectToAction("Error");
        }

        public ActionResult GetPlaylists(string category)
        {
            var result = service.GetPlaylists(category);
            if (result != null)
                return Json(result, JsonRequestBehavior.AllowGet);
            return RedirectToAction("Error");
        }


        public ActionResult GetTracks(string tracksUrl)
        {
            var result = service.GetPlaylistTracks(tracksUrl);
            if (result != null)
                return Json(result, JsonRequestBehavior.AllowGet);
            return RedirectToAction("Error");
        }

        public ActionResult GetCategories()
        {
            var result = service.GetCategories();
            if (result != null)
                return Json(result, JsonRequestBehavior.AllowGet);
            return RedirectToAction("Error");
        }

        public ActionResult Error()
        {
            Response.StatusCode = 400;

            return new HttpStatusCodeResult(400, "Could not complete request");
        }

    }
}