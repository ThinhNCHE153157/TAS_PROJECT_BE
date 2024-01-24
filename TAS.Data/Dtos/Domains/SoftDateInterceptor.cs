using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using TAS.Data.Entities.Interfaces;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace TAS.Data.Dtos.Domains
{
    public class SoftDateInterceptor : SaveChangesInterceptor
    {
        private readonly IHttpContextAccessor _accessor;

        public SoftDateInterceptor(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        private string GetCurrentUsername()
        {
            const string CreateUserDefault = "TAS_System";
            try
            {
                var httpContext = _accessor?.HttpContext;

                if (httpContext is null)
                {
                    return CreateUserDefault;
                }
                const string nameKey = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
                var currentUser = httpContext.User.Claims
                    .FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name))?.Value;
                return currentUser == null ? CreateUserDefault : currentUser;
            }
            catch
            {
                return CreateUserDefault;
            }

        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            if (eventData.Context is null)
            {
                return result;
            }

            var modified = eventData.Context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);

            foreach (var item in modified)
            {
                var changedOrAddedItem = item.Entity as IDateTracking;
                if (changedOrAddedItem != null)
                {
                    if (item.State == EntityState.Added)
                    {
                        changedOrAddedItem.CreateDate = DateTime.Now;
                        changedOrAddedItem.CreateUser = GetCurrentUsername();
                    }
                    changedOrAddedItem.UpdateDate = DateTime.Now;
                    changedOrAddedItem.UpdateUser = GetCurrentUsername();
                }
            }
            return result;
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            if (eventData.Context is null)
            {
                return base.SavingChangesAsync(eventData, result, cancellationToken);
            }

            var modified = eventData.Context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);

            foreach (var item in modified)
            {
                var changedOrAddedItem = item.Entity as IDateTracking;
                if (changedOrAddedItem != null)
                {
                    if (item.State == EntityState.Added)
                    {
                        changedOrAddedItem.CreateDate = DateTime.Now;
                        changedOrAddedItem.CreateUser = GetCurrentUsername();
                    }
                    changedOrAddedItem.UpdateDate = DateTime.Now;
                    changedOrAddedItem.UpdateUser = GetCurrentUsername();
                }
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}

