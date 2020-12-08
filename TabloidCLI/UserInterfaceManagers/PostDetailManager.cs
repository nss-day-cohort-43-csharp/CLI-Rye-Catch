using System;
using System.Collections.Generic;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    internal class PostDetailManager : IUserInterfaceManager
    {
        private IUserInterfaceManager _parentUI;
        private PostRepository _postRepository;

        public PostDetailManager(IUserInterfaceManager parentUI, string connectionString, int postId)
        {

        }

        public IUserInterfaceManager Execute()
        {

        }
    }
}
