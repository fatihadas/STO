using STO.Data.Context;
using STO.Data.Model;
using STO.Data.Model.OtherModels;
using STO.Data.Repositories;
using STO.Data.UnitOfWork;
using STO.Presentation.Web.data;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace STO.Presentation.Web.Controllers
{
    [ControlLogin]
    public class GenelController : BaseController
    {
        public ActionResult Durum()
        {
            return View(GetDurumModel());
        }

        public DurumModel GetDurumModel()
        {
            DurumModel models = new DurumModel();

            models.KargoluSiparisSayisi = _uow.GetRepository<Sipari>().GetAll().Where(x => x.SiparisDurum == 4).ToList().Count;
            models.MüsteriSayisi = _uow.GetRepository<Musteri>().GetAll().Count();
            models.SiparisSayisi = _uow.GetRepository<Sipari>().GetAll().Count();
            models.UrunSayisi = _uow.GetRepository<Urun>().GetAll().Count();

            return models;
        }


        public ActionResult ProfilDuzenle(int? id)
        {
            return View(_uow.GetRepository<Kullanici>().GetById(Convert.ToInt32(id)));
        }

        [HttpPost]
        public ActionResult ProfilDuzenle(Kullanici model, int? id)
        {
            return View(ProfilimiDuzenle(model, id));
        }

        public Kullanici ProfilimiDuzenle(Kullanici model, int? id)
        {

            Kullanici kullanici = _uow.GetRepository<Kullanici>().GetById(Convert.ToInt32(id));
            kullanici.KullaniciAdi = model.KullaniciAdi;
            kullanici.KullaniciSifre = model.KullaniciSifre;
            kullanici.KullaniciAdiSoyadi = model.KullaniciAdiSoyadi;
            kullanici.KullaniciEmail = model.KullaniciEmail;
            kullanici.KullaniciResimUrl = model.KullaniciResimUrl;

            _uow.GetRepository<Kullanici>().Update(kullanici);
            _uow.SaveChanges();

            Session["KullaniciAdi"] = model.KullaniciAdi;
            Session["KullaniciSifre"] = model.KullaniciSifre;
            Session["KullaniciAdiSoyadi"] = model.KullaniciAdiSoyadi;
            Session["KullaniciEmail"] = model.KullaniciEmail;
            Session["KullaniciResimUrl"] = model.KullaniciResimUrl;

            return kullanici;
        }

        public ActionResult Hata()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ResimYukle()
        {
            if (Request.Files.Count > 0)
            {
                var durum = true;
                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase hpfb = Request.Files[file];
                    string path = "/Content/ProfilResimleri/" + "resim-" + hpfb.FileName;
                    try
                    {
                        Request.Files[file].SaveAs(Server.MapPath(path));

                        Kullanici kullanici = _uow.GetRepository<Kullanici>().GetById(Convert.ToInt32(Session["KullaniciId"]));
                        kullanici.KullaniciResimUrl = path;
                        _uow.GetRepository<Kullanici>().Update(kullanici);
                        _uow.SaveChanges();

                        Session["KullaniciResimUrl"] = path;
                    }
                    catch
                    {
                        durum = false;
                        throw;
                    }
                }
                return (durum == true) ? Json(new { Message = "kaydedildi" }, JsonRequestBehavior.AllowGet) :
                    Json(new { Message = "hata" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return View();
            }

        }


    }
}