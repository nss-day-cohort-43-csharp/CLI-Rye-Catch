﻿// Finished by Sam Edwards 
using System;
using System.Collections.Generic;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    public class TagManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private TagRepository _tagRepository;
        private string _connectionString;

        public TagManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _tagRepository = new TagRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("");
            Console.WriteLine("Tag Menu");
            Console.WriteLine(" 1) List Tags");
            Console.WriteLine(" 2) Add Tag");
            Console.WriteLine(" 3) Edit Tag");
            Console.WriteLine(" 4) Remove Tag");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "2":
                    Add();
                    return this;
                case "3":
                    Edit();
                    return this;
                case "4":
                    Remove();
                    return this;
                case "0":
                    Console.Clear();
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void List()
        {
            Console.Clear();
            Console.WriteLine("List of Tags\n------------");

            List<Tag> tags = _tagRepository.GetAll();
            tags.ForEach(t => Console.WriteLine(t.Name));
        }

        private Tag Choose(string prompt = null)
        {
            if (prompt == null) prompt = "Please choose a Tag:";
            Console.WriteLine(prompt);

            List<Tag> tags = _tagRepository.GetAll();

            for (int i = 0; i < tags.Count; i++)
            {
                Tag tag = tags[i];
                Console.WriteLine($" {i + 1}) {tag.Name}");
            }
            Console.Write("> ");
            
            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return tags[choice - 1];
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

            Console.WriteLine("New Tag\n-------");
            Tag tag = new Tag();
            string name = "";

            while (tag.Name == null)
            {
                Console.Write("Tag Name: ");
                name = Console.ReadLine().ToLower();
                if (!String.IsNullOrWhiteSpace(name)) tag.Name = name;  
            }
            _tagRepository.Insert(tag);
            Console.WriteLine($"{name} added to database.");
        }

        private void Edit()
        {
            Tag tagToEdit = Choose("Which tag would you like to edit?");
            if (tagToEdit == null) return;
            Console.WriteLine($"Original: {tagToEdit.Name}");
            Console.Write("New tag name (black to leave unchanged): ");
            string tagName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(tagName)) tagToEdit.Name = tagName;

            _tagRepository.Update(tagToEdit);
        }

        private void Remove()
        {
            Console.Clear();
            Tag tagToDelete = Choose("Which tag would you like to remove?");
            if (tagToDelete != null) _tagRepository.Delete(tagToDelete.Id);
        }
    }
}
