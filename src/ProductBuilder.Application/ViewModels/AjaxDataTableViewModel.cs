namespace ProductBuilder.Application.ViewModels
{
    using System.Collections.Generic;

    public class AjaxDataTableViewModel
    {
        public int Draw { get; set; }

        public int RecordsTotal { get; set; }

        public int RecordsFiltered { get; set; }

        public IEnumerable<IEnumerable<string>> Data { get; set; }
    }
}
