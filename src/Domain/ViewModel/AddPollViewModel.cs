using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ComeTogether.Domain.ViewModel
{
    public class AddPollViewModel
    {
        [Required(ErrorMessage = "Question is required.")]
        public string Question { get; set; }
        [Required(ErrorMessage = "Answer is required.")]
        public string Answer { get; set; }
    }
}
