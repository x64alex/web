drop table articles;
drop table journals;

CREATE TABLE journals (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(50) NOT NULL
);

CREATE TABLE articles (
    id INT AUTO_INCREMENT PRIMARY KEY,
    user VARCHAR(50) NOT NULL,
    journalid INT,
	FOREIGN KEY(journalid) REFERENCES journals(id),
    summary VARCHAR(100),
    date DATE
);	
insert into journals(name) values("1");
insert into articles(user,journalid,summary,date) values("1",1,"adsa","200202002");


select * from journals j;
select * from articles a;