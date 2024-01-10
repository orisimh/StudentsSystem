using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Data;
using WebApplication2.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {

        private DemoDB _db;
        //private IMongoClient _mngclnt=null;
        // private IMongoCollection<Student> _clts = null;

        //DistributedCacheExtensions _dch = null;
        IDistributedCache _dch;
        // IJWTAuthenticationManager _jwt;
        //public StudentsController(DemoDB db, IDistributedCache  distributedCache , IMongoClient mngclnt , MongoDBConnectionString mndbconn , IJWTAuthenticationManager jwt) //IDistributedCache DistributedCacheExtensions cacheservice ,
        //{
        //    var mgdb = mngclnt.GetDatabase(mndbconn.DB);          
        //    _clts = mgdb.GetCollection<Student>(mndbconn.collections_name);

        //    _db = db;   
        //    _dch = distributedCache;

        //    _jwt = jwt;
        //}

        public QuestionsController(DemoDB db, IDistributedCache distributedCache)
        {

            _db = db;
            _dch = distributedCache;

        }


        [HttpGet]
        [EnableCors("ApiCorsPolicy")]
        public IActionResult Get(string questionId ,
            int pageNumber = 1,
            int pageSize = 10,
            string filterText = ""
            ) // [FromBody]
        {

            
            var num = _db.Question.ToList().Count;

            if (num == 0)
            {

                return StatusCode(404, "No Questions Found");
            }

            var query = _db.Question
           .Where(
                q =>  (  ( string.IsNullOrEmpty(questionId) || q.QST_Id.ToString() == questionId )

                 &&  ( string.IsNullOrEmpty(filterText) || q.QST_Text.Contains(filterText) )

                )
                );


            var result = query
              .OrderBy(q => q.QST_Id) 
              .Skip((pageNumber - 1) * pageSize)
             .Take(pageSize)
             .Select(q => new  {
              QST_Id  = q.QST_Id.ToString()  ,
              QST_Text = q.QST_Text  ,
              QST_Type = q.QST_Type.ToString()  ,

              Answers = _db.Answer
                    .Where(a => a.QuestionQST_Id == q.QST_Id)
                    .Select(a => new
                    {
                        AnswerId = a.ANS_Id,
                        AnswerText = a.ANS_Text ,
                        ANS_isCorrectAns = a.ANS_isCorrectAns,

                    })
                    .ToList()


            })
          .ToList();

    


            return Ok(result);



        }



        [HttpPost]  //  "Add/{id}"
        [Route("Add")]
        // [EnableCors("ApiCorsPolicy")]
        public IActionResult Add(Question qs) 
        {
            int questionId = 0 ; 

            try
            {
                if (qs == null)
                {

                    return StatusCode(404, "Object is NULL");
                }

                if (qs.QST_Type <= 0 )
                {

                    return BadRequest("please provide valid Question Type");
                }

                _db.Database.BeginTransaction();

                //var Answers = qs.Answers;
                //qs.Answers = null;

                _db.Question.Add(qs);


              //  _db.SaveChanges();


               // questionId =  _db.Question.OrderByDescending(q => q.QST_Id).ToList().First().QST_Id;

                foreach ( Answer ans in qs.Answers)
                {
                   // ans.ANS_QST_ID = questionId;

                    _db.Answer.Add(ans);

                }

                //_db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Answer ON;");

                _db.SaveChanges();

               //  _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Answer Off;");

                _db.Database.CommitTransaction();


            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
                //BadRequest(e.InnerException.Message);

            }

            return Ok(questionId);
            // _db.Question.OrderByDescending(q => q.QST_Id).ToList().First()
        }




        [HttpPost]  //  "Add/{id}"
        [Route("Vote")]
        // [EnableCors("ApiCorsPolicy")]
        public IActionResult Vote( int id  , int questionId) // JObject student [FromBody]
        {

            try
            {
                var query = _db.Question
                                .Where(q => q.QST_Id.ToString() == questionId.ToString());


                if (query.FirstOrDefault() == null)
                    return NotFound("Question Not Found");

                var isValid = query.Select(q => new
                {
                    Answer = _db.Answer
                          .Where(a => (a.QuestionQST_Id == q.QST_Id) && (a.ANS_Id == id)).FirstOrDefault()
                }).FirstOrDefault();


                if (isValid.Answer == null )
                {
                    return StatusCode(404, "please provide valid number of answer and question");
                }



                var answer = _db.Answer.Where(ans => ans.ANS_Id == id).FirstOrDefault(); // .FirstOrDefault().ANS_Votes;

                   
                answer.ANS_Votes += 1;
                _db.Answer.Update(answer);
                _db.SaveChanges();

                var type = query.FirstOrDefault().QST_Type;

                if (type == 2 ) // Question is type of "Trivia"
                {

                     var result = query
                         .Select(q => new {

                             Answers = _db.Answer
                            .Where(a => a.QuestionQST_Id == q.QST_Id)
                            .Select(a => new
                            {
                                AnswerId = a.ANS_Id,
                                AnswerText = a.ANS_Text,
                                ANS_Votes = a.ANS_Votes.ToString(),
                                ANS_isCorrectAns = a.ANS_isCorrectAns
                            }).ToList()
                         }).ToList();

                    return Ok(result);


                }
                else
                {

                    var result  = query
                     .Select(q => new {

                     Answers = _db.Answer
                        .Where(a => a.QuestionQST_Id == q.QST_Id)
                        .Select(a => new
                        {
                            AnswerId = a.ANS_Id,
                            AnswerText = a.ANS_Text,
                            ANS_Votes = a.ANS_Votes.ToString()
                        }).ToList()
                     }).ToList();


                    return Ok(result);

                }





               

            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
                //BadRequest(e.InnerException.Message);

            }

            // _db.Question.OrderByDescending(q => q.QST_Id).ToList().First()

        }


       
    }
}
