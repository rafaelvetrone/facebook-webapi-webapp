using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FacebookAPIWebApp.BusinessClasses
{

    public interface IFacebookService
    {
        Account GetAccount(string accessToken);
        string PostOnWall(string accessToken, string message);

        string LikePost(string accessToken, string postId);

        List<Post> GetPosts(string accessToken);
    }

    public class FacebookService : IFacebookService
    {
        private readonly IFacebookClient _facebookClient;

        public FacebookService(IFacebookClient facebookClient)
        {
            _facebookClient = facebookClient;
        }

        public Account GetAccount(string accessToken)
        {
            //var result = _facebookClient.Get<dynamic>(accessToken, "me", "fields=id,name,first_name,last_name");
            var result = _facebookClient.Get<dynamic>(accessToken, "me", "fields=id,name,first_name,last_name");

            if (result == null)
            {
                return new Account();
            }

            var account = new Account
            {
                Id = result.id,
                //Email = result.email,
                Name = result.name,
                //UserName = result.username,
                FirstName = result.first_name,
                LastName = result.last_name,
                //Locale = result.locale
            };

            return account;
        }

        public string LikePost(string accessToken, string postId)
        {
            var result = _facebookClient.Get<dynamic>(accessToken, postId + "/likes", "method=POST");
            //var result = _facebookClient.Post(accessToken, postId + "/likes", "", "method=POST");

            //string retorno = JsonConvert.DeserializeObject<string>(result);

            return result;
        }

        public string PostOnWall(string accessToken, string message)
        {
            return _facebookClient.Post(accessToken, "me/feed", new { message });
        }

        public List<Post> GetPosts(string accessToken)
        {
            var result = _facebookClient.Get<dynamic>(
                accessToken, "me", "fields=posts{id,message, description,permalink_url,created_time}");

            if (result == null)
            {
                return new List<Post>();
            }

            var postsJs = result.posts;
            var idJs = result.id;

            var dataJs = postsJs.data;

            List<Post> posts = new List<Post>();

            foreach(var datumJs in dataJs)
            {
                var newPost = new Post
                {
                    Id = datumJs.id,
                    Message = datumJs.message,
                    Description = datumJs.description,
                    PermalinkUrl = datumJs.permalink_url,
                    CreatedTime = datumJs.created_time
                };

                posts.Add(newPost);
            }

            return posts;
        }
    }
}