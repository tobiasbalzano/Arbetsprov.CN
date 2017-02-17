using System.Collections.Generic;
using Arbetsprov.CN.Models.Entities;

namespace Arbetsprov.CN.Web.ViewModels.Home
{
    public class RecommendationViewModel
    {
        public IList<Track> Tracks { get; set; }
    }
}