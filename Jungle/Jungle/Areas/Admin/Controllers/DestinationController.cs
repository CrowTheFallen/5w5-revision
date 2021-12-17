using Jungle_DataAccess.Repository.IRepository;
using Jungle_Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jungle.Areas.Admin.Controllers
{
  [Area("Admin")]
  public class DestinationController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;

    public DestinationController(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public async Task<IActionResult> Index()
    {
      IEnumerable<Destination> DestinationList = await _unitOfWork.Destination.GetAllAsync();
      
      return View(DestinationList);
    }

    public async Task<IActionResult> Upsert(int? id)
    {
      Destination destination = new Destination();
      if (id == null)
      {
        // Create
        return View(destination);
      }
      // Update
      destination = await _unitOfWork.Destination.GetAsync(id.GetValueOrDefault());
      if (destination == null)
      {
        return NotFound();
      }
      return View(destination);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upsert(Destination destination)
    {
      if (ModelState.IsValid)
      {
        if (destination.Id == 0)
        {
        await _unitOfWork.Destination.AddAsync(destination);

        }
        else
        {
         await _unitOfWork.Destination.UpdateAsync(destination);
        }
       await _unitOfWork.SaveAsync();
        return RedirectToAction(nameof(Index));
      }
      return View(destination);
    }

  }
}
