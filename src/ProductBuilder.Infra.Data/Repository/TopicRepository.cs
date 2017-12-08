namespace ProductBuilder.Infra.Data.Repository
{
    using Asd.Infra.Data.Context;
    using Asd.Infra.Data.Repository;
    using ProductBuilder.Domain.Interfaces;
    using ProductBuilder.Domain.Models;

    public class TopicRepository : AsdRepository<Topic>, ITopicRepository
    {
        public TopicRepository(AsdSqlContext context) 
            : base(context) { }
    }
}