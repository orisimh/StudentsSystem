using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApplication2.Data
{
    [BsonIgnoreExtraElements]
    public class Students
    {
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            [Key]
            [BsonId]
            [BsonRepresentation(BsonType.Int32)]
            //[BsonElement("Id")]
            public int Id { get; set; } 
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Gender { get; set; }
            public int? Age { get; set; }
            public double? GradesAvg { get; set; }
            public DateTime? Brithday { get; set; }
            public DateTime? DateIncrease { get; set; }
            public string Country { get; set; }
            public string Nation { get; set; }
            public string Tel { get; set; }
            public string Phone { get; set; }
            public string email { get; set; }


        //public string Address { get; set; }
        //public string SchoolName { get; set; }
        //public Double AvgGrades { get; set; }
        //public int Age { get; set; }
        //[Id] nvarchar(10) NOT NULL,

        //   [FirstName] nchar(50) NULL,
        //[LastName] nchar(50) NULL,
        //[Gender] nvarchar(4)  NULL ,
        //[]
        //       date NULL,

        //   [DateIncrease] date NULL,




    }
    }
