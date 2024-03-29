﻿namespace ProductBuilder.Domain.Commands.Product.Base
{
    using Asd.Domain.Core.Commands;
    using System;
    public abstract class ProductCommand : AsdCommand
    {
        public Guid Id { get; protected set; }
        public string Title { get; protected set; }
        public string ProductVision { get; protected set; }
    }
}