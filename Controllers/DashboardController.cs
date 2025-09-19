using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PropertyManagementApp.Models;
using PropertyManagementApp.Data;
using Microsoft.Extensions.Logging;
using System.Linq;

public class DashboardController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<DashboardController> _logger;

    public DashboardController(AppDbContext context, ILogger<DashboardController> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    public IActionResult Index()
    {
        _logger.LogInformation("Fetching all properties from database.");

        var properties = _context.Properties.ToList();
        ViewBag.Properties = properties;

        var userRole = HttpContext.Session.GetString("UserRole");
        ViewBag.UserRole = userRole;

        _logger.LogInformation("User role from session: {UserRole}", userRole);
        return View();
    }

    [HttpPost]
    public IActionResult AddProperty(Properties property)
    {
        if (property == null)
        {
            _logger.LogWarning("Attempted to add a null property.");
            return BadRequest();
        }

        _context.Properties.Add(property);
        _context.SaveChanges();
        _logger.LogInformation("Added new property: {PropertyName}, Location: {Location}, Price: {Price}", 
            property.Name, property.Location, property.Price);
        TempData["SuccessMessage"] = "Property added successfully!";
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult DeleteProperty(int id)
    {
        var prop = _context.Properties.FirstOrDefault(p => p.Id == id);
        if (prop == null)
        {
            _logger.LogWarning("Attempted to delete property with Id {PropertyId} but it was not found.", id);
            return NotFound();
        }

        _context.Properties.Remove(prop);
        _context.SaveChanges();
        _logger.LogInformation("Deleted property: {PropertyName}, Id: {PropertyId}", prop.Name, prop.Id);
        TempData["SuccessMessage"] = "Property deleted successfully!";
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult EditProperty(Properties property)
    {
        if (property == null)
        {
            _logger.LogWarning("Attempted to edit a null property.");
            return BadRequest();
        }

        var prop = _context.Properties.FirstOrDefault(p => p.Id == property.Id);
        if (prop == null)
        {
            _logger.LogWarning("Attempted to edit property with Id {PropertyId} but it was not found.", property.Id);
            return NotFound();
        }

        // Update values
        prop.Name = property.Name ?? prop.Name;
        prop.Location = property.Location ?? prop.Location;
        prop.Price = property.Price;

        _context.SaveChanges();
        TempData["SuccessMessage"] = "Property updated successfully!";
        _logger.LogInformation("Edited property: {PropertyName}, Id: {PropertyId}, New Location: {Location}, New Price: {Price}", 
            prop.Name, prop.Id, prop.Location, prop.Price);

        return RedirectToAction("Index");
    }
}
