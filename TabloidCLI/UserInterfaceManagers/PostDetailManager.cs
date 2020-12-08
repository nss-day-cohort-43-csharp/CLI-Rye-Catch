using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    internal class PostDetailManager :  IUserInterfaceManager
    {
        private IUserInterfaceManager _parentUI;
        private PostRepository _postRepository;
        private int _postId;

        public PostDetailManager(IUserInterfaceManager parentUI, string connectionString, int postId)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _postId = postId;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("");
            Post post = _postRepository.Get(_postId);
            Console.WriteLine($"{post.Title} Details Menu");
            Console.WriteLine(" 1) View ");
            Console.WriteLine(" 2) Add Tag ");
            Console.WriteLine(" 3) Remove Tag");
            Console.WriteLine(" 4) Note Management [Not Working]");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    View();
                    return this;
                case "2":
                    //AddTag();
                    return this;
                case "3":
                    //RemoveTag();
                    return this;
                case "4":
                    //Notes
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void View()
        {
            Console.Clear();
            Post post = _postRepository.Get(_postId);
            int postId = post.Id;

            List<Tag> name = _postRepository.GetPostTags(postId);
            Console.WriteLine($@"Title: {post.Title}
URL: {post.Url}
Date: {post.PublishDateTime}
Tags");

            foreach (Tag tag in name)
            {
                Console.WriteLine("-----------");
                Console.WriteLine(tag.Name);
                
            };
            Console.WriteLine("-----------");
        }
    }
}
