namespace ProductBuilder.Domain.Interfaces
{
    using Asd.Domain.Interfaces;
    using ProductBuilder.Domain.Models;

    public interface ICommandRepository : IAsdRepository<Command> { }
}