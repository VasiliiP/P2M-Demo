using System.Collections.Generic;
using System.Threading.Tasks;
using Play2Money.Models;

namespace Play2Money.Services
{
    public interface IPlayer2MoneyService
    {
        Player ProcessRequest(RequestViewModel requestVM);
        Player UpdatePlayer(PlayerViewModel playerVM);
        List<OrderViewModel> GetAllOrders(bool withCompleted = true);
    
    }
}