using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BBWeb.Models.ViewModels
{
    public class GenaralListViewModel<T>
    {
        public IEnumerable<T> Models { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}