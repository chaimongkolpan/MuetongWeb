using MuetongWeb.Models.Entities;
using MuetongWeb.Models.Requests;
namespace MuetongWeb.Mappers
{
    public static class RoleMappers
    {
        public static Role UpdateRequestMapper(Role destination, RoleUpdateRequest request)
        {
            destination.HomePageUrl = request.HomePageUrl;
            return destination;
        }
    }
}
