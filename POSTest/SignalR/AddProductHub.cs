using Microsoft.AspNetCore.SignalR;
using POSTest.Dto;
using System.Threading.Tasks;

namespace POSTest.SignalR
{
    public class AddProductHub : Hub
    {
        public async Task SendProduct(ProductHub product, int length , int index )
        {
            await Clients.All.SendAsync("showProduct", product, length , index);
        }
        public async Task CancelProduct(int indexofItem)
        {
            await Clients.All.SendAsync("cancelProduct", indexofItem);
        }
    }
}
