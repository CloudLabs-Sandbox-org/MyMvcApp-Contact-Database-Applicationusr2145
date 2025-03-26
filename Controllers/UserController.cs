// Removed unnecessary using directive
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;

namespace MyMvcApp.Controllers;

public class UserController : Controller
{
    public static System.Collections.Generic.List<User> userlist = new System.Collections.Generic.List<User>();

    // GET: User
    public ActionResult Index(string searchTerm)
{
    // Filtrar la lista de usuarios si se proporciona un término de búsqueda
    var filteredUsers = string.IsNullOrEmpty(searchTerm)
        ? userlist
        : userlist.Where(u => u.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) 
                           || u.Email.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();

    // Pasar el término de búsqueda a la vista para mantenerlo en el campo de búsqueda
    ViewBag.SearchTerm = searchTerm;

    // Retornar la vista con la lista filtrada
    return View(filteredUsers);
}

    // GET: User/Details/5
    public ActionResult Details(int id)
    {
        // Find the user by ID
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    // GET: User/Create
    public ActionResult Create()
    {
        // Return the Create view
        return View();
    }

    // POST: User/Create
    [HttpPost]
    public ActionResult Create(User user)
    {
        if (ModelState.IsValid)
        {
            // Add the new user to the list
            userlist.Add(user);
            return RedirectToAction(nameof(Index));
        }
        return View(user);
    }

    // GET: User/Edit/5
    public ActionResult Edit(int id)
    {
        // Find the user by ID
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    // POST: User/Edit/5
    [HttpPost]
    public ActionResult Edit(int id, User user)
    {
        if (ModelState.IsValid)
        {
            // Find the user by ID and update their details
            var existingUser = userlist.FirstOrDefault(u => u.Id == id);
            if (existingUser == null)
            {
                return NotFound();
            }
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            return RedirectToAction(nameof(Index));
        }
        return View(user);
    }

    // GET: User/Delete/5
    public ActionResult Delete(int id)
    {
        // Find the user by ID
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    // POST: User/Delete/5
    [HttpPost]
    public ActionResult Delete(int id, IFormCollection collection)
    {
        // Find the user by ID and remove them from the list
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        userlist.Remove(user);
        return RedirectToAction(nameof(Index));
    }
}
