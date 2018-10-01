using System;
using System.Collections.Generic;
using System.Linq;
using Play2Money.Data;
using Play2Money.Models;
using Microsoft.EntityFrameworkCore;

namespace Play2Money.Services
{    
    public class Player2MoneyService : IPlayer2MoneyService
    {
        private readonly ApplicationDbContext context;
        private readonly IEmailService _emailService;

        public Player2MoneyService(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }
        public Player ProcessRequest(RequestViewModel requestVM)
        {
            bool IsNew = false;
            var player = context.Players.Include(x => x.Orders).SingleOrDefault(p => p.GlobalUid == requestVM.Id);
            
            if (player is null)
            {
                player = new Player(requestVM);
                IsNew = true;
            }

            int orderCount = (requestVM.P/100) - player.Orders.Count;

            for (int i = 0; i < orderCount; i++)
            {
                player.Orders.Add (new Order ());

            }

            if (player.Points < requestVM.P)
            {
                player.Points = requestVM.P;
            }

            var ct = IsNew ? context.Add(player) : context.Update(player);
            context.SaveChanges();
            
            return player;
        }

        private void SendEmail()
        {
            var message = new EmailMessage();
            _emailService.Send(message);
        }

        public Player UpdatePlayer(PlayerViewModel playerVM)
        {
            var player = context.Players.Include(x => x.Orders).FirstOrDefault(p => p.GlobalUid == playerVM.GlobalUid);
            
            if (player is null)
                return null;

            player.Points = playerVM.Points;
            player.Orders.ForEach(o => o.Reference = playerVM.Orders.FirstOrDefault(x => x.Id == o.Id)?.Reference);

            context.Update(player);
            context.SaveChanges();
            return player;
        }

        public List<OrderViewModel> GetAllOrders(bool withCompleted = true)
        {
            if (!context.Orders.Any())
                return null;

            return context.Orders.Include(o => o.Player)
                    .ToList()
                    .Select(o => new OrderViewModel(o))
                    .OrderBy(x => x.Player.GlobalUid)
                    .ToList();
        }

    }
}