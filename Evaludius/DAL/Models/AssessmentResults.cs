using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class AssessmentResult
    {
        public int Id { get; set; }

        public Assessment Assessment { get; set; }

        public Player Player { get; set; }

        public SkillSetItem SkillSetItem { get; set; }

        public int TotalPoints { get; set; }
    }
}
