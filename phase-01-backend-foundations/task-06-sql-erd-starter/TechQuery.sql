
create table authors
(
    authorid int primary key identity(1,1),
    fullname varchar(100) not null,
    birthdate date,
    country varchar(50)
);


create table categories
(
    categoryid int primary key identity(1,1),
    name varchar(100) not null,
    description varchar(255)
);


create table books
(
    bookid int primary key identity(1,1),
    title varchar(150) not null,
    isbn varchar(20),
    publishedyear int,
    availablecopies int not null,
    authorid int not null,
    categoryid int not null,

    foreign key (authorid) references authors(authorid),
    foreign key (categoryid) references categories(categoryid)
);


create table members
(
    memberid int primary key identity(1,1),
    fullname varchar(100) not null,
    phonenumber varchar(20),
    email varchar(100),
    joindate date not null,
    isactive bit not null
);


create table borrowrecords
(
    borrowrecordid int primary key identity(1,1),
    bookid int not null,
    memberid int not null,
    borrowdate date not null,
    duedate date not null,
    returndate date,
    status varchar(20) not null,

    foreign key (bookid) references books(bookid),
    foreign key (memberid) references members(memberid)
);




insert into authors (fullname, birthdate, country)
values
('j.k. rowling', '1965-07-31', 'united kingdom'),
('george orwell', '1903-06-25', 'united kingdom'),
('agatha christie', '1890-09-15', 'united kingdom'),
('mark twain', '1835-11-30', 'united states'),
('naguib mahfouz', '1911-12-11', 'egypt');




insert into categories (name, description)
values
('fantasy', 'fantasy and magical stories'),
('science fiction', 'science fiction and futuristic stories'),
('mystery', 'mystery and detective stories'),
('classic', 'classic literature'),
('arabic literature', 'arabic novels and literature');



insert into books
(title, isbn, publishedyear, availablecopies, authorid, categoryid)
values
('harry potter and the philosopher''s stone',
 '9780747532699', 1997, 4, 1, 1),

('harry potter and the chamber of secrets',
 '9780747549604', 1998, 2, 1, 1),

('1984',
 '9780451524935', 1949, 3, 2, 4),

('animal farm',
 '9780451526342', 1945, 5, 2, 4),

('murder on the orient express',
 '9780062693662', 1934, 1, 3, 3),

('the adventures of tom sawyer',
 '9780486400778', 1876, 0, 4, 4),

('the cairo trilogy',
 '9780385264668', 1956, 2, 5, 5),

('palace walk',
 '9780385264651', 1956, 3, 5, 5);




insert into members
(fullname, email, phonenumber, joindate, isactive)
values
('ahmed ali', 'ahmed@example.com', '01011111111', '2025-01-10', 1),
('mona hassan', 'mona@example.com', '01022222222', '2025-02-15', 1),
('omar mohamed', 'omar@example.com', '01033333333', '2025-03-20', 1),
('sara ahmed', 'sara@example.com', '01044444444', '2025-04-05', 0),
('youssef khaled', 'youssef@example.com', '01055555555', '2025-05-12', 1);




insert into borrowrecords
(bookid, memberid, borrowdate, duedate, returndate, status)
values
(3, 1, '2026-08-01', '2026-08-10', null, 'overdue'),
(1, 2, '2026-08-05', '2026-08-20', null, 'borrowed'),
(5, 3, '2026-07-20', '2026-07-30', '2026-07-28', 'returned'),
(4, 1, '2026-07-10', '2026-07-20', '2026-07-18', 'returned'),
(6, 4, '2026-07-01', '2026-07-10', null, 'overdue'),
(2, 5, '2026-08-10', '2026-08-25', null, 'borrowed'),
(8, 2, '2026-06-15', '2026-06-25', '2026-06-24', 'returned'),
(7, 3, '2026-08-12', '2026-08-22', null, 'borrowed');




select *
from books;


select *
from members
where isactive = 1;


-- select books by category

select *
from books
where categoryid = 1;

-- count books per category

select
    c.categoryid,
    c.name as categoryname,
    count(b.bookid) as bookcount
from categories c
left join books b
    on c.categoryid = b.categoryid
group by
    c.categoryid,
    c.name;


-- select borrow records with member name and book title

select
    br.borrowrecordid,
    m.fullname as membername,
    b.title as booktitle,
    br.borrowdate,
    br.duedate,
    br.returndate,
    br.status
from borrowrecords br
inner join members m
    on br.memberid = m.memberid
inner join books b
    on br.bookid = b.bookid;


-- select overdue books

select
    br.borrowrecordid,
    b.title as booktitle,
    m.fullname as membername,
    br.duedate,
    br.status
from borrowrecords br
inner join books b
    on br.bookid = b.bookid
inner join members m
    on br.memberid = m.memberid
where br.duedate < getdate()
and br.returndate is null;


--select borrowing history for one member

select
    br.borrowrecordid,
    b.title as booktitle,
    br.borrowdate,
    br.duedate,
    br.returndate,
    br.status
from borrowrecords br
inner join books b
    on br.bookid = b.bookid
where br.memberid = 1
order by br.borrowdate desc;


-- select available books

select *
from books
where availablecopies > 0;


-- count how many books each author has

select
    a.authorid,
    a.fullname as authorname,
    count(b.bookid) as bookcount
from authors a
left join books b
    on a.authorid = b.authorid
group by
    a.authorid,
    a.fullname;


-- select top 5 most borrowed books

select top 5
    b.bookid,
    b.title,
    count(br.borrowrecordid) as borrowcount
from books b
inner join borrowrecords br
    on b.bookid = br.bookid
group by
    b.bookid,
    b.title
order by
    borrowcount desc;