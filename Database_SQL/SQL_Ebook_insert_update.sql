USE EbookManagement;

INSERT INTO Account(Username, Roles,Passwords , Fullname , email , phone) VALUES 
('user1' , 'Admin','12345' , 'Mr A', 'MrA@gmail.com', 12345678901)  ,
('user2' , 'User','12345' , 'Mr B', 'MrB@gmail.com', 12345678901)  ,
('user3' , 'User','12345' , 'Mr C', 'MrC@gmail.com', 12345678901) ,
('Khoa' , 'Admin','123456789' , 'Dong Bach Khoa', 'MrBachKhoa@gmail.com', 69696969)  ,
('BuPu' , 'User','123456' , 'Mr BuPu', 'MrBuPu@gmail.com', 12123)  ,
('JimmiBuh' , 'User','123456' , 'Mr Jimmi', 'JimmiBuh@gmail.com', 1678901)  ,
('NghiBuh' , 'User','123456' , 'Mr Nghi', 'MrNghi@gmail.com', 123456789), 
('XThanh' , 'User','123456' , 'Mr Thanh', 'MrThanh@gmail.com', 12345678910) ;
INSERT INTO Book(BookName, BookCover, YearOfPublic, Summary , Languages , Content , UpdateDate  ) VALUES 
('Clean Code' , 'images/cleancode.png',2008  ,'even bad code can function. But if code isn''t clean, it can bring a development organization to its knees.','English','Clean Codeis divided into three parts. The first describes the principles, patterns, and practices of writing clean code.', '2020-05-14'),
('Design Patterns: Elements of Reusable Object-Oriented Software', 'images/designpatterns.png',1994,'Design Patterns is a modern classic in the literature of object-oriented development','English','The book provides numerous examples where using composition rather than inheritance can improve the reusability and flexibility of code.', '2021-06-12'),
('The Pragmatic Programmer Your Journey To Mastery, 20th Anniversary Edition', 'images/thepragmaticprogrammer.png',2019,'To participate in the next generation of professional product delivery you have to be pragmatic but disciplined. ' , 'English', 'The Pragmatic Programmer filled with practical advice, both technical and professional, that will serve you and your projects well for years to come.', '2019-07-19')  ;

INSERT INTO Author(AuthorName , Profiles) VALUES 
('Robert C.Martin','founder and president of Object Mentor, Inc., a team of experienced consultants who mentor their clients worldwide in the fields of C++, Java, C#, Ruby, OO, Design Patterns, UML, Agile Methodologies, and eXtreme programming.'),
('Erich Gamma' , 'Swiss computer scientist and co-author of the influential software engineering textbook, Design Patterns: Elements of Reusable Object-Oriented Software. He co-wrote the JUnit software testing framework with Kent Beck and led the design of the Eclipse platforms Java Development Tools'),
('Andy Hunt' ,'an author and publisher. Hes authored and co-authored the best-selling classic The Pragmatic Programmer, the popular Pragmatic Thinking & Learning, award-winning Practices of an Agile Developer, Learn to Program with Minecraft Plugins: Create Flying Creepers and Flaming Cows in Java for the kids, many articles, and the novels in the Conglommora series. Andy was one of the 17 authors of the Agile Manifesto' );

INSERT INTO Subjects(SubName,majorID ) VALUES ('DiscreteMath', '1') , ('AdvancedMath','1') , ('ProMath','1'), ('JavaBeginner','2')
, ('JavaAdvanced','2'), ('BeginnerC','2') , ('AdvancedC','2') , ('BeginnerEnglish', '3') , ('AdvancedEnglish' ,'3') , ('PMath','3') ; 

INSERT INTO ManageBook(UpdateDate) VALUES 
('2019-07-19'),('2022-05-08') ,('2019-11-15')  ;

Insert into ReadingHistory(DateRead) Values ('20220608 10:34:09 AM') ; 

INSERT INTO Major(MajorName) VALUES 
('Computer science') , ('Mathematics') , ('other') ;  

INSERT INTO BookRating(Daterating ,NoOfRate ,CmtContent) VALUES 
('2019-01-12', 42 , 'some thing funny'),('2021-08-05' , 11 , 'ya ya') ,('2019-11-15',12 ,'blah blah')  ;

Select * from book ; 