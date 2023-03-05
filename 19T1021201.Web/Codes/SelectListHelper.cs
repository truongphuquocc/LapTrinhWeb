using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _19T1021201.DomainModels;
using _19T1021201.BusinessLayers;

namespace _19T1021201.Web
{
    /// <summary>
    /// Cung cấp hàm tiện ích liên quan đến SelectList
    /// </summary>
    public static class SelectListHelper
    {
        /// <summary>
        /// Lấy danh sách các quốc gia
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> Countries()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "",
                Text = "--Chọn quốc gia--",
            });
            foreach (var item in CommonDataService.ListOfCountries())
            {
                list.Add(new SelectListItem()
                {
                    Value = item.CountryName,
                    Text = item.CountryName
                });
            }

            return list;
        }

        /// <summary>
        /// lấy danh sách loại hàng
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> Categories()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "0",
                Text = "--Chọn loại hàng--"
            });
            foreach (var item in CommonDataService.ListOfCategorys())
            {
                list.Add(new SelectListItem()
                {

                    Value = item.CategoryID.ToString(),
                    Text = item.CategoryName

                });
            }
            return list;
        }

        /// <summary>
        /// lấy danh sách các nhà cung cấp
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> Suppliers()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "0",
                Text = "--Chọn nhà cung cấp--"
            });
            foreach (var item in CommonDataService.ListOfSuppliers(""))
            {
                list.Add(new SelectListItem()
                {

                    Value = item.SupplierID.ToString(),
                    Text = item.SupplierName

                });
            }
            return list;
        }
    }
}