﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AIA.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ChoiceHistory> ChoiceHistories { get; set; }
        public virtual DbSet<Choice> Choices { get; set; }
        public virtual DbSet<Condition> Conditions { get; set; }
        public virtual DbSet<Form> Forms { get; set; }
        public virtual DbSet<MasterScore> MasterScores { get; set; }
        public virtual DbSet<QuestionHistory> QuestionHistories { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionType> QuestionTypes { get; set; }
        public virtual DbSet<ScoreHistory> ScoreHistories { get; set; }
        public virtual DbSet<Score> Scores { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
    }
}
