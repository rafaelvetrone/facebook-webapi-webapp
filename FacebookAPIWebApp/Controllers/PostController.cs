using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Dynamic;
using FacebookAPIWebApp.BusinessClasses;
using FacebookAPIWebApp.DAOClasses;
using System.Threading.Tasks;
using FacebookAPIWebApp.Models;
using System.Globalization;

namespace FacebookAPIWebApp.Controllers
{
    public class PostController : Controller
    {
        string _appId = "382021202349351";
        string _appSecret = "";
        string _permissions = "user_posts, user_status, manage_pages, pages_show_list, publish_pages, publish_to_groups";
        string _token = "EAAFbcj4QDScBALOs9bpReoLtq7zBn7ZBthiviQvBw8qtMWAZBiZAZAwalH4FLAstKwrZBX1TVVyeIDUELoPUsJ11Il1T30ZAmD0swQcJ8geJZABpy1pYyZCLWl2wZAacWJwzDVXx5flRTa6wKbyjb7mTGPmajuCZCcsfTAbe9nk0cYnkUW86pSnZCpZA4gPQUWxm83gZD";
        string _pageAccessToken = "EAAFbcj4QDScBALOs9bpReoLtq7zBn7ZBthiviQvBw8qtMWAZBiZAZAwalH4FLAstKwrZBX1TVVyeIDUELoPUsJ11Il1T30ZAmD0swQcJ8geJZABpy1pYyZCLWl2wZAacWJwzDVXx5flRTa6wKbyjb7mTGPmajuCZCcsfTAbe9nk0cYnkUW86pSnZCpZA4gPQUWxm83gZD";

        CultureInfo cultInf = new CultureInfo("en-US", false);
        // GET: Home
        public ActionResult Index()
        {
            AccountDAO dao = new AccountDAO();

            List<AccountDO> getAll = dao.Carregar();

            var accessToken = _token;
            var facebookClient = new FacebookClient();
            var facebookService = new FacebookService(facebookClient);
            var account = facebookService.GetAccount(accessToken);

            AccountDO accDO = new AccountDO()
            {
                Id = account.Id,
                Name = account.Name,
                FirstName = account.FirstName,
                LastName = account.LastName
            };

            dao.Salvar(accDO);

            ViewBag.AccountId = account.Id;
            ViewBag.AccountName = account.Name;

            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CarregarFBAPI(string accessToken)
        {
            PostDAO dao = new PostDAO();

            var facebookClient = new FacebookClient();
            var facebookService = new FacebookService(facebookClient);
            var account = facebookService.GetAccount(accessToken);
            var posts = facebookService.GetPosts(accessToken);

            List<PostViewModel> viewModelList = new List<PostViewModel>();

            foreach(var post in posts)
            {
                PostDO postDO = new PostDO()
                {
                    Id = post.Id,
                    Message = post.Message,
                    Description = post.Description,
                    PermalinkUrl = post.PermalinkUrl,
                    CreatedTime = DateTime.Parse(post.CreatedTime, cultInf)
                };

                dao.Salvar(postDO);

                PostViewModel vm = new PostViewModel()
                {
                    Id = postDO.Id,
                    Message = postDO.Message,
                    Description = postDO.Description,
                    PermalinkUrl = postDO.PermalinkUrl,
                    CreatedTime = postDO.CreatedTime.ToString(cultInf)
                };

                viewModelList.Add(vm);
            }

            return View("Posts",viewModelList);
        }

        public ActionResult Search()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SearchPosts(string creationDate)
        {
            PostDAO dao = new PostDAO();

            //DateTime data = new DateTime(1900, 1, 1);
            DateTime data = DateTime.Parse(creationDate);

            List<PostDO> posts = dao.CarregarPorData(data);

            List<PostViewModel> viewModelList = new List<PostViewModel>();

            foreach (var post in posts)
            {
                PostViewModel vm = new PostViewModel()
                {
                    Id = post.Id,
                    Message = post.Message,
                    Description = post.Description,
                    PermalinkUrl = post.PermalinkUrl,
                    CreatedTime = post.CreatedTime.ToString(cultInf)
                };

                viewModelList.Add(vm);
            }            

            return View("Posts", viewModelList);
        }

        [HttpGet]
        public ActionResult Like(string id)
        {
            var accessToken = _pageAccessToken;
            var facebookClient = new FacebookClient();
            var facebookService = new FacebookService(facebookClient);
            var account = facebookService.LikePost(accessToken, id);

            return View("Search");
        }

        [HttpGet]
        public ActionResult InputPost()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InputPost(InputPostViewModel post)
        {
            var facebookClient = new FacebookClient();
            var facebookService = new FacebookService(facebookClient);
            var account = facebookService.PostOnWall(post.AccessToken, post.Message);

            return View(post);
        }
    }
}