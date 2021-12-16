using ChampsSpeciaux.Repository.IRepository;
using ChampsSpeciaux.Data;
using ChampsSpeciaux.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using ChampsSpeciaux.Utility;
using System.IO;

namespace ChampsSpeciaux.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger; 
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Travel> TravelList = await _unitOfWork.Travel.GetAllAsync();

            return View(TravelList);
        }

        //GET CREATE
        public IActionResult Create()
        {
            return View();
        }

        //POST CREATE
        [HttpPost]
        public async Task<IActionResult> Create(Travel travel)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _webHostEnvironment.WebRootPath; //Chemin des images de voyage: Travel
                var files = HttpContext.Request.Form.Files; //nouvelle image récupérée

                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();// Nom fichier généré, unique
                    var uploads = Path.Combine(webRootPath, AppConstants.ImagePath);// chemin pour les image
                    var extenstion = Path.GetExtension(files[0].FileName); // extraire l'extention du fichier

                    // Create un cannal pour transférer le fichier 
                    using (var filesStreams = new FileStream(Path.Combine("..\\ChampsSpeciaux\\wwwroot\\images\\", fileName + extenstion), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }

                    // Composer le nom du fichier avec son extension qui sera enregister dans la BD
                    // avec le path relatif à partir du Root
                    // sans le path relatif (le path devra être ajouté dans la View)
                    travel.TravelImage = fileName + extenstion;
                }

                // Ajouter à la BD
                await _unitOfWork.Travel.AddAsync(travel);
                TempData[AppConstants.Success] = "The travel was created.";
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            TempData[AppConstants.Error] = "An error occurred while creating the Travel.";
            return this.View(travel);
        }

        //GET - EDIT
        public async Task<IActionResult> Edit(int? id)
        {
            Travel travel = new Travel();
            travel = await _unitOfWork.Travel.GetAsync(id.GetValueOrDefault());
            if (travel == null)
            {
                return NotFound();
            }
            return View(travel);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Travel travel)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _webHostEnvironment.WebRootPath; //Chemin des images de voyage: Travel
                var files = HttpContext.Request.Form.Files; //nouvelle image récupérée

                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();// Nom fichier généré, unique
                    var uploads = Path.Combine(webRootPath, AppConstants.ImagePath);// chemin pour les image
                    var extenstion = Path.GetExtension(files[0].FileName); // extraire l'extention du fichier


                    if (travel.TravelImage != null)
                    {
                        //L'image est modifiée: l'ancienne doit être supprimée
                        var imagePath = Path.Combine(webRootPath, travel.TravelImage.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                   

                    // Create un canal pour transférer le fichier 
                    using (var filesStreams = new FileStream(Path.Combine("..\\ChampsSpeciaux\\wwwroot\\images\\", fileName + extenstion), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }
                    // Composer le nom du fichier avec son extension qui sera enregister dans la BD
                    // avec le path relatif à partir du Root
                    // sans le path relatif (le path devra être ajouté dans la View)
                    travel.TravelImage = fileName + extenstion;
                }
                else
                {
                    // L'image n'est pas modifiée: on récupère l'information
                    // afin qu'elle ne soit pas perdue après l'update
                    Travel objFromDb = await _unitOfWork.Travel.GetAsync(travel.Id);
                    travel.TravelImage = objFromDb.TravelImage;

                }

                _unitOfWork.Travel.Update(travel);
                TempData[AppConstants.Success] = "The travel was updated.";
                _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData[AppConstants.Error] = "An error occurred while updating the Travel.";
                return View(travel);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
    
