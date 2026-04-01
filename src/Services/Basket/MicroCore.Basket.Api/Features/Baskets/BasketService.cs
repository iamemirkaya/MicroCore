using MicroCore.Basket.Api.Const;
using MicroCore.Shared.Services;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace MicroCore.Basket.Api.Features.Baskets;
public class BasketService
{
    private readonly IDistributedCache _distributedCache;
    private readonly IIdentityService _identityService;


    public BasketService(IDistributedCache distributedCache, IIdentityService identityService)
    {
        _distributedCache = distributedCache;
        _identityService = identityService;
    }

    private string GetCacheKey()
    {
        return string.Format(BasketConst.BasketCacheKey, _identityService.UserId); 
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