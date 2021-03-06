﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BlogEngine.Core.Data.Entities.Common;
using BlogEngine.Core.Data.Entities.JoiningEntities;
using BlogEngine.Shared.Validations;

namespace BlogEngine.Core.Data.Entities
{
    public class Category : BaseEntity
    {
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} should not be more than 100 Characters")]
        [FirstLetterUppercase]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        public byte[] GeneralCover { get; set; }

        public List<BlogCategory> BlogCategories { get; set; } = new List<BlogCategory>();
    }
}