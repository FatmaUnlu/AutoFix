﻿using AutoFix.Data;
using AutoFix.Models.Entities;
using AutoFix.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AutoFix.Areas.Admin.Controllers
{
    public class ReportController : AdminBaseController
    {
        private readonly CartRepo _cartRepo;
        private readonly ServiceProductRepo _serviceProductRepo;
        private readonly MyContext _context;

        public ReportController(CartRepo cartRepo, ServiceProductRepo serviceProductRepo, MyContext context)
        {
            _context = context;
            _cartRepo = cartRepo;
            _serviceProductRepo = serviceProductRepo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProductReport()
        {
            //var data1 = _cartRepo.Get(x => x.OrderStatus == OrderStatus.Odendi.ToString()).GroupBy(x => x.ServiceProductId).Select(x => x.ServiceProductId);

            var query = (from Satilanlar in _cartRepo.Get(x => x.OrderStatus == OrderStatus.Odendi.ToString()).AsEnumerable()
                         group Satilanlar by Satilanlar.ServiceProductId
 into newGroup
                         orderby newGroup.Key
                         select new
                         {
                             Id = newGroup.Key,
                             Count = newGroup.Count()
                             //Name=newGroup.Key.Name
                         }).ToList();

            
            //var data1 = _cartRepo.SoldProducts().ToList();          
            //var data = JsonConvert.SerializeObject(data1);

            return View();
        }
        public IActionResult ReportProduct()
        {
            //List<PieData> chartData = new List<PieData>
            //{
            //    new PieData { xValue =  "Chrome", yValue = 37, text = "37%" },
            //    new PieData { xValue =  "UC Browser", yValue = 17, text = "17%" },
            //    new PieData { xValue =  "iPhone", yValue = 19, text = "19%" },
            //    new PieData { xValue =  "Others", yValue = 4, text = "4%" },
            //    new PieData { xValue =  "Opera", yValue = 11, text = "11%" },
            //    new PieData { xValue =  "Android", yValue = 12, text = "12%" },
            //};
            //ViewBag.dataSource = chartData;
            //return View();
            List<ChartData> chartData = new List<ChartData>
            {
                new ChartData { x= "USA", yValue= 46, yValue1=56 },
                new ChartData { x= "GBR", yValue= 27, yValue1=17 },
                new ChartData { x= "CHN", yValue= 26, yValue1=36 },
                new ChartData { x= "UK", yValue= 56,  yValue1=16 },
                new ChartData { x= "AUS", yValue= 12, yValue1=46 },
                new ChartData { x= "IND", yValue= 26, yValue1=16 },
                new ChartData { x= "DEN", yValue= 26, yValue1=12 },
                new ChartData { x= "MEX", yValue= 34, yValue1=32},
            };
            ViewBag.dataSource = chartData;
            return View();
        }

        //public class PieData
        //{
        //    public string xValue;
        //    public double yValue;
        //    public string text;
        //}
        public class ChartData
        {
            public string x;
            public double yValue;
            public double yValue1;
        }
        public IActionResult FailureReport()
        {
            return View();
        }

        public IActionResult GainReport()
        {
            return View();
        }
        public IActionResult TechnicianReport()
        {
            return View();
        }
    }
}
