using System;
using System.Collections.Generic;
using System.Linq;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    public class InputHandler
    {
        private static int CheckInput(int inputId, List<int> idsAvailable)
        {
            while (true)
            {
                try
                {
                    inputId = int.Parse(Console.ReadLine());
                    if (inputId > 0)
                    {
                        List<int> matching = idsAvailable.Where(id => id == inputId).ToList();

                        if (matching.Any()) return inputId;
                        else throw new Exception();
                    }
                    else throw new Exception();
                }
                catch
                {
                    Console.WriteLine("Invalid Selection");
                }
            }
        }
        public static int CheckInputId(List<Author> authors)
        {
            List<int> idsAvailable = authors.Select(a => a.Id).ToList();
            int inputId = 0;
            return CheckInput(inputId, idsAvailable);
        }

        public static int CheckInputId(List<Blog> blogs)
        {
            List<int> idsAvailable = blogs.Select(a => a.Id).ToList();
            int inputId = 0;
            return CheckInput(inputId, idsAvailable);
        }

        public static int CheckInputId(List<Post> posts)
        {
            List<int> idsAvailable = posts.Select(a => a.Id).ToList();
            int inputId = 0;
            return CheckInput(inputId, idsAvailable);
        }

        public static int CheckInputId(List<Tag> tags)
        {
            List<int> idsAvailable = tags.Select(a => a.Id).ToList();
            int inputId = 0;
            return CheckInput(inputId, idsAvailable);
        }
    }
}
