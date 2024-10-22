using System.ComponentModel.DataAnnotations;

namespace KontrastDB.Models
{
    public class ContrastReactions
    {
        [Key]
        public int CaseID { get; set; }
        public string ReactionType { get; set; }
        public string DateVarChar { get; set; }
        public string BatchNumber { get; set; }
        public string ContrastName { get; set; }
    }
}
