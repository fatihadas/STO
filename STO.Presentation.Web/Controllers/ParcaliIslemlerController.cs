//using STO.Data.Context;
using STO.Data.Model;
using STO.Presentation.Web.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace STO.Presentation.Web.Controllers
{
    [ControlLogin]
    public class ParcaliIslemlerController : BaseController
    {
        public PartialViewResult BildirimMenusu()
        {
            return PartialView(SiparisBekleyenListe());
        }

        public List<Sipari> SiparisBekleyenListe()
        {
            return _uow.GetRepository<Sipari>().GetAll().Where(x => x.SiparisDurum == 1).ToList();
        }
    }
}