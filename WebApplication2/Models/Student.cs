using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApplication2
{
    //[BsonIgnoreExtraElements]
    public class Student
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        //[BsonElement("id")]
        public string Id { get; set; }

        [BsonElement("firstname")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int? Age { get; set; }
        public double GradesAvg { get; set; }
        public DateTime Brithday { get; set; }
        public DateTime DateIncrease { get; set; }
        public string Country { get; set; }
        public string Nation { get; set; }
        public string Tel { get; set; }
        public string Phone { get; set; }
        public string email { get; set; }


        //public DateTime Date { get; set; }

        //public int id { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string Address { get; set; }
        //public string SchoolName { get; set; }
        //public float AvgGrades { get; set; }
        //public int Age { get; set; }
    }
}
