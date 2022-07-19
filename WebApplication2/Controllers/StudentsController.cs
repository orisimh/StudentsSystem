using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.VisualStudio.RpcContracts.Caching;
using Newtonsoft.Json.Linq;
using WebApplication2.Data;
//using WebApplication2.Services;
using WebApplication2.Services;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase

    {

        private DemoDB _db;

        //DistributedCacheExtensions _dch = null;
        IDistributedCache _dch;
        public StudentsController(DemoDB db, IDistributedCache  distributedCache) //IDistributedCache DistributedCacheExtensions cacheservice ,
        {
            _db = db;   
            _dch = distributedCache;
        }


        //List<Student> stdnts = new List<Student>();


        //[HttpGet]
        //[EnableCors("ApiCorsPolicy")]
        //public IActionResult Get()
        //{
        //    var num = _db.Students.ToList().Count;
        //    if (num == 0)
        //    {

        //        return StatusCode(404, "No Students Found");
        //    }

        //    return Ok(_db.Students.ToList());
        //    // return Ok(default);


        //}

        //[Route("id")]
        [HttpGet("id")]
        [EnableCors("ApiCorsPolicy")]
        public  IActionResult GetId([FromQuery(Name = "id")]  string id)//  string iid ,
        {
            //string key = "089";
            var num =   _db.Students.ToList().Count;
            if (num == 0)
            {

                return StatusCode(404, "No Students Found");
            }

            //_dch.SetString("key02", "hello");
           var res =_dch.Get(id);
            if (res != null)
            {
                return Ok(res);      
            }
            return Ok(_db.Students.FirstOrDefaultAsync(x=> x.Id.ToString() == id));
            // return Ok(default);


        }
        //[Route("id")]
        [HttpGet]// "{key}"
        [EnableCors("ApiCorsPolicy")]
        public async Task<IActionResult> GetidAsync([FromQuery(Name = "key")] string key , string age)//, string key)
        {
            var num = await _db.Students.ToListAsync();
            if (num.Count == 0)
            {

                return StatusCode(404, "No Students Found");
            }

            if(key == null)
            {

                return Ok(_db.Students.ToList());
            }


            //_dch.SetString("key02", "hello");
            var res = await  _dch.GetRecordAsync<JObject>(key);
            if (res != null)
            {
                return Ok(res);
            }

            Students row =  await _db.Students.FirstOrDefaultAsync(x => x.Id.ToString() == key);
            await _dch.SetRecordAsync<Students>(key, row, TimeSpan.FromSeconds(10));


            return Ok(row);
            // return Ok(default);
        }

        [HttpGet("age")]
        [EnableCors("ApiCorsPolicy")]
        public async Task<IActionResult> GetAgeAsync(int age)// [FromQuery] Student st , string key) (Name = "age")
        {
            //var num = await _db.Students.ToListAsync();
            //if (num.Count == 0)
            //{

            //    return StatusCode(404, "No Students Found");
            //}

            //if (age == null)
            //{

            //    return Ok(_db.Students.ToList());
            //}


            ////_dch.SetString("key02", "hello");
            //var res = await _dch.GetRecordAsync<JObject>(age);
            //if (res != null)
            //{
            //    return Ok(res);
            //}

            //tblStudent row = await _db.Students.FirstOrDefaultAsync(x => x.Id == age);
            //await _dch.SetRecordAsync<tblStudent>(age, row, TimeSpan.FromSeconds(10));


            return Ok(default(string));
        }

        [HttpGet("GradesAvg")]
        [EnableCors("ApiCorsPolicy")]
        public async Task<IActionResult> GetGradesAsync(int age)// [FromQuery] Student st , string key) (Name = "age")
        {
            //var num = await _db.Students.ToListAsync();
            //if (num.Count == 0)
            //{

            //    return StatusCode(404, "No Students Found");
            //}

            //if (age == null)
            //{

            //    return Ok(_db.Students.ToList());
            //}


            ////_dch.SetString("key02", "hello");
            //var res = await _dch.GetRecordAsync<JObject>(age);
            //if (res != null)
            //{
            //    return Ok(res);
            //}

            //tblStudent row = await _db.Students.FirstOrDefaultAsync(x => x.Id == age);
            //await _dch.SetRecordAsync<tblStudent>(age, row, TimeSpan.FromSeconds(10));


            return Ok(default(string));
        }

        [HttpGet("FirstName")]
        [EnableCors("ApiCorsPolicy")]
        public async Task<IActionResult> GetFSAsync(string fs)// [FromQuery] Student st , string key) (Name = "age")
        {
            
            var res = await _dch.GetRecordAsync<JObject>(fs);
            if (res != null)
            {
                return Ok(res);
            }

            var results = await _db.Students.Where(x => x.FirstName == fs).ToListAsync();
            if (results.Count == 0)
            {

                return StatusCode(404, "No Students Found");
            }

            //tblStudent row = await _db.Students.FirstOrDefaultAsync(x => x.FirstName == fs);
            await _dch.SetRecordAsync<List<Students>>(fs, results, TimeSpan.FromSeconds(10));


            return Ok(results);
        }



        [HttpPost("Add")]  //  "Add/{id}"
        [EnableCors("ApiCorsPolicy")]
        public IActionResult Post(Student student) // JObject student [FromBody]
        {

            try
            {
                if (student == null)
                {

                    return StatusCode(404, "Object is NULL");
                }

                Students studentnew = new Students();

                //studentnew.Id = 0;// student.Id;
                studentnew.FirstName = student.FirstName;
                studentnew.LastName = student.LastName;
                studentnew.Gender = student.Gender;
                studentnew.Age = student.Age;
                studentnew.GradesAvg = student.GradesAvg;
                studentnew.Nation = student.Nation;
                studentnew.Phone = student.Phone;
                studentnew.Tel = student.Tel;
                studentnew.Brithday = student.Brithday;
                studentnew.DateIncrease = student.DateIncrease;
                studentnew.Country = student.Country;
                studentnew.email = student.email;

                // _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Tirgul].[dbo].[Students] ON");

                _db.Students.Add(studentnew);



                //tblDeltaStudent StudentDnew = new tblDeltaStudent();
                //StudentDnew.Id = student.Id;
                //StudentDnew.FirstName = student.FirstName;
                //StudentDnew.LastName = student.LastName;
                //StudentDnew.Gender = student.Gender;
                //StudentDnew.Age = student.Age;
                //StudentDnew.GradesAvg = student.GradesAvg;
                //StudentDnew.Nation = student.Nation;
                //StudentDnew.Phone = student.Phone;
                //StudentDnew.Tel = student.Tel;
                //StudentDnew.Brithday = student.Brithday;
                //StudentDnew.DateIncrease = student.DateIncrease;
                //StudentDnew.Country = student.Country;
                //StudentDnew.email = student.email;

                //_db.DeltaStudents.Add(StudentDnew);

             // _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Students ON");

                _db.SaveChanges();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.InnerException.Message);
                //BadRequest(e.InnerException.Message);

            }

            return Ok(_db.Students.ToList());

        }

        [HttpGet]
        [Route("Delta.csv")]
        [Produces("text/csv")]
        public IActionResult GetCSV()
        {
            try
            {             
                Encoding hebrewEncoding = Encoding.GetEncoding("Windows-1255");
                StringBuilder sb= CreateStringCsv();
                _db.Database.ExecuteSqlRaw("TRUNCATE TABLE DeltaStudents");
                return File(hebrewEncoding.GetBytes(sb.ToString()), "text/csv", "data.csv");

            }
            catch (Exception ex)
            {
                //HandleError(ex);
                return BadRequest(ex);
            }
        }



       

        private  StringBuilder CreateStringCsv()
        {
            var arr = _db.Students.ToList();
            List<object> students = (from student in arr//.Take(10)
                                     select new[] { student.Id.ToString(),
                                                         student.FirstName.ToString(),
                                                         student.LastName.ToString(),
                                                         student.Gender.ToString(),
                                                         student.Brithday.ToString(),
                                                         student.DateIncrease.ToString(),
                                                         student.email.ToString(),
                                                         student.Country.ToString(),
                                                         student.Phone.ToString(),
                                                         student.Tel.ToString(),
                                                         student.Nation.ToString()
                                }).ToList<object>();

            students.Insert(0, new string[11] { "ID", "FirstName","LastName","Gender" ,
                                                         "Brithday",
                                                         "DateIncrease",
                                                         "email",
                                                        "Country",
                                                         "Phone",
                                                         "Tel",
                                                         "Nation" });

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < students.Count; i++)
            {
                string[] student = (string[])students[i];
                for (int j = 0; j < student.Length; j++)
                {
                    //Append data with separator.
                    sb.Append(student[j] + ',');
                }

                //Append new line character.
                sb.Append("\r\n");

            }

            return sb;
        }

    }
}