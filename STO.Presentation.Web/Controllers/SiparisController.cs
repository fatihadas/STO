using STO.Presentation.Web.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using STO.Data.Context;
using STO.Data.UnitOfWork;
using STO.Data.Model;
using STO.Data.Repositories;
using STO.Presentation.Web.Models;

namespace STO.Presentation.Web.Controllers
{
    [ControlLogin]
    public class SiparisController : BaseController
    {
        public ActionResult Yeni()
        {
            return View(GetModel());
        }

        public SiparisYeni GetModel()
        {
            SiparisYeni models = new SiparisYeni();
            models.Siparis = new Sipari();

            List<SelectListItem> urunListItem = new List<SelectListItem>();
            foreach (var item in _uow.GetRepository<Urun>().GetAll().ToList())
            {
                urunListItem.Add(new SelectListItem
                {
                    Text = item.UrunAdi.ToString(),
                    Value = item.Id.ToString()
                });
            }
            models.urunListItem = urunListItem;

            List<SelectListItem> musteriListItem = new List<SelectListItem>();
            foreach (var item in _uow.GetRepository<Musteri>().GetAll().ToList())
            {
                musteriListItem.Add(new SelectListItem
                {
                    Text = item.MusteriAdiSoyadi.ToString(),
                    Value = item.Id.ToString()
                });
            }
            models.musteriListItem = musteriListItem;

            List<SelectListItem> siparisDurumListItem = new List<SelectListItem>();
            foreach (var item in _uow.GetRepository<SiparisDurum>().GetAll().ToList())
            {
                siparisDurumListItem.Add(new SelectListItem
                {
                    Text = item.Durum.ToString(),
                    Value = item.Id.ToString()
                });
            }
            models.siparisDurumListItem = siparisDurumListItem;

            return models;
        }


        [HttpPost]
        public ActionResult Yeni(SiparisYeni model)
        {
            Sipari siparis = new Sipari();
            siparis.UrunId = model.Siparis.UrunId;
            siparis.MusteriId = model.Siparis.MusteriId;
            siparis.SiparisAdet = model.Siparis.SiparisAdet;
            siparis.SiparisDurum = model.Siparis.SiparisDurum;
            siparis.EklenmeTarihi = DateTime.Now;
            siparis.SiparisTarihi = DateTime.Now;

            _uow.GetRepository<Sipari>().Add(siparis);
            _uow.SaveChanges();
            
            model = GetModel();
            model.Siparis = _uow.GetRepository<Sipari>().GetAll().OrderByDescending(x => x.SiparisId).FirstOrDefault();

            return View(model);
        }


        public ActionResult Liste()
        {
            return View(_uow.GetRepository<Sipari>().GetAll().ToList());
        }


        public ActionResult Sil(int? id)
        {
            if (Delete(id))
            {
                return RedirectToAction("Liste", "Siparis");
            }
            else
            {
                TempData["Error"] = _uow.GetRepository<Sipari>().GetById(Convert.ToInt32(id)).SiparisId.ToString() + " id'li sipariş silinemedi.";
                
                return RedirectToAction("Hata", "Genel");
            }
        }

        public bool Delete(int? id)
        {
            try
            {
                Sipari siparis = _uow.GetRepository<Sipari>().GetById(Convert.ToInt32(id));
                _uow.GetRepository<Sipari>().Delete(siparis);
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
            return View(_uow.GetRepository<Sipari>().GetById(Convert.ToInt32(id)));
        }

        public ActionResult Duzenle(int? id)
        {
            return View(GetSelectedModel(id));
        }

        public SiparisYeni GetSelectedModel(int? id)
        {
            SiparisYeni models = new SiparisYeni();
            models.Siparis = _uow.GetRepository<Sipari>().GetById(Convert.ToInt32(id));
            
            List<SelectListItem> urunListItem = new List<SelectListItem>();
            foreach (var item in _uow.GetRepository<Urun>().GetAll().ToList())
            {
                urunListItem.Add(new SelectListItem
                {
                    Text = item.UrunAdi.ToString(),
                    Value = item.Id.ToString()
                });
            }
            models.urunListItem = urunListItem;

            List<SelectListItem> musteriListItem = new List<SelectListItem>();
            foreach (var item in _uow.GetRepository<Musteri>().GetAll().ToList())
            {
                musteriListItem.Add(new SelectListItem
                {
                    Text = item.MusteriAdiSoyadi.ToString(),
                    Value = item.Id.ToString()
                });
            }
            models.musteriListItem = musteriListItem;

            List<SelectListItem> siparisDurumListItem = new List<SelectListItem>();
            foreach (var item in _uow.GetRepository<SiparisDurum>().GetAll().ToList())
            {
                siparisDurumListItem.Add(new SelectListItem
                {
                    Text = item.Durum.ToString(),
                    Value = item.Id.ToString()
                });
            }
            models.siparisDurumListItem = siparisDurumListItem;

            return models;
        }


        [HttpPost]
        public ActionResult Duzenle(SiparisYeni model, int? id)
        {
            return View(SiparisDuzenle(model, id));
        }

        public SiparisYeni SiparisDuzenle(SiparisYeni model, int? id)
        {
            Sipari siparis = _uow.GetRepository<Sipari>().GetById(Convert.ToInt32(id));
            
            siparis.UrunId = model.Siparis.UrunId;
            siparis.MusteriId = model.Siparis.MusteriId;
            siparis.SiparisAdet = model.Siparis.SiparisAdet;
            //siparis.SiparisTarihi = model.Siparis.SiparisTarihi;
            siparis.EklenmeTarihi = DateTime.Now;
            siparis.SiparisDurum = model.Siparis.SiparisDurum;

            _uow.GetRepository<Sipari>().Update(siparis);
            _uow.SaveChanges();

            model = GetSelectedModel(id);
            model.Siparis = siparis;
            return model;
        }


    }
}