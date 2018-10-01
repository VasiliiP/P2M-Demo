using Microsoft.AspNetCore.Mvc;
using Play2Money.Data;
using Play2Money.Models;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Play2Money.Controllers
{
    //[Authorize]
    public class AdminController : Controller
	{
        private readonly ApplicationDbContext context;
        public AdminController(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }

        [HttpGet("Admin/Index")]
        public IActionResult Index()
        {   
            var orderListVM = new OrderListViewModel();
            return View(orderListVM);
        }

        [HttpPost("Admin/Index")]
        public IActionResult Index(OrderListViewModel orderListVM)
        {
            
            return View(orderListVM);
        }

	    [HttpPost("Admin/Update")]
	    public IActionResult Update(int id)
	    {
	        var order = context.Orders.FirstOrDefault(x => x.Id == id);
	        if (order is null)
	            return NotFound();

	        var orderState = order.IsDone;

	        order.IsDone = !orderState;
	        context.Update(order);
	        context.SaveChanges();
	        return Json(new { data = "true" });
	    }

	    [HttpPost("Admin/Delete")]
	    public IActionResult Delete(int id)
	    {
	        var order = context.Orders.FirstOrDefault(x => x.Id == id);
	        if (order is null)
	            return NotFound();

	        context.Remove(order);
	        context.SaveChanges();
	        return Json(new { data = "true" });
	    }

        [HttpPost("Admin/LoadData")]
        public IActionResult LoadData(bool showCompleted = false)  
        {  
           try  
           {  
               var draw = HttpContext.Request.Form["draw"].FirstOrDefault();  
  
               // Skip number of Rows count  
               var start = Request.Form["start"].FirstOrDefault();  
  
               // Paging Length 10,20  
               var length = Request.Form["length"].FirstOrDefault();  
  
               // Sort Column Name  
               var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][data]"].FirstOrDefault();  
  
               // Sort Column Direction (asc, desc)  
               var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();  
  
               // Search Value from (Search box)  
               var searchValue = Request.Form["search[value]"].FirstOrDefault();  
  
               //Paging Size (10, 20, 50,100)  
               int pageSize = length != null ? Convert.ToInt32(length) : 0;  
  
               int skip = start != null ? Convert.ToInt32(start) : 0;  
  
               int recordsTotal = 0;
  
               var orderList = context.GetOrderTableList().Where(o => o.IsDone == false || o.IsDone == showCompleted);

               if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))  
               {  
                   orderList = orderList.OrderBy(sortColumn + " " + sortColumnDirection);  
               }  

               if (!string.IsNullOrEmpty(searchValue))  
               {  
                   orderList = orderList.Where(o => o.Reference.Contains(searchValue));  
               }  
  
               recordsTotal = orderList.Count();  

               var data = orderList.Skip(skip).Take(pageSize).ToList();  

               return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });  
  
           }  
           catch (Exception)  
           {  
               //TODO: ex
               throw;  
           }  
  
       }     
        
    }
}
