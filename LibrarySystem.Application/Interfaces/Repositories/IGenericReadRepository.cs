namespace LibrarySystem.Application.Interfaces.Repositories;

public interface IGenericReadRepository<T, U> where T : class
{
    Task<T?> GetById(U id);
    Task<List<T>> GetAll();
}
public interface IGenericReadWithParamRepository<T, P> where T : class
{
    Task<T> GetAll(P param);
}
public interface IGenericReadByIdRepository<T, U> where T : class?
{
    Task<T> GetById(U id);
}
