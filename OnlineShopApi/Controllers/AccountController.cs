﻿using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using OnlineShop.DAL;
using OnlineShop.DAL.Entities;
using OnlineShopApi.Models;
using OnlineShopApi.ViewModels.Identity;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopApi.Controllers
{
	public class AccountController : BaseMvcController
	{
		private ApplicationUserManager UserManager
			=> HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

		private IAuthenticationManager AuthenticationManager
			=> HttpContext.GetOwinContext().Authentication;

		[HttpGet]
		public ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Login(LoginViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				IdentityUser user = UserManager.Find(viewModel.Email, viewModel.Password);
				if (user == null)
				{
					ModelState.AddModelError("", "Неверный логин или пароль.");
				}
				else
				{
					ClaimsIdentity claim = UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
					AuthenticationManager.SignOut();
					AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true }, claim);
					return RedirectToAction("ProductList", "Home");
				}
			}
			return View(viewModel);
		}

		[HttpGet]
		public ActionResult Registration()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Registration(RegistrationViewModel viewModel)
		{
			IdentityUser identityUser = new IdentityUser()
			{
				Email = viewModel.Email,
				UserName = viewModel.Email
			};
			IdentityResult result = UserManager.Create(identityUser, viewModel.Password);
			if (result.Succeeded)
			{
				Mapper.Initialize(cfg => cfg.CreateMap<RegistrationViewModel, User>());
				User user = Mapper.Map(viewModel, typeof(RegistrationViewModel), typeof(User)) as User;
				user.Guid = identityUser.Id;
				user.Role = UnitOfWork.RoleRepo.Get(r => r.Login == "user").FirstOrDefault();
				UnitOfWork.UserRepo.Create(user);
				return RedirectToAction("ProductList", "Home");
			}
			foreach (string error in result.Errors)
			{
				ModelState.AddModelError("", error);
			}
			return View(viewModel);
		}

		public ActionResult Logout()
		{
			AuthenticationManager.SignOut();
			return RedirectToAction("Login");
		}
	}
}