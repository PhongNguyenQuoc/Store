﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Store.Models.DAO;
using Store.Models.EF;
namespace Store.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            ViewBag.Temp = "";
            var slide = new SlideDAO().slide();
            var tablet = new ProductDAO().Tablet();
            ViewBag.Tablet = tablet;
            var apple = new ProductDAO().Apple();
            ViewBag.Apple = apple;
            var sound = new ProductDAO().Sound();
            ViewBag.Sound = sound;
            var newproduct = new ProductDAO().New_Products();
            ViewBag.NewProduct = newproduct;
            var ads = new CommonDAO().ads();
            ViewBag.ADS = ads;
            return View(slide);
        }

        public ActionResult PromotionPartial()
        {
            var promotion = new NewDAO().promotion();
            return PartialView(promotion);
        }
        public ActionResult ProductPartial()
        {
            return PartialView();
        }
        public ActionResult MenuPartial()
        {
            var menu = new ProductDAO().products();
            return View(menu);
        }
        public ActionResult FooterPartial()
        {
            var footer = new CommonDAO().footer();
            return PartialView(footer);
        }
        public ActionResult ShowProducts(string MaLoaiSP, string MaNSX, int page = 1, int pageSize = 10)
        {
            if (MaNSX == "null")
            {
                IEnumerable<SanPham> lst1 = new ProductDAO().showbycategories(MaLoaiSP);
                var data1 = lst1.Skip(page).Take(pageSize).ToList();
                ViewData["MaLoaiNSX"] = MaNSX;
                return View(data1);
            }
            IEnumerable<SanPham> lst = new ProductDAO().showproducts(MaLoaiSP, MaNSX);
            var data = lst.Skip(page).Take(pageSize).ToList();
            return View(data);
        }

        
        public ActionResult Search(string TenSP)
        {

            DBStore db = new DBStore();
            if (TenSP == null)
            {
                return RedirectToAction("Index", "Home");
            }
            IEnumerable<SanPham> lst = db.SanPhams.Where(x=>x.TenSP.Contains(TenSP));
            return View(lst);
        }
    }
}