using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoreApp.Models;

namespace StoreApp.Models.ViewModel
{
    public class DisplayVM
    {
        public IEnumerable<Categories> Categories { get; set; }
        public IEnumerable<Products> Products { get; set; }
    }
}