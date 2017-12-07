﻿namespace ProductBuilder.Domain.Models
{
    using Asd.Domain.Core.Models;
    using System;
    using System.Collections.Generic;

    public class Product : AsdEntity
    {
        public string Title { get; set; }

        public string ProductVision { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }

        public virtual ICollection<Team> Teams { get; set; }

        public Product(Guid id) 
            : this()
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));
            Id = id;
        }

        protected Product()
        {
            UserRoles = new HashSet<UserRole>();
            Teams = new HashSet<Team>();
        }
    }
}