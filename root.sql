CREATE OR REPLACE
PACKAGE PACK_ROOT
as
procedure info_check_v_user(session_id1 in number,cur out sys_refcursor,message out varchar2);
procedure update_check_user(check_user_id1 in number,status1 in varchar2,message out varchar2);
procedure insert_director(director_name1 in director.director_name%type,film_name1 in director.film_name%type,director_opis1 in director.director_opis%type,message out varchar2);
procedure delete_director(director_name1 in director.director_name%type,message out varchar2);
procedure insert_film(film_name1 in varchar2, director1 in varchar2, genre_name1 in varchar2,duration_film1 in number,year_film1 in varchar2,film_opis1 in varchar2,message out varchar2);
procedure delete_film(film_name1 in films.film_name%type,message out varchar2);
procedure insert_sessions(hall_id1 in sessions.hall_id%type,film_name1 in sessions.film_name%type,session_start1 in varchar2,session_price1 in sessions.session_price%type,session_status1 in sessions.session_status%type,message out varchar2);
procedure delete_sessions(session_id1 in number,message out varchar2);
procedure update_sessions(session_id1 in number, session_status1 in sessions.session_status%type,message out varchar2);
procedure session_status;
end PACK_ROOT;


  create or replace package body PACK_ROOT
  as
---1
 procedure info_check_v_user(session_id1 in number,cur out sys_refcursor,message out varchar2)
is
cursor curs is select * from check_user_v where session_id = session_id1;
i number;
g number;
begin
select count(*) into i from sessions where session_id = session_id1;
select count(*) into g from check_user_v  where session_id = session_id1;
if i > 0
then
if g = 0
then
message :='no search people';
dbms_output.put_line(message);
else
for fcurs in curs
loop
dbms_output.put_line('ID: '||fcurs.check_user_id||' User_name: '||fcurs.user_name||' session_id: '||fcurs.session_id||' num_pace: '||fcurs.place_number||' status: ' ||fcurs.status);
end loop;
message :='good';
dbms_output.put_line(message);
open cur for select * from check_user_v where session_id = session_id1;
end if;
else
message :='no search session';
dbms_output.put_line(message);
end if;
EXCEPTION
WHEN OTHERS THEN
message :='error';
dbms_output.put_line(message||' '||sqlerrm);
end info_check_v_user;

---2
procedure update_check_user(check_user_id1 in number,status1 in varchar2,message out varchar2)
is
begin
update check_user set status = status1 where check_user_id = check_user_id1;
message :='updated';
dbms_output.put_line(message);
EXCEPTION
WHEN OTHERS THEN
message :='error';
dbms_output.put_line(message||' '||sqlerrm);
end update_check_user;

--3
procedure insert_director(director_name1 in director.director_name%type,film_name1 in director.film_name%type,director_opis1 in director.director_opis%type,message out varchar2)
is 
num_film number;
index_name varchar2(40) := 'film_namex';
begin
select count(*) into num_film from films where film_name = film_name1;
if num_film = 1
then
insert into director(director_name,film_name,director_opis) values(director_name1,film_name1,director_opis1);
message := 'data add';
dbms_output.put_line(message);
execute immediate 'alter index ' || index_name || ' rebuild';
else
message := 'no search film';
dbms_output.put_line(message);
end if;
EXCEPTION
WHEN OTHERS THEN
message :='error';
dbms_output.put_line(message||' '||sqlerrm);
end insert_director;

--4
 procedure delete_director(director_name1 in director.director_name%type,message out varchar2)
is
num_director number;
begin
select count(*)into num_director from director where director_name = 'Terrence Malick';
if num_director > 0
then
delete director where director_name = director_name1;
message := 'data delete';
dbms_output.put_line(message);
else
message := 'no search film';
dbms_output.put_line(message);
end if;
EXCEPTION
WHEN OTHERS THEN
message :='error';
dbms_output.put_line(message||' '||sqlerrm);
end delete_director;

--5
procedure insert_film(film_name1 in varchar2, director1 in varchar2, genre_name1 in varchar2,duration_film1 in number,year_film1 in varchar2,film_opis1 in varchar2,message out varchar2)
is
year_fi date := to_date(year_film1,'YYYY');
len number;
namee films.film_name%type;
begin
SELECT INITCAP(film_name1) into namee  FROM DUAL;
SELECT LENGTH(year_film1) into len FROM DUAl;
if len = 4
then
insert into films(film_name,director,genre_name,duration_film,year_film,film_opis) values(namee,director1,genre_name1,duration_film1,year_fi,film_opis1);
message := 'data add';
dbms_output.put_line(message);
else
message := 'date bad';
end if;
EXCEPTION
WHEN OTHERS THEN
message :='error';
dbms_output.put_line(message||' '||sqlerrm);
end insert_film;

--6
procedure delete_film(film_name1 in films.film_name%type,message out varchar2)
is
num number;
num_session number;
begin
select count(*) into num_session from sessions where film_name = film_name1;
select count(*) into num from films where film_name = film_name1;
if num = 1
then
if num_session = 0
then
DELETE FROM films WHERE film_name = film_name1;
message := 'data delete';
dbms_output.put_line(message);
else
message := 'film use session';
dbms_output.put_line(message);
end if;
else
message := 'no search film';
dbms_output.put_line(message);
end if;
EXCEPTION
WHEN OTHERS THEN
message :='error';
dbms_output.put_line(message||' '||sqlerrm);
end delete_film;

--7
procedure insert_sessions(
hall_id1 in sessions.hall_id%type
,film_name1 in sessions.film_name%type
,session_start1 in varchar2
,session_price1 in sessions.session_price%type
,session_status1 in sessions.session_status%type
,message out varchar2)
is
max_id number;
duration1 number;
price varchar2(20);
num_film number;
datee date := to_date(session_start1,'DD.MM.YYYY');
begin
price := session_price1 || '$';
select count(*) into num_film from films where film_name = film_name1 and rownum = 1;
if num_film > 0
then
select duration_film into duration1 from films where film_name = film_name1;
select max(session_id)into max_id from sessions;
insert into sessions(session_id,cinema_name,hall_id,film_name,session_start,session_time,session_price,session_status) values(max_id+1,'an3all',hall_id1,film_name1,datee,duration1,price,session_status1);
message := 'data insert';
dbms_output.put_line(message);
else
message := 'no search film';
dbms_output.put_line(message);
end if;
EXCEPTION
WHEN OTHERS THEN
message :='error';
dbms_output.put_line(message||' '||sqlerrm);
end insert_sessions;

--8
procedure delete_sessions(session_id1 in number,message out varchar2)
is
num_session number;
begin
select count(*) into num_session from sessions where session_id=session_id1;
if num_session = 1
then
delete sessions where session_id = session_id1;
message := 'data delete';
dbms_output.put_line(message);
else
message := 'no search session';
dbms_output.put_line(message);
end if;
EXCEPTION
WHEN OTHERS THEN
message :='error';
dbms_output.put_line(message||' '||sqlerrm);
end delete_sessions;

--9
procedure update_sessions(session_id1 in number, session_status1 in sessions.session_status%type,message out varchar2)
is
num_session number;
begin
select count(*) into num_session from sessions where session_id=session_id1;
if num_session = 1
then
update sessions set session_status = session_status1 where session_id = session_id1;
message := 'data update';
dbms_output.put_line(message || 'изменено на: '||session_status1);
else
message := 'no search session';
dbms_output.put_line(message);
end if;
EXCEPTION
WHEN OTHERS THEN
message :='error';
dbms_output.put_line(message||' '||sqlerrm);
end update_sessions;

--10

procedure session_status
is cursor curs is select session_start from sessions;
sysdates date;
dates date;
begin
for fcurs in curs
loop
if sysdate >= fcurs.session_start
then
update sessions set session_status = 'started' where session_start = fcurs.session_start;
end if;
end loop;
EXCEPTION
WHEN OTHERS THEN
dbms_output.put_line(sqlerrm);
end;
end PACK_ROOT;










---1-информация бронирование мест на сеанс
declare 
cur sys_refcursor;
message varchar(30);
begin
PACK_ROOT.info_check_v_user(3,cur,message);
end;


--2-проверка посещения зрителя опрделенного сеанса
declare 
message varchar(40);
begin
PACK_ROOT.update_check_user(1,'none',message);
end;

select * from check_user


---3-добавление информации о режиссёрах
declare 
message varchar2(40);
begin
PACK_ROOT.insert_director('Ruben Fleischer','Venom','Ruben Samuel Fleischer (born October 31, 1974) is an American film director, film producer, television producer, music video director, and commercial director who lives in Montclair, New Jersey. He is best known as the director of Zombieland (2009), his first feature film, and its sequel Zombieland: Double Tap (2019)',message);
end;

select * from director;

---4-удаление информации о режиссёрах по имени
declare 
message varchar2(40);
begin
PACK_ROOT.delete_director('Ruben Fleischer',message);
end;

select * from director;

--5-добавление информации о фильмах
declare 
message varchar2(40);
begin
PACK_ROOT.insert_film('venom1','Ruben Fleischer1','action',112,'2018','A failed reporter is bonded to an alien entity, one of many symbiotes who have invaded Earth. But the being takes a liking to Earth and decides to protect it',message);
end;

select * from films;

--6-удаление информации о фильмах

declare 
message varchar2(40);
begin
PACK_ROOT.delete_film('Venom1',message);
end;

select * from films;
--7-процедура добавления сеанса
declare 
message varchar2(40);
begin
PACK_ROOT.insert_sessions(1,'Venom1','23.09.2023','6','not started',message);
end;

select * from sessions;

--7-процедура удаление сеанса
declare 
message varchar2(40);
begin
PACK_ROOT.delete_sessions(4,message);
end;
select * from sessions;


--7-процедура изменения сеанса по id
declare 
message varchar(40);
begin
PACK_ROOT.update_sessions(4,'not started',message);
end;

select * from sessions;

--10-проверка сеанса
begin
session_status();
end;