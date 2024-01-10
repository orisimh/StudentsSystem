

POC Question Asking Assaigment 

Instructions :

to run locally in port 44383: 
https://localhost:44383/api/questions

to run with Swagger:
https://localhost:44383/Index.html

The tables scripts to create the tables in sql server :

create table Question (

	[QST_Id] [int] IDENTITY(1,1) NOT NULL primary key,
	[QST_Text] [nvarchar](max) not NULL,
	[QST_Type] int not NULL,

)


create table Answer (

	[ANS_Id] [int] IDENTITY(1,1) NOT NULL ,
	[ANS_QST_ID] int not NULL,
	[ANS_Text] [nvarchar](max) not NULL,
	[ANS_isCorrectAns] int null default(0) ,
	[ANS_Votes] int  default(0) with values  

)

	 

create table QuestionType (

	[QSTP_Id] [int]  NOT NULL primary key,
	[QSTP_Desc] nvarchar(max) not NULL,


)


