using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Services;

namespace WebApplication2.Data
{
	public class DBSeeder
	{
		private IMongoCollection<Student> _clts = null;

		public DBSeeder(){}

		public DBSeeder(IMongoClient mngclnt = null, MongoDBConnectionString mndbconn =null)
        {

			var mgdb = mngclnt.GetDatabase(mndbconn.DB);
			_clts = mgdb.GetCollection<Student>(mndbconn.collections_name);
		}

		public  void Seed()// EmployeeDbContext context
		{
			// context.Database.EnsureCreated() does not use migrations to create the database and therefore the database that is created cannot be later updated using migrations 
			// use context.Database.Migrate() instead


			if (_clts.Find(x=>true).Any())  //context.Employees.Any())
			{
				return;
			}


			_clts.InsertMany(GetDummyStudentsList()); //Database.Migrate();

			// insert dummy data
			//context.AddRange(GetDummyEmployeeList());
			//context.SaveChanges();
		}


		public static List<Student> GetDummyStudentsList()
		{
			var students = new List<Student> {
			new Student{ FirstName = "Foo", LastName = "Bar", Country = "address 1", Brithday =  DateTime.Today.AddYears(-1)},
			new Student{ FirstName = "first_name 2", LastName = "last_name 2",  Country = "address 2", Brithday =  DateTime.Today.AddYears(-2) },
			new Student{ FirstName = "first_name 3", LastName = "last_name 3", Country = "address 3", Brithday =  DateTime.Today.AddYears(-3)  },
			new Student{ FirstName = "first_name 4", LastName = "last_name 4",  Country = "address 4", Brithday =  DateTime.Today.AddYears(-4)  },
			new Student{ FirstName = "first_name 5", LastName = "last_name 5",  Country = "address 5", Brithday =  DateTime.Today.AddYears(-5) },
			new Student{ FirstName = "first_name 6", LastName = "last_name 6", Country = "address 6", Brithday =  DateTime.Today.AddYears(-6)  },
			new Student{ FirstName = "first_name 7", LastName = "last_name 7",  Country = "address 7", Brithday =  DateTime.Today.AddYears(-7)  },
			new Student{ FirstName = "first_name 8", LastName = "last_name 8",  Country = "address 8", Brithday =  DateTime.Today.AddYears(-8)  },
		};

			return students;
		}
	}
}
