using ApplicationCore.Entities;
using ApplicationCore.Exceptions;

namespace Ardalis.GuardClauses
{
    public static class BasketGuards
    {
        public static void NullBasket(this IGuardClause guardClause, int basketId, IBaseEntity entity)
        {
            if (entity == null)
                throw new ItemNotFoundException(basketId);
        }
    }
}