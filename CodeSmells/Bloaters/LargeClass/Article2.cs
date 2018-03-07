using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeSmells.Bloaters.LargeClass
{
    public class Article2Repository
    {
        public Article2 Get(int id)
        {
            // fetch article from database

            return new Article2();
        }

        public void Save(Article2 article)
        {
            // save to database
        }

        public List<Article2> ListArticles(Func<Article2, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }

    public interface IArticleCommands
    {
        void SubmitForEditing(string updatingUserName, Article2 article);
        void Approve(string updatingUserName, Article2 article);
        void Publish(string updatingUserName, Article2 article);
    }

    public abstract class ArticleState : IArticleCommands
    {
        public static readonly DraftArticleState Draft = new DraftArticleState();
        //public static readonly ArticleState NeedsEditing = new ArticleState("Needs Editing");
        //public static readonly ArticleState Approved = new ArticleState("Approved");
        //public static readonly ArticleState Published = new ArticleState("Published");
        public ArticleState(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }

        public abstract void SubmitForEditing(string updatingUserName, Article2 article);
        public abstract void Approve(string updatingUserName, Article2 article);
        public abstract void Publish(string updatingUserName, Article2 article);
    }

    public class DraftArticleState : ArticleState
    {
        public DraftArticleState()
            : base("Draft")
        {
        }

        public override void SubmitForEditing(string updatingUserName, Article2 article)
        {
            throw new NotImplementedException();
        }

        public override void Approve(string updatingUserName, Article2 article)
        {
            throw new ApplicationException("Can only approve articles from the Needs Editing state.");
        }

        public override void Publish(string updatingUserName, Article2 article)
        {
            throw new ApplicationException("Can only publish articles that have been approved.");
        }
    }
    public class ArticleSaveService
    {
        private readonly Article2Repository _articleRepository;

        public ArticleSaveService(Article2Repository articleRepository)
        {
            this._articleRepository = articleRepository;
        }

        public void Save(string updatingUserName, Article2 article)
        {
            if (article.Id == 0)
            {
                CreateNew(updatingUserName, article);
                return;
            }
            if (updatingUserName.IsInRole("Administrator") ||
                updatingUserName.IsInRole("Editor") ||
                (updatingUserName.IsInRole("Author") && updatingUserName == article.AuthorUserName))
            {
                article.DateLastUpdated = DateTime.Now;
                article.LastUpdatedByUserName = updatingUserName;

                _articleRepository.Save(article);
            }
        }

        private void CreateNew(string updatingUserName, Article2 article)
        {
            if (!updatingUserName.IsInRole("Author") &&
                !updatingUserName.IsInRole("Editor") &&
                !updatingUserName.IsInRole("Administrator"))
            {
                throw new ApplicationException("You do not have permission to create an article.");
            }
            article.AuthorUserName = updatingUserName;
            article.DateCreated = DateTime.Now;
            article.DateLastUpdated = DateTime.Now;
            article.LastUpdatedByUserName = updatingUserName;
            article.Status = ArticleState.Draft;

            _articleRepository.Save(article);
        }
    }

    public static class UserExtensions
    {
        public static bool IsInRole(this string userName, string roleName)
        {
            return userName == roleName;
        }
    }

    public class Article2
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string AuthorUserName { get; set; }
        public DateTime? DatePublished { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastUpdated { get; set; }
        public string LastUpdatedByUserName { get; set; }
        public ArticleState Status { get; set; }

        public void SubmitForEditing(string updatingUserName)
        {
            Status.SubmitForEditing(updatingUserName, this);
        }

        public void Approve(string updatingUserName)
        {
            Status.Approve(updatingUserName, this);
        }

        public void Publish(string updatingUserName)
        {
            Status.Publish(updatingUserName, this);
        }
    }
}
