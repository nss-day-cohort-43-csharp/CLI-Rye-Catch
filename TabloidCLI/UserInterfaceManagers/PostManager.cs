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
        private string _connectionString;

        public PostManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _authorRepository = new AuthorRepository(connectionString);
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
                    Author author = Choose();
                    if (author == null) return this;                  
                    else return new AuthorDetailManager(this, _connectionString, author.Id);                   
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
            Console.WriteLine("List of Authors\n---------------");
            List<Author> authors = _authorRepository.GetAll();
            foreach (Author author in authors) Console.WriteLine(author);    
        }

        private Author Choose(string prompt = null)
        {
            Console.Clear();
            if (prompt == null) prompt = "Please choose an Author:"; 
            Console.WriteLine(prompt);

            List<Author> authors = _authorRepository.GetAll();

            for (int i = 0; i < authors.Count; i++)
            {
                Author author = authors[i];
                Console.WriteLine($" {i + 1}) {author.FullName}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return authors[choice - 1];
            }
            catch (Exception ex)
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

            Console.WriteLine("List of Authors\n---------------");
            List<Author> authors = _authorRepository.GetAll();
            foreach (Author author in authors) Console.WriteLine($"{author.Id} - {author.FullName}");



            _authorRepository.Insert(author);
        }

        private void Edit()
        {
            Console.Clear();
            Author authorToEdit = Choose("Which author would you like to edit?");
            if (authorToEdit == null) return;           

            Console.WriteLine();
            Console.Write("New first name (blank to leave unchanged): ");
            string firstName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(firstName))
            {
                authorToEdit.FirstName = firstName;
            }
            Console.Write("New last name (blank to leave unchanged): ");
            string lastName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(lastName))
            {
                authorToEdit.LastName = lastName;
            }
            Console.Write("New bio (blank to leave unchanged): ");
            string bio = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(bio))
            {
                authorToEdit.Bio = bio;
            }

            _authorRepository.Update(authorToEdit);
        }

        private void Remove()
        {
            Console.Clear();
            Author authorToDelete = Choose("Which author would you like to remove?");
            if (authorToDelete != null)
            {
                _authorRepository.Delete(authorToDelete.Id);
            }
        }
    }
}
