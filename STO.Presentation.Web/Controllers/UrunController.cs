using STO.Presentation.Web.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using STO.Data.UnitOfWork;
using STO.Data.Model;
//using STO.Data.Repositories;
using STO.Presentation.Web.Models;
//using STO.Data.Context;

namespace STO.Presentation.Web.Controllers
{
    [ControlLogin]
    public class UrunController : BaseController
    {
        public ActionResult Ekle()
        {
            return RedirectToAction("Yeni", "Urun");
        }

        public ActionResult Yeni()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Yeni(Urun model)
        {
            _uow.GetRepository<Urun>().Add(model);
            _uow.SaveChanges();
            
            return View(model);
        }

        public ActionResult Liste()
        {
            return View((IEnumerable<Urun>)_uow.GetRepository<Urun>().GetAll().ToList());
        }


        public ActionResult Sil(int? id)
        {
            if (Delete(id))
            {
                return RedirectToAction("Liste", "Urun");
            }
            else
            {
                TempData["Error"] = _uow.GetRepository<Urun>().GetById(Convert.ToInt32(id)).UrunAdi.ToString() + " adlı ürün silinemedi.";

                return RedirectToAction("Hata", "Genel");
            }
        }

        public bool Delete(int? id)
        {
            try
            {
                Urun urun = _uow.GetRepository<Urun>().GetById(Convert.ToInt32(id));
                _uow.GetRepository<Urun>().Delete(urun);
                _uow.SaveChanges();
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public ActionResult Goster(int? id)
        {
            return View(_uow.GetRepository<Urun>().GetById(Convert.ToInt32(id)));
        }

        public ActionResult Duzenle(int? id)
        {

            return View(_uow.GetRepository<Urun>().GetById(Convert.ToInt32(id)));
        }

        [HttpPost]
        public ActionResult Duzenle(Urun model)
        {
            return View(UrunDuzenle(model));
        }

        public Urun UrunDuzenle(Urun model)
        {
            Urun urun = _uow.GetRepository<Urun>().GetById(Convert.ToInt32(model.Id));
            urun.UrunAciklama = model.UrunAciklama;
            urun.UrunAdet = model.UrunAdet;
            urun.UrunAdi = model.UrunAdi;
            urun.UrunBarkod = model.UrunBarkod;
            urun.UrunFiyat = model.UrunFiyat;

            _uow.GetRepository<Urun>().Update(urun);
            _uow.SaveChanges();
            
            return urun;
        }

    }
}