namespace ProductBuilder.Application.ViewModels
{
    using System.Collections.Generic;

    public class VisJsNetworkApiViewModel
    {
        public IEnumerable<VisJsNetworkNodeApiViewModel> Nodes { get; set; }

        public IEnumerable<VisJsNetworkEdgeApiViewModel> Edges { get; set; }
    }
}
