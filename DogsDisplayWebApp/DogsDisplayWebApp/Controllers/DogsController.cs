using DogsDisplayWebApp.Abstractions;
using DogsDisplayWebApp.Data;
using DogsDisplayWebApp.Domain;
using DogsDisplayWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogsDisplayWebApp.Controllers
{
    public class DogsController : Controller
    {
        private readonly IDogService _dogService;

        public DogsController(IDogService dogsService)
        {
            this._dogService = dogsService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(DogCreateViewModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                var created = _dogService.Create(bindingModel.Name, bindingModel.Age, bindingModel.Breed, bindingModel.Picture);
                if (created)
                {
                    return this.RedirectToAction("Success");
                }
            }
            return this.View();
        }

        public IActionResult Success()
        { return this.View(); }

        public IActionResult All(string searchStringBreed, string searchStringName)
        {
            List<DogAllViewModel> dogs = _dogService.GetDogs(searchStringBreed, searchStringName)
                 .Select(dogFromDb => new DogAllViewModel
                 {
                   Id = dogFromDb.Id,
                   Name= dogFromDb.Name,
                   Age = dogFromDb.Age,
                   Breed = dogFromDb.Breed,
                   Picture = dogFromDb.Picture
                 }).ToList();
            return this.View(dogs);
        }

        public IActionResult Edit(int id)
        {

            Dog item = _dogService.GetDogById(id);
            if(item==null)
            {
                return NotFound();
            }
            DogCreateViewModel dog = new DogCreateViewModel()
            {
                Id = item.Id,
                Name = item.Breed,
                Age = item.Age,
                Breed = item.Breed,
                Picture = item.Picture
            };

            return View(dog);
        }

        [HttpPost]

        public IActionResult Edit(int id ,DogCreateViewModel bindingModel)
        {
            if(ModelState.IsValid)
            {
                var updated = _dogService.UpdateDog(id, bindingModel.Name, bindingModel.Age, bindingModel.Breed, bindingModel.Picture);
                if(updated)
                {
                    return this.RedirectToAction("All");
                }
            }
            return View(bindingModel);
        }

        public IActionResult Delete(int id)
        {
            Dog item = _dogService.GetDogById(id);
            if(item==null)
            { return NotFound(); }
            DogCreateViewModel dog = new DogCreateViewModel()
            {
                Id = item.Id,
                Name = item.Breed,
                Age = item.Age,
                Breed = item.Breed,
                Picture = item.Picture
            };
            return View(dog);
        }

        [HttpPost]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            var deleted = _dogService.RemoveById(id);
            if(deleted)
            {
                return this.RedirectToAction("All", "Dogs");
            }
            else
            { return View(); }
        }

        public IActionResult Details(int id)
        {
            Dog item = _dogService.GetDogById(id);
            if(item==null)
            { return NotFound(); }    

            DogDetailsViewModel dog = new DogDetailsViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                Age = item.Age,
                Breed = item.Breed,
                Picture = item.Picture
            };
            return View(dog);
        }
        public IActionResult Sort()
        {
            List<DogAllViewModel> dogs = _dogService.GetDogs()
                 .Select(dogFromDb => new DogAllViewModel
                 {
                     Id = dogFromDb.Id,
                     Name = dogFromDb.Name,
                     Age = dogFromDb.Age,
                     Breed = dogFromDb.Breed,
                     Picture = dogFromDb.Picture
                 }).OrderBy(x=>x.Name)
                 .ToList();
            return this.View(dogs);

        }
    }
}

