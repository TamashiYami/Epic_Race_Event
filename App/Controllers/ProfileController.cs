using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Data;
using App.Models;
using App.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
	public class ProfileController : Controller
	{
		private readonly IRepository<Profile> _profileRepository;

		public ProfileController(IRepository<Profile> profileRepository)
		{
			_profileRepository = profileRepository;
		}
		// GET: Profile
		public ActionResult Index()
		{
			if (HttpContext.Session.GetInt32("Id") == null)
			{
				return RedirectToAction("Index", "Login");
			}
			var user = _profileRepository.GetById((int)HttpContext.Session.GetInt32("Id"));
			ProfileViewModel profile = new(
				user.FirstName,
				user.LastName,
				user.Email,
				user.BirthDate
			);
			return View("Index", profile);
		}

		// GET: Profile/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: Profile/Create
		public ActionResult Create()
		{
			return View("CreateProfile");
		}

		// POST: Profile/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(CreateProfileRequest profile)
		{
			try
			{
				if (ModelState.IsValid)
				{
					// TODO: Add insert logic here
					// check if the user is more than 16 years old
					if (profile.BirthDate.AddYears(16) > DateTime.Now)
					{
						ModelState.AddModelError("DateOfBirth", "You must be at least 16 years old to register");
						return View("CreateProfile");
					}
					Profile newProfile = new()
					{
						FirstName = profile.FirstName,
						LastName = profile.LastName,
						Email = profile.Email,
						Password = BCrypt.Net.BCrypt.HashPassword(profile.Password),
						BirthDate = profile.BirthDate,
					};
					_profileRepository.Add(newProfile);
					_profileRepository.Commit();

					HttpContext.Session.SetString("Name", newProfile.FirstName);
					HttpContext.Session.SetInt32("Id", newProfile.Id);

					return RedirectToAction(nameof(Index));
				}
				return View("CreateProfile");
			}
			catch
			{
				return View("CreateProfile");
			}
		}

		// GET: Profile/Edit
		public ActionResult Edit()
		{
			return View("EditProfile");
		}

		// POST: Profile/Edit
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(EditProfilRequest request)
		{
			try
			{
				if (ModelState.IsValid)
				{
					Profile profile = _profileRepository.GetById((int)HttpContext.Session.GetInt32("Id"));
					profile.FirstName = request.FirstName;
					profile.LastName = request.LastName;
					_profileRepository.Commit();
					return RedirectToAction("Index");
				}
				return View("EditProfile");
			}
			catch
			{
				return View("Index");
			}
		}
	}
}