﻿namespace ProductBuilder.Application.ViewModels.AggregatePropertyApi
{
    using System;

    public class CreateAggregateApiViewModel
    {
        public Guid Id { get; set; }

        public Guid LinkedAggregateId { get; set; }

        public bool IsAggregateRoot { get; set; }

        public string Type { get; set; }

        public Guid AsdAggregateId { get; set; }

        public string Name { get; set; }
    }
}