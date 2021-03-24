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
  public class UsersController : Controller
  {
    private readonly RecipeContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    public UsersController(UserManager<ApplicationUser> userManager, RecipeContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<IActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var user = _db.Users.Where(entry => entry.AppUser.Id == currentUser.Id).ToList();
      return View(user);
    }

    public ActionResult Create()
    {
      ViewBag.FoodId = new SelectList(_db.Food, "FoodId", "FoodName");
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(User user, int foodId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      user.AppUser = currentUser;
      _db.Users.Add(user);
      _db.SaveChanges();
      if (foodId != 0)
      {
          _db.FoodUsers.Add(new FoodUser() { FoodId = foodId, UserId = user.UserId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      
      var thisUser = _db.Users
          .Include(user => user.JoinEntities)
          .ThenInclude(join => join.Food)
          .FirstOrDefault(user => user.UserId == id);
      return View(thisUser);
    }

    public ActionResult Edit(int id)
    {
      var thisUser = _db.Users.FirstOrDefault(user => user.UserId == id);
      ViewBag.FoodId = new SelectList(_db.Food, "FoodId", "Name");
      return View(thisUser);
    }

    [HttpPost]
    public ActionResult Edit(User user, int foodId)
    {
      if (foodId != 0)
      {
        _db.FoodUsers.Add(new FoodUser() { FoodId = foodId, UserId = user.UserId });
      }
      _db.Entry(user).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddFood(int id)
    {
      var thisUser = _db.Users.FirstOrDefault(user => user.UserId == id);
      ViewBag.FoodId = new SelectList(_db.Food, "FoodId", "Name");
      return View(thisUser);
    }

    [HttpPost]
    public ActionResult AddCategory(User user, int foodId)
    {
      if (foodId != 0)
      {
      _db.FoodUsers.Add(new FoodUser() { FoodId = foodId, UserId = user.UserId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisUser = _db.Users.FirstOrDefault(user => user.UserId == id);
      return View(thisUser);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisUser = _db.Users.FirstOrDefault(user => user.UserId == id);
      _db.Users.Remove(thisUser);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteFood(int joinId)
    {
      var joinEntry = _db.FoodUsers.FirstOrDefault(entry => entry.FoodUserId == joinId);
      _db.FoodUsers.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}