﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKDotNetCore.RestApi.Models;

[Table("Tbl_Blog")]

public class BlogModel
{
    [Key]
    public int BlogId { get; set; }
    public string? BlogTitle { get; set; }

    public string? BlogAuthor { get; set; }

    public string? BlogContent { get; set; }
}

//lasted way
//public record BlogEntity(int BlogId, string BlogTitle, string BlogAuthor, string BlogContent);
