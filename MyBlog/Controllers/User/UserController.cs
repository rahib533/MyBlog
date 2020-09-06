using MyBlog.App_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MyBlog.Controllers.User
{
    [AllowAnonymous]
    public class UserController : Controller
    {
        // GET: User
        [AllowAnonymous]
        public ActionResult Index(int id = 7)
        {
            return View(id);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult UserLogin(UserInfo ui)
        {
            bool sonuc = Membership.ValidateUser(ui.Adi, ui.Parol);
            if (sonuc)
            {
                FormsAuthentication.RedirectFromLoginPage(ui.Adi, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index","User",new { id = 404 });
            }
        }



        public ActionResult UserLogout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Qeydiyyat()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Qeydiyyat(UserInfo ui)
        {
            MembershipCreateStatus veziyyet;
            string msg = "";
            Membership.CreateUser(ui.Adi, ui.Parol, ui.Email, ui.GizliSual, ui.GizliCavab, true, out veziyyet);
            switch (veziyyet)
            {
                case MembershipCreateStatus.Success:
                    break;
                case MembershipCreateStatus.InvalidUserName:
                    msg += " Uygun olmayan istifadeci adi";
                    break;
                case MembershipCreateStatus.InvalidPassword:
                    msg += " Uygun olmayan Shifre";
                    break;
                case MembershipCreateStatus.InvalidQuestion:
                    msg += " Uygun olmayan Gizli sual";
                    break;
                case MembershipCreateStatus.InvalidAnswer:
                    msg += " Uygun olmayan Gizli cavab";
                    break;
                case MembershipCreateStatus.InvalidEmail:
                    msg += " Uygun olmayan email";
                    break;
                case MembershipCreateStatus.DuplicateUserName:
                    msg += " Istifadeci adi tekrardir";
                    break;
                case MembershipCreateStatus.DuplicateEmail:
                    msg += " email tekrardir";
                    break;
                case MembershipCreateStatus.UserRejected:
                    msg += " Istifadeci blok xetasi";
                    break;
                case MembershipCreateStatus.InvalidProviderUserKey:
                    msg += " Uygun olmayan Istifadeci key xetasi";
                    break;
                case MembershipCreateStatus.DuplicateProviderUserKey:
                    msg += " Istifadeci keyi tekrardir";
                    break;
                case MembershipCreateStatus.ProviderError:
                    msg += " Provider Error";
                    break;
                default:
                    break;
            }

            ViewBag.Mesaj = msg;
            if (veziyyet == MembershipCreateStatus.Success)
            {
                return RedirectToAction("Index","Home");
            }
            else
            {
                return RedirectToAction("Qeydiyyat");
            }
        }


    }
}