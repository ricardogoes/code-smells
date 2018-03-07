using System;
using System.Linq;

namespace CodeSmells.Dispensables
{
    public class ArticleService
    {
        public void Edit(Article article, User user)
        {
            if (!user.IsInRole("Editors") && !article.Author.Equals(user))
            {
                // throw exception
            }
            // do other work
        }

        public void Publish(Article article, User user)
        {
            if (!user.IsInRole("Editors") && !article.Author.Equals(user))
            {
                // throw exception
            }
            // do other work
        }

        public void Delete(Article article, User user)
        {
            if (!user.IsInRole("Editors") && !article.Author.Equals(user))
            {
                // throw exception
            }
            // do other work
        }
    }

    public class User
    {
        public bool IsInRole(string p)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }
    }

    public class Article
    {
        public User Author { get; set; }
    }

    public class RefactoredArticleService
    {
        public void Edit(Article article, User user)
        {
            VerifyUserCanPerformAction(user, article);
            // do other work
        }
  
        public void Publish(Article article, User user)
        {
            VerifyUserCanPerformAction(user, article);
            // do other work
        }

        public void Delete(Article article, User user)
        {
            VerifyUserCanPerformAction(user, article);
            // do other work
        }

        private void VerifyUserCanPerformAction(User user, Article article)
        {
            if (!user.IsInRole("Editors") && !article.Author.Equals(user))
            {
                // throw exception
            }
        }
    }

}
