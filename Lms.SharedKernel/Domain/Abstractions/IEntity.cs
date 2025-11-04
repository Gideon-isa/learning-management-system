namespace Lms.SharedKernel.Domain.Abstractions
{
    public interface IEntity<TKey>      
    {
        TKey Id { get; }
    }
}
