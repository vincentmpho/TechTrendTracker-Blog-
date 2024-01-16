﻿namespace TechTrendTracker.Models.Domain
{
    public class BlogPost
    {
        public Guid  Id { get; set; }

        public string Heading { get; set; }

        public string PageTitle { get; set; }

        public string Content { get; set; }

        public string ShortDescription { get; set; }

        public string FeauredImageUrl { get; set; }

        public DateTime PublishedDate { get; set; }

        public string Author { get; set; }

        public bool visible { get; set; }

        public int MyProperty { get; set; }

        public ICollection<Tag>Tags  { get; set; }
    }
}
