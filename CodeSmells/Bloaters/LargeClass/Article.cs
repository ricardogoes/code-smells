using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeSmells.Bloaters.LargeClass
{
    public enum PublicationStatus
    {
        Draft,
        NeedsEditing,
        Approved,
        Published
    }

    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string AuthorUserName { get; set; }
        public DateTime? DatePublished { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastUpdated { get; set; }
        public string LastUpdatedByUserName { get; set; }
        public PublicationStatus Status { get; private set; }

        public static Article GetArticle(int id)
        {
            // fetch article from database

            return new Article();
        }

        public void SubmitForEditing(string updatingUserName)
        {
            if (Status != PublicationStatus.Draft)
            {
                throw new ApplicationException("Can only submit Draft articles for editing.");
            }
            Status = PublicationStatus.NeedsEditing;
            Save(updatingUserName);
        }

        public void Approve(string updatingUserName)
        {
            if (!IsInRole(updatingUserName, "Administrator") &&
                !IsInRole(updatingUserName, "Editor"))
            {
                throw new ApplicationException("Only editors and administrators can approve articles.");
            }
            if (Status != PublicationStatus.NeedsEditing)
            {
                throw new ApplicationException("Can only approve articles from the Needs Editing state.");
            }
            Status = PublicationStatus.Approved;
            Save(updatingUserName);
        }

        public void Publish(string updatingUserName)
        {
            if (!IsInRole(updatingUserName, "Administrator") &&
                !IsInRole(updatingUserName, "Editor"))
            {
                throw new ApplicationException("Only editors and administrators can publish articles.");
            }
            if (Status != PublicationStatus.Approved)
            {
                throw new ApplicationException("Can only publish articles that have been approved.");
            }
            Status = PublicationStatus.Published;
            DatePublished = DateTime.Now;
            Save(updatingUserName);
        }

        public void PublishOnDate(string updatingUserName, DateTime dateToPublish)
        {
            if (!IsInRole(updatingUserName, "Administrator") &&
                !IsInRole(updatingUserName, "Editor"))
            {
                throw new ApplicationException("Only editors and administrators can publish articles.");
            }
            if (Status != PublicationStatus.Approved)
            {
                throw new ApplicationException("Can only publish articles that have been approved.");
            }
            Status = PublicationStatus.Published;
            DatePublished = dateToPublish;
            Save(updatingUserName);
        }

        public void Save(string updatingUserName)
        {
            if (Id == 0)
            {
                SaveNew(updatingUserName);
                return;
            }
            if (IsInRole(updatingUserName, "Administrator") ||
                IsInRole(updatingUserName, "Editor") ||
                (IsInRole(updatingUserName, "Author") && updatingUserName == AuthorUserName))
            {
                DateLastUpdated = DateTime.Now;
                LastUpdatedByUserName = updatingUserName;

                // save to database
            }
        }

        private void SaveNew(string updatingUserName)
        {
            if (!IsInRole(updatingUserName, "Author") &&
                !IsInRole(updatingUserName, "Editor") &&
                !IsInRole(updatingUserName, "Administrator"))
            {
                throw new ApplicationException("You do not have permission to create an article.");
            }
            AuthorUserName = updatingUserName;
            DateCreated = DateTime.Now;
            DateLastUpdated = DateTime.Now;
            LastUpdatedByUserName = updatingUserName;
            Status = PublicationStatus.Draft;

            // save to database
        }

        public static bool IsInRole(string userName, string roleName)
        {
            return userName == roleName;
        }

        public static List<Article> ListArticlesByAuthor(string authorUserName)
        {
            throw new NotImplementedException();
        }
        public static List<Article> ListArticlesByStatus(PublicationStatus status)
        {
            throw new NotImplementedException();
        }
        public static List<Article> ListArticles()
        {
            throw new NotImplementedException();
        }
    }
}
