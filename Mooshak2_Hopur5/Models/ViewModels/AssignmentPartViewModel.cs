﻿using Mooshak2_Hopur5.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mooshak2_Hopur5.Models.ViewModels
{
    public class AssignmentPartViewModel
    {
        public int AssignmentPartId { get; set; }
        [Required]
        public int AssignmentId { get; set; }
        [Required]
        public string AssignmentName { get; set; }
        [DisplayName("Part name")]
        [Required]
        public string AssignmentPartName { get; set; }
        [DisplayName("Part description")]
        [Required]
        public string AssignmentPartDescription { get; set; }
        public int Weight { get; set; }
        [DisplayName("Programming language")]
        [Required]
        public int ProgrammingLanguageId { get; set;  }
        public List<AssignmentPartViewModel> AssignmentPartList { get; set; }
        public List<AssignmentTestCase> AssignmentTestCaseList { get; set; }
        public List<SubmissionViewModel> PartSubmissionsList { get; set; }
        public IEnumerable<SelectListItem> ProgrammingLanguages { get; set; }
        public AssignmentPartFile File { get; set; }

        #region extra properties
        
        [DisplayName("Attachment")]
        public HttpPostedFileBase AssignmentPartUploaded { get; set; }
        
        #endregion
    }
}