using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lap_Shop.Models;

public partial class TbItem
{
    public TbItem()
    {
        TbItemDiscounts = new HashSet<TbItemDiscount>();
        TbItemImages = new HashSet<TbItemImage>();
        TbPurchaseInvoiceItems = new HashSet<TbPurchaseInvoiceItem>();
        TbSalesInvoiceItems = new HashSet<TbSalesInvoiceItem>();    
        Customers =new HashSet<TbCustomer>();
    }
    [ValidateNever]
    public int ItemId { get; set; }
    [Required(ErrorMessage ="please enter item name")]

    public string ItemName { get; set; } = null!;
    [Required(ErrorMessage = "please enter sales price")]
    [DataType(DataType.Currency,ErrorMessage ="please enter currency ")]
    [Range(50,100000,ErrorMessage ="please enter price between 50 and 100000")]
    public decimal SalesPrice { get; set; }
    [Required(ErrorMessage = "please enter sales price")]
    [DataType(DataType.Currency, ErrorMessage = "please enter currency ")]
    [Range(50, 100000, ErrorMessage = "please enter price between 50 and 100000")]
    public decimal PurchasePrice { get; set; }
    [Required(ErrorMessage = "please enter Category")]
    public int CategoryId { get; set; }

    public string? ImageName { get; set; }
    [ValidateNever]
    public DateTime CreatedDate { get; set; }
    [ValidateNever]
    public string CreatedBy { get; set; } = null!;
    [ValidateNever]
    public int CurrentState { get; set; }
    [ValidateNever]
    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? Description { get; set; }

    public string? Gpu { get; set; }

    public string? HardDisk { get; set; }

    public int? ItemTypeId { get; set; }

    public string? Processor { get; set; }
    [Required(ErrorMessage = "please enter Category")]
    [Range(1,512 , ErrorMessage ="please enter in ram range")]
    public int? RamSize { get; set; }

    public string? ScreenReslution { get; set; }

    public string? ScreenSize { get; set; }

    public string? Weight { get; set; }
    [Required(ErrorMessage = "please enter the OS")]
    public int? OsId { get; set; }

    public virtual TbCategory? Category { get; set; } = null!;

    public virtual TbItemType? ItemType { get; set; }

    public virtual TbO? Os { get; set; }

    public virtual ICollection<TbItemDiscount> TbItemDiscounts { get; set; } = new List<TbItemDiscount>();

    public virtual ICollection<TbItemImage> TbItemImages { get; set; } = new List<TbItemImage>();

    public virtual ICollection<TbPurchaseInvoiceItem> TbPurchaseInvoiceItems { get; set; } = new List<TbPurchaseInvoiceItem>();

    public virtual ICollection<TbSalesInvoiceItem> TbSalesInvoiceItems { get; set; } = new List<TbSalesInvoiceItem>();

    public virtual ICollection<TbCustomer> Customers { get; set; } = new List<TbCustomer>();
}
