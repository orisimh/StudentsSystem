using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    [Table("Answer")]

    public class Answer
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ANS_Id { get; set; }
        // public int ?ANS_QST_ID { get; set; }
        public string ANS_Text { get; set; }
        public int ANS_isCorrectAns { get; set; }
        public int? ANS_Votes { get; set; }
        public int? QuestionQST_Id { get; set; }

        

    }
}
