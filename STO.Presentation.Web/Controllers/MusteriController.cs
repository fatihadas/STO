using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using STO.Presentation.Web.data;
//using STO.Data.Context;
//using STO.Data.UnitOfWork;
using STO.Data.Model;
//using STO.Data.Repositories;
using STO.Presentation.Web.Models;
using STO.Business.UnitOfWork;

namespace STO.Presentation.Web.Controllers
{

    [ControlLogin]
    public class MusteriController : Controller
    {
        private readonly IUnitOfWork _uow;

        public MusteriController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public ActionResult Yeni()
        {
            return View(GetModel());
        }

        
        public MusteriYeni GetModel()
        {
            MusteriYeni models = new MusteriYeni();
            models.Musteri = new Musteri();

            List<SelectListItem> illerListItem = new List<SelectListItem>();

            foreach (var item in _uow.GetRepository<Sehir>().GetAll().ToList())
            {
                illerListItem.Add(new SelectListItem
                {
                    Text = item.SehirAdi.ToString(),
                    Value = item.SehirId.ToString()
                });
            }
            models.illerListItem = illerListItem;
            return models;
        }

        [HttpPost]
        public ActionResult Yeni(MusteriYeni model)
        {
            _uow.GetRepository<Musteri>().Add(model.Musteri);
            _uow.SaveChanges();
            
            model = GetModel();

            model.Musteri = _uow.GetRepository<Musteri>().GetAll().OrderByDescending(x => x.Id).FirstOrDefault();
            return View(model);
        }

        public ActionResult Liste()
        {
            return View(_uow.GetRepository<Musteri>().GetAll().ToList());
        }


        public ActionResult Sil(int? id)
        {
            if (Delete(id))
            {
                return RedirectToAction("Liste", "Musteri");
            }
            else
            {
                TempData["Error"] = _uow.GetRepository<Musteri>().GetById(Convert.ToInt32(id)).MusteriAdiSoyadi.ToString() + " adlı müşteri silinemedi.";
                
                return RedirectToAction("Hata", "Genel");
            }
        }
        public bool Delete(int? id)
        {
            try
            {
                Musteri musteri = _uow.GetRepository<Musteri>().GetById(Convert.ToInt32(id));
                _uow.GetRepository<Musteri>().Delete(musteri);
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
            return View(_uow.GetRepository<Musteri>().GetById(Convert.ToInt32(id)));
        }

        public ActionResult Duzenle(int? id)
        {
            return View(GetSelectedModel(id));
        }

        public MusteriYeni GetSelectedModel(int? id)
        {
            MusteriYeni models = new MusteriYeni();
            models.Musteri = _uow.GetRepository<Musteri>().GetById(Convert.ToInt32(id));

            List<SelectListItem> illerListItem = new List<SelectListItem>();
            foreach (var item in _uow.GetRepository<Sehir>().GetAll().ToList())
            {
                illerListItem.Add(new SelectListItem
                {
                    Text = item.SehirAdi.ToString(),
                    Value = item.SehirId.ToString()
                });
            }
            models.illerListItem = illerListItem;
            return models;
        }


        [HttpPost]
        public ActionResult Duzenle(MusteriYeni model, int? id)
        {
            return View(MusteriDuzenle(model, id));
        }

        public MusteriYeni MusteriDuzenle(MusteriYeni model, int? id)
        {
            Musteri musteri = _uow.GetRepository<Musteri>().GetById(Convert.ToInt32(id));
            musteri.MusteriAdres = model.Musteri.MusteriAdres;
            musteri.MusteriAdiSoyadi = model.Musteri.MusteriAdiSoyadi;
            musteri.MusteriTel = model.Musteri.MusteriTel;
            musteri.MusteriSehirId = model.Musteri.MusteriSehirId;
            musteri.Username = model.Musteri.Username;
            musteri.Password = model.Musteri.Password;

            _uow.GetRepository<Musteri>().Update(musteri);
            _uow.SaveChanges();
            
            model = GetModel();
            model.Musteri = musteri;
            return model;
        }


    }
}