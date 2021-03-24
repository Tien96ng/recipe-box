using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Recipe.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Recipe.Controllers
{

  [Authorize]
  public class FoodController : Controller 
  { 
    private readonly RecipeContext _db;
     public FoodController(RecipeContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Food.ToList());
    }

    public ActionResult Create()
    {
      ViewBag.UserId = new SelectList(_db.Users, "UserId", "Name");
      return View();
    }
   [HttpPost]
    public ActionResult Create(Food food, int UserId)
    {
      _db.Food.Add(food);
      _db.SaveChanges();
      if (UserId != 0)
      {
        _db.FoodUsers.Add(new FoodUser() { UserId = UserId, FoodId = food.FoodId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisFood = _db.Food
          .Include(food => food.JoinEntities)
          .ThenInclude(join => join.User)
          .FirstOrDefault(food => food.FoodId == id);
      return View(thisFood);
    }

    public ActionResult Edit(int id)
    {
      var thisFood = _db.Food.FirstOrDefault(food => food.FoodId == id);
      ViewBag.UserId = new SelectList(_db.Users, "UserId", "Name");
      return View(thisFood);
    }

    [HttpPost]
    public ActionResult Edit(Food food, int UserId)
    {
      if (UserId != 0)
      {
        _db.FoodUsers.Add(new FoodUser() { UserId = UserId, FoodId = food.FoodId });
      }
      _db.Entry(food).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddUser(int id)
    {
      var thisFood = _db.Food.FirstOrDefault(food => food.FoodId == id);
      ViewBag.UserId = new SelectList(_db.Users, "UserId", "Name");
      return View(thisFood);
    }

    [HttpPost]
    public ActionResult AddUser(Food food, int UserId)
    {
      if (UserId != 0)
      {
      _db.FoodUsers.Add(new FoodUser() { UserId = UserId, FoodId = food.FoodId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisFood = _db.Food.FirstOrDefault(food => food.FoodId == id);
      return View(thisFood);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisFood = _db.Food.FirstOrDefault(food => food.FoodId == id);
      _db.Food.Remove(thisFood);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteUser(int joinId)
    {
      var joinEntry = _db.FoodUsers.FirstOrDefault(entry => entry.FoodUserId == joinId);
      _db.FoodUsers.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}