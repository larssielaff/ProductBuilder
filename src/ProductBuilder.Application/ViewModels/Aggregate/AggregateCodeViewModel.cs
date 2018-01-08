using System.Collections.Generic;

namespace ProductBuilder.Application.ViewModels.Aggregate
{
    public class AggregateCodeViewModel
    {
        public string AggregateClassName { get; set; }

        public string AggregateCode { get; set; }

        public string BaseCommandClassName { get; set; }

        public string BaseCommandCode { get; set; }

        public string AggregateValidatorClassName { get; set; }

        public string AggregateValidatorCode { get; set; }

        public string IAggregateRepositoryInterfaceName { get; set; }

        public string IAggregateRepositoryCode { get; set; }

        public string AggregateRepositoryClasseName { get; set; }

        public string AggregateRepositoryCode { get; set; }
        
        public IDictionary<string, string> DomainEvents { get; set; }

        public IDictionary<string, string> DomainCommands { get; set; }

        public string AggregateEventHandlerClasseName { get; set; }

        public string AggregateEventHandlerCode { get; set; }
    }
}
