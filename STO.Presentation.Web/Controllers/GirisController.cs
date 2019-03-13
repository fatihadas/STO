//using STO.Data.Context;
//using STO.Data.Model;
//using STO.Data.Repositories;
//using STO.Data.UnitOfWork;
using STO.Business.UnitOfWork;
using STO.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace STO.Presentation.Web.Controllers
{
    public class GirisController : Controller
    {
        //IUnitOfWork

        private readonly IUnitOfWork _uow;

        public GirisController(IUnitOfWork uow)
        {
            _uow = uow;
        }


        [HttpPost]
        public ActionResult Islem(string username, string password)
        {
            if (IsLoginSuccess(username, password))
            {
                return RedirectToAction("Durum", "Genel");
            }
            return RedirectToAction("Islem", "Giris");
        }

        public bool IsLoginSuccess(string user, string pass)
        {

            Kullanici kullanici = _uow.GetRepository<Kullanici>().GetAll().Where(x => x.KullaniciAdi.Equals(user) && x.KullaniciSifre.Equals(pass)).FirstOrDefault();

            if (kullanici != null)
            {
                Session.Add("SonGirisTarihi", kullanici.SonGirisTarihi);

                kullanici.SonGirisTarihi = DateTime.Now.ToString();

                //kullanici.KullaniciAdi = model.KullaniciAdi;
                //kullanici.KullaniciSifre = model.KullaniciSifre;
                //kullanici.KullaniciAdiSoyadi = model.KullaniciAdiSoyadi;
                //kullanici.KullaniciEmail = model.KullaniciEmail;
                //kullanici.KullaniciResimUrl = model.KullaniciResimUrl;

                Session.Add("KullaniciId", kullanici.KullaniciId);
                Session.Add("KullaniciAdi", kullanici.KullaniciAdi);
                Session.Add("KullaniciSifre", kullanici.KullaniciSifre);

                Session.Add("KullaniciAdiSoyadi", kullanici.KullaniciAdiSoyadi);
                Session.Add("KullaniciEmail", kullanici.KullaniciEmail);
                Session.Add("KullaniciResimUrl", kullanici.KullaniciResimUrl);

                _uow.GetRepository<Kullanici>().Update(kullanici);
                _uow.SaveChanges();

                return true;
            }
            return false;
        }


        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Login", "Giris");
        }




        public ActionResult Login()
        {
            if (HttpContext.Session["KullaniciId"] != null)
            {
                return RedirectToAction("Durum", "Genel");
            }

            return View();
        }


        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            Kullanici kullanici = _uow.GetRepository<Kullanici>().GetAll().Where(x => x.KullaniciAdi.Equals(username) && x.KullaniciSifre.Equals(password)).FirstOrDefault();
            if (kullanici != null)
            {
                Session.Add("SonGirisTarihi", kullanici.SonGirisTarihi);
                kullanici.SonGirisTarihi = DateTime.Now.ToString();

                _uow.GetRepository<Kullanici>().Update(kullanici);
                _uow.SaveChanges();

                Session.Add("KullaniciId", kullanici.KullaniciId);
                Session.Add("KullaniciAdi", kullanici.KullaniciAdi);
                Session.Add("KullaniciSifre", kullanici.KullaniciSifre);
                Session.Add("KullaniciAdiSoyadi", kullanici.KullaniciAdiSoyadi);
                Session.Add("KullaniciEmail", kullanici.KullaniciEmail);
                Session.Add("KullaniciResimUrl", kullanici.KullaniciResimUrl);

                return RedirectToAction("Durum", "Genel");
            }
            else
            {
                return RedirectToAction("Login", "Giris");
            }

        }

    }
}