using System;
using System.ComponentModel.DataAnnotations;

namespace MMTS.Models
{
    public class ForumPost
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime PostedOn { get; set; } = DateTime.Now;

        public string? FilePath { get; set; }
    }
}
