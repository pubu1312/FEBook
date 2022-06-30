Create database EbookManagement;
USE EbookManagement;

CREATE TABLE Author(
  AuthorID INT IDENTITY(1,1),
  AuthorName VARCHAR(50) NOT NULL,
  Profiles VARCHAR(MAX) NOT NULL,
  PRIMARY KEY (AuthorID),
  DeleteStatus BIT DEFAULT(0)
);

CREATE TABLE Major(
  MajorName VARCHAR(20) NOT NULL,
  majorID INT IDENTITY(1,1),
  PRIMARY KEY (majorID),
  DeleteStatus BIT DEFAULT(0)
);

CREATE TABLE Subjects(
  SubjectID INT IDENTITY(1,1),
  Subname VARCHAR(15) NOT NULL,
  majorID INT  ,
  PRIMARY KEY (SubjectID),
  FOREIGN KEY (majorID) REFERENCES Major(majorID),
  DeleteStatus BIT DEFAULT(0)
);

CREATE TABLE Account(
  UserID INT IDENTITY(1,1),
  UserName VARCHAR(50) NOT NULL,
  Roles VARCHAR(5) NOT NULL,
  FullName VARCHAR(50) NOT NULL,
  Email VARCHAR(50) NOT NULL,
  Passwords VARCHAR(50) NOT NULL,
  majorID INT ,
  Phone VARCHAR(11) NOT NULL,
  PRIMARY KEY (UserID),
  FOREIGN KEY (majorID) REFERENCES Major(majorID),
  DeleteStatus BIT DEFAULT(0)
);

CREATE TABLE Book(
  BookID INT IDENTITY(1,1),
  BookName VARCHAR(MAX) NOT NULL,
  BookCover varchar(max) not null,
  YearOfPublic INT NOT NULL,
  Summary VARCHAR(MAX) NOT NULL,
  Languages VARCHAR(20) NOT NULL,
  Content VARCHAR(MAX) NOT NULL,
  UpdateDate DATE NOT NULL,
  SubjectID INT ,
  PRIMARY KEY (BookID),
  FOREIGN KEY (SubjectID) REFERENCES Subjects(SubjectID),
  DeleteStatus BIT DEFAULT(0)
);

CREATE TABLE BookRating(
  rateID  INT IDENTITY(1,1) NOT NULL,
  DateRating DATE NOT NULL,
  NoOfRate INT NOT NULL,
  CmtContent VARCHAR(MAX) NOT NULL,
  UserID INT ,
  BookID INT ,
  PRIMARY KEY (rateID),
  FOREIGN KEY (UserID) REFERENCES Account(UserID),
  FOREIGN KEY (BookID) REFERENCES Book(BookID),
  DeleteStatus BIT DEFAULT(0)
);

CREATE TABLE ReadingHistory(
  DateRead datetime not null , 
  UserID  INT IDENTITY(1,1) ,
  BookID INT ,
  PRIMARY KEY (UserID, BookID, DateRead),
  FOREIGN KEY (UserID) REFERENCES Account(UserID),
  FOREIGN KEY (BookID) REFERENCES Book(BookID),
  DeleteStatus BIT DEFAULT(0)
);

CREATE TABLE BookDowndoad(
  DownloadID INT IDENTITY(1,1),
  UserID INT ,
  BookID int , 
  PRIMARY KEY (DownloadID),
  FOREIGN KEY (UserID) REFERENCES Account(UserID),
  FOREIGN KEY (BookID) REFERENCES Book(BookID),
  DeleteStatus BIT DEFAULT(0)
);

CREATE TABLE BookAuthor(
  AuthorUpdateDate DATE NOT NULL,
  AuthorID INT IDENTITY(1,1) ,
  BookID INT ,
  PRIMARY KEY (AuthorID, BookID),
  FOREIGN KEY (AuthorID) REFERENCES Author(AuthorID),
  FOREIGN KEY (BookID) REFERENCES Book(BookID),
  DeleteStatus BIT DEFAULT(0)
);

CREATE TABLE ManageBook(
  UpdateDate DATE NOT NULL,
  UserID INT ,
  BookID INT ,
  PRIMARY KEY (UserID, BookID, UpdateDate),
  FOREIGN KEY (UserID) REFERENCES Account(UserID),
  FOREIGN KEY (BookID) REFERENCES Book(BookID),
  DeleteStatus BIT DEFAULT(0)
);
