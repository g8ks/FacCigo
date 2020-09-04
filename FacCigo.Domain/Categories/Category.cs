using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace FacCigo
{
    public class Category : Entity<Guid>
    {
        public string Name { get; set; }
        public Guid? CategoryId { set; get; }
        public Category ParentCategory { set; get; }
        public Collection<Category> SubCategories { set; get; }
        public Collection<Exam> Exams { set; get; }
        public string ShortName { get; set; }
        public Category (Guid id){
            Id = id;
        }

        public Category(Guid id,string name,string shortname)
        {
            Id = id;
            Name = name;
            ShortName = shortname;
        }
        public Category(Guid id, string name,string shortname,Guid categoryId)
        {
            Id = id;
            Name = name;
            CategoryId = categoryId;
            ShortName = shortname;
        }
    }
}
