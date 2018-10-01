using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Play2Money.Models;
using Play2Money.Data;
using Play2Money.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Play2Money.Controllers
{
	public class OrderController : Controller
	{
        //private readonly ApplicationDbContext context;
        private readonly IPlayer2MoneyService service;
        public OrderController(ApplicationDbContext dbContext, IPlayer2MoneyService appService)
        {
            //context = dbContext;
            service = appService;
        }

        [HttpGet]
        public IActionResult Index([FromQuery,BindRequired] RequestViewModel req)
        {
            if (!ModelState.IsValid || !req.IsValid)
                return BadRequest(ModelState);

            var player = service.ProcessRequest(req);
            
            return View(new PlayerViewModel(player));
        }

        [HttpPost("Order/Index")]
        public IActionResult Index([BindRequired]PlayerViewModel playerVM)
        {
            var player = service.UpdatePlayer(playerVM);
            return View(new PlayerViewModel(player));
        }

	    [HttpGet("Order/Test")]
	    public IActionResult Test()
	    {
            return View(new TestRequestViewModel());
	    }


        [HttpPost("Order/Test")]
	    public IActionResult Test(TestRequestViewModel model)
	    {
            model.ComputeHash();
            ViewBag.Href = $"Index?id={model.GlobalUid}&p={model.P}&g={model.G}&h={model.H}";
            return View(model);
        }

        
    }
}