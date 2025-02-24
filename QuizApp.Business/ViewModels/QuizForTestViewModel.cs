﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Business.ViewModels
{
    public class QuizForTestViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string QuizCode { get; set; }
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }
        public List<QuestionForTestViewModel> Questions { get; set; }
    }
}
