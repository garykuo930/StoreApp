using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoreApp.Models
{
    [MetadataType(typeof(ProductsMetadata))]
    public partial class Products
    {
    }

    public class ProductsMetadata
    {
        public int ProductID { get; set; }
        [DisplayName("商品類別")]
        public int CategoryID { get; set; }
        [DisplayName("商品編號")]
        public string ModelNumber { get; set; }
        [DisplayName("商品名稱")]
        public string ModelName { get; set; }
        [DisplayName("商品圖片")]
        public string ProductImage { get; set; }
        [DisplayName("單價")]
        [DisplayFormat(DataFormatString = "{0,-15:C0}")]
        public Nullable<decimal> UnitCost { get; set; }
        [DisplayName("商品描述")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public byte[] BytesImage { get; set; }
    }
}