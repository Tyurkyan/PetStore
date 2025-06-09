using PetStore.Models.DTO;

namespace PetStore.DL.Cache
{
    public interface ICacheRepository<TKey, TData> where TData : ICacheItem<TKey>
    {
        Task<IEnumerable<TData?>> FullLoad();

        Task<IEnumerable<TData?>> DifLoad(DateTime lastExecuted);
    }
}
