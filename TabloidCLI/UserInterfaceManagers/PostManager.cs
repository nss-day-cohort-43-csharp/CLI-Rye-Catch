using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    public class PostManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private PostRepository _postRepository;
        private AuthorRepository _authorRepository;
        private BlogRepository _BlogRepository;
        private string _connectionString;

        public PostManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _authorRepository = new AuthorRepository(connectionString);
            _BlogRepository = new BlogRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("");
            Console.WriteLine("Post Menu");
            Console.WriteLine(" 1) List Posts");
            Console.WriteLine(" 2) Post Details");
            Console.WriteLine(" 3) Add Post");
            Console.WriteLine(" 4) Edit Post");
            Console.WriteLine(" 5) Remove Post");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "2":
                    Post post = Choose();
                    if (post == null) return this;                  
                    else return new PostDetailManager(this, _connectionString, post.Id);                   
                case "3":
                    Add();
                    return this;
                case "4":
                    Edit();
                    return this;
                case "5":
                    Remove();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        
        

        /// <summary>
        /// Everything below this point needs work. Somethings will need some work in the post repo, Delete and Edit most likely.
        /// </summary>
         

        private void List()
        {
            Console.Clear();
            Console.WriteLine("-----Posts-----");
            List<Post> posts = _postRepository.GetAll();
            foreach (Post post in posts)
            {
                Console.WriteLine($"Title: {post.Title}");
                Console.WriteLine($"Link: '{post.Url}'");
                Console.WriteLine("---------------");
            }
        }

        private Post Choose(string prompt = null)
        {
            Console.Clear();
            if (prompt == null) prompt = "Please choose a Post:";
            Console.WriteLine(prompt);
            Console.WriteLine("---------------");

            List<Post> posts = _postRepository.GetAll();

            for (int i = 0; i < posts.Count; i++)
            {
                Post post = posts[i];
                Console.WriteLine($" {i + 1}) {post.Title}");
                Console.WriteLine("---------------");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return posts[choice - 1];
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private void Add()
        {
            Console.Clear();
            Console.WriteLine("New Post");
            Post post = new Post();

            Console.Write("Post Title: ");
            post.Title = Console.ReadLine();

            Console.Write("Post URL: ");
            post.Url = Console.ReadLine();

            post.PublishDateTime = DateTime.Now;
            //This block is for the Author list that the user chooses from.
            Console.Clear();
            Console.WriteLine("List of Authors\n---------------");
            List<Author> authors = _authorRepository.GetAll();
            foreach (Author author in authors) Console.WriteLine($"{author.Id} - {author.FullName}");
            Console.WriteLine("---------------");
            Console.WriteLine("Enter the Author's ID");
            post.Author = new Author();
            post.Author.Id = int.Parse(Console.ReadLine());
            //this block is for listing the blogs that we can choose
            Console.Clear();
            Console.WriteLine("List of Blogs\n---------------");
            List<Blog> blogs = _BlogRepository.GetAll();
            foreach (Blog blog in blogs)
            {
                Console.WriteLine($"{blog.Id} - {blog.Title}");
            }
            Console.WriteLine("---------------");
            Console.WriteLine("Enter the Blog's ID");
            post.Blog = new Blog();
            post.Blog.Id = int.Parse(Console.ReadLine());
            Console.Clear();

            _postRepository.Insert(post);
        }

        private void Edit()
        {
            Console.Clear();
            Post postToEdit = Choose("Which post would you like to edit?");
            if (postToEdit == null) return;

            Console.WriteLine();
            Console.Write("New Title (blank to leave unchanged): ");
            string title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
            {
                postToEdit.Title = title;
            }
            Console.Write("New URL (blank to leave unchanged): ");
            string url = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(url))
            {
                postToEdit.Url = url;
            }
            // this is where things get tricky - This ia prints out a list of authors with their Ids, the user will then pick an Id & we make a new Author Object to hold that Id.
            Console.Clear();
            Console.Write("New Author (blank to leave unchanged): ");
            Console.WriteLine("List of Authors\n---------------");
            List<Author> authors = _authorRepository.GetAll();
            foreach (Author author in authors) Console.WriteLine($"{author.Id} - {author.FullName}");
            Console.WriteLine("Enter the Author's ID");
            string authorId = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(authorId))
            {
                postToEdit.Author = new Author();
                postToEdit.Author.Id = int.Parse(authorId);
            }
            //this will be the block to make sure that we create a new Blog object if we need it.
            Console.Clear();
            Console.Write("New Blog (blank to leave unchanged): ");
            Console.WriteLine("List of Blogs\n---------------");
            List<Blog> blogs = _BlogRepository.GetAll();
            foreach (Blog blog in blogs) Console.WriteLine($"{blog.Id} - {blog.Title}");
            Console.WriteLine("Enter the Blogs's ID");
            string blogId = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(authorId))
            {
                postToEdit.Blog = new Blog();
                postToEdit.Blog.Id = int.Parse(blogId);
            }

            _postRepository.Update(postToEdit);
        }

        private void Remove()
        {
            Console.Clear();
            Post postToDelete = Choose("Which post would you like to remove?");
            if (postToDelete != null)
            {

                _postRepository.Delete(postToDelete.Id);

            }
        }
    }
}
