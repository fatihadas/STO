using STO.Data.Model;
using System.Collections.Generic;
using System.Web.Mvc;

namespace STO.Presentation.Web.Models
{
    public class SiparisYeni
    {
        public Sipari Siparis { get; set; }
        public List<SelectListItem> urunListItem { get; set; }
        public List<SelectListItem> musteriListItem { get; set; }
        public List<SelectListItem> siparisDurumListItem { get; set; }

    }
}