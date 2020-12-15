//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL_NewsManagementSystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Post
    {
        public string PostID { get; set; }
        public string PostUrl { get; set; }
        public string UserUrl { get; set; }
        public string PostContent { get; set; }
        public string UploadTime { get; set; }
        public Nullable<int> TotalLikes { get; set; }
        public Nullable<int> TotalComment { get; set; }
        public Nullable<int> TotalShare { get; set; }
        public string FacebookID { get; set; }
        public string NewsLabelID { get; set; }
        public string SentimentLabelID { get; set; }
    
        public virtual NewsLabel NewsLabel { get; set; }
        public virtual WatchList WatchList { get; set; }
        public virtual SentimentLabel SentimentLabel { get; set; }
    }
}
