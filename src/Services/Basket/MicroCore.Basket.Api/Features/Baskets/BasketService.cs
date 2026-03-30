using MicroCore.Basket.Api.Const;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace MicroCore.Basket.Api.Features.Baskets;
public class BasketService
{
    private readonly IDistributedCache _distributedCache;


    public BasketService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    private string GetCacheKey()
    {
        var tempUserId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6");
        return string.Format(BasketConst.BasketCacheKey, tempUserId ); //_identityService.UserId
    }

    private string GetCacheKey(Guid userId)
    {
        return string.Format(BasketConst.BasketCacheKey, userId);
    }

    public Task<string?> GetBasketFromCache(CancellationToken cancellationToken)
    {
        return _distributedCache.GetStringAsync(GetCacheKey(), cancellationToken);
    }

    public async Task CreateBasketCacheAsync(Models.Basket basket, CancellationToken cancellationToken)
    {

        var basketAsString = JsonSerializer.Serialize(basket);

        await _distributedCache.SetStringAsync(GetCacheKey(), basketAsString, cancellationToken);
    }

    public async Task DeleteBasket(Guid userId)
    {
        await _distributedCache.RemoveAsync(GetCacheKey(userId));
    }
}