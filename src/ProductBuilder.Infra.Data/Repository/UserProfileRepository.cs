namespace ProductBuilder.Infra.Data.Repository
{
    using Asd.Infra.Data.Context;
    using Asd.Infra.Data.Repository;
    using ProductBuilder.Domain.Interfaces;
    using ProductBuilder.Domain.Models;
    public class UserProfileRepository : AsdRepository<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(AsdSqlContext context) 
            : base(context) { }
    }
}
