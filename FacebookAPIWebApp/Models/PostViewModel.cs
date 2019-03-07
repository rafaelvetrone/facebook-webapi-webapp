using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FacebookAPIWebApp.Models
{
    public class PostViewModel
    {
        public string Id { get; set; }
        public string Message { get; set; }
        public string Description { get; set; }
        public string PermalinkUrl { get; set; }
        public string CreatedTime { get; set; }
    }
}