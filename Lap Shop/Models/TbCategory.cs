﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lap_Shop.Models;

public partial class TbCategory
{
    [ValidateNever]
    public int CategoryId { get; set; }

    [Required (ErrorMessage ="please enter category name")]
    public string CategoryName { get; set; } = null!;
    [ValidateNever]
    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public int CurrentState { get; set; }
    [ValidateNever]
    public string ImageName { get; set; } = null!;

    public bool ShowInHomePage { get; set; }
    
    public string? UpdatedBy { get; set; }
    
    public DateTime? UpdatedDate { get; set; }

    public virtual ICollection<TbItem> TbItems { get; set; } = new List<TbItem>();
}
