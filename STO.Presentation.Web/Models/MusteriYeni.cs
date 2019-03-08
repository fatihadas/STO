using System.Collections.Generic;
using System.Web.Mvc;
using STO.Data.Model;

namespace STO.Presentation.Web.Models
{
    public class MusteriYeni
    {
        public Musteri Musteri { get; set; }
        public List<SelectListItem> illerListItem { get; set; }
    }
}