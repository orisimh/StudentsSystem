using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{

    [Table("Question")]

    public class Question
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        //[BsonElement("Id")]
        public int QST_Id { get; set; }
        public string QST_Text { get; set; }
        public int QST_Type { get; set; }
        public List<Answer> Answers { get; set; }

    //public string Gender { get; set; }
    //public int? Age { get; set; }
    //public double? GradesAvg { get; set; }
    //public DateTime? Brithday { get; set; }

    //public DateTime? DateIncrease { get; set; }
}
}
