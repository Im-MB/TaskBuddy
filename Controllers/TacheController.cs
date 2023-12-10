﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;
using TaskBuddy.Data;
using TaskBuddy.Models;

namespace TaskBuddy.Controllers
{
    [Authorize]
    public class TacheController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public TacheController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

       
        [HttpGet]
        public IActionResult ListeTaches()
        {
            var listeTaches = _dbContext.Tasks.ToList();
            return View(listeTaches);
        }
        [HttpGet]
        public IActionResult CreateTache() {
            return View();
                
         }
        [HttpPost]
        public IActionResult CreateTache(Tache tache)
        {
            _dbContext.Tasks.Add(tache);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(ListeTaches)) ;
        }
        [HttpGet]
        public IActionResult Details(int id) {
        
        var tache=_dbContext.Tasks.SingleOrDefault (t => t.IdTask == id);
            return View(tache);
        
        }
        [HttpGet]
        public IActionResult DeleteTache(int id)
        {
            var tache=_dbContext.Tasks.SingleOrDefault(t => t.IdTask == id);
            return View(tache);
        }
        [HttpPost]
        public IActionResult DeleteTache(Tache tache)
        {
            _dbContext.Tasks.Remove(tache);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(ListeTaches));
        }
       
        [HttpGet]
        public IActionResult EditTache(int id)
        {
            var tache= _dbContext.Tasks.Find(id);
            return View(tache);
        }
        [HttpPost]  
        public IActionResult EditTache(Tache newTache)
        {
            var oldTache= _dbContext.Tasks.Find(newTache.IdTask);
            if(oldTache != null) { 
            
            oldTache.Nom=newTache.Nom;
            oldTache.Etat = newTache.Etat;
            oldTache.DateD = newTache.DateD;
            oldTache.DateF=newTache.DateF;
            oldTache.Note = newTache.Note;
             _dbContext.SaveChanges();
            }
            return RedirectToAction(nameof(ListeTaches));
        }    

    } 
}
