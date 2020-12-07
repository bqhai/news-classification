﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_NewsManagementSystem.Models;
using DAL_NewsManagementSystem.DAL;
using Models_NewsManagementSystem.MappingClass;
using Models_NewsManagementSystem.Repository;
using BLL_NewsManagementSystem.Lib;

namespace BLL_NewsManagementSystem.BLL
{
    public class BLL_Post
    {
        private DAL_Post _dalPost = new DAL_Post();
        private EntityMapper<AutoCrawledPost, PostDto> _mapToPostDto = new EntityMapper<AutoCrawledPost, PostDto>();
        private EntityMapper<PostDto, AutoCrawledPost> _mapToAutoCrawledPost = new EntityMapper<PostDto, AutoCrawledPost>();
        public BLL_Post()
        {

        }
        public bool AddNewOrUpdatePost (PostDto postDto)
        {           
            try
            {
                AutoCrawledPost post = _mapToAutoCrawledPost.Translate(postDto);
                if (_dalPost.CheckExistPost(postDto.PostUrl))
                {
                    _dalPost.UpdatePost(post);
                }
                else
                {
                    post.PostID = AutoGenerate.PostID();
                    post.SentimentLabelID = "NEG";
                    _dalPost.AddNewPost(post);
                }            
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
