using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ViewModels;

namespace WorkoutPlanner.ViewModels
{
    public class MuscleViewModel:IViewModel
    {
        public int Id { get; set; }
        public string NameFrench { get; set; }
        public string NameEnglish { get; set; }

        public Byte[] Timestamp { get; set; }
    }
}