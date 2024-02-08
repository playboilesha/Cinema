--������ ��� ������������
grant execute on SYSTEM.pack_user to XXXCORE1;
grant select on SYSTEM.sessions to XXXCORE1;
grant select on SYSTEM.booking to XXXCORE1;
grant select on SYSTEM.Films to XXXCORE1;
grant select on SYSTEM.Genre to XXXCORE1;
grant select on SYSTEM.halls to XXXCORE1;
grant select on SYSTEM.cinema to XXXCORE1;


CREATE OR REPLACE
PACKAGE PACK_USER
as
procedure Info_Sessions_start(strings out sys_refcursor,status in varchar2);
procedure Check_session(session_id1 in number,message out varchar2,status out varchar2);
procedure search_place(session_id1 in number,places out sys_refcursor,count_place1 out halls.count_place%type,filmname out sessions.film_name%type);
procedure BOKKING_USER(user_name1 varchar2,session_id1 number,place_number1 number,message out varchar2);
procedure Info_films(strings out sys_refcursor);
procedure Info_Genre(Genre_name1 in varchar, strings out sys_refcursor);
procedure Info_Direcor(Film_name1 in varchar2, strings out sys_refcursor,message out varchar2);
procedure search_film_for_name(Film_name1 in varchar2,strings out sys_refcursor,message out varchar2);
procedure info_cinema_v(strings out sys_refcursor);
procedure info_halls_v(strings out sys_refcursor);
procedure check_booking_user(user_name1 in varchar2,cur out sys_refcursor);
end PACK_USER;


create or replace package body PACK_USER
as
---1
procedure Info_Sessions_start(strings out sys_refcursor,status in varchar2)
is
begin
open strings for
select * from sessions where session_status = status;
end Info_Sessions_start;
----2
procedure Check_session(session_id1 in number,message out varchar2,status out varchar2)
is 
num number;
begin
select count(*) into num from sessions where session_id = session_id1;
if num = 1
then
message:='good';
status := 'true';
else
message:='no search session';
status := 'false';
end if;
end Check_session;
----3
procedure search_place(session_id1 in number,places out sys_refcursor,count_place1 out halls.count_place%type,filmname out sessions.film_name%type)
is
cursor curs is select place_number from BOOKING where session_id = session_id1;
i number := 1;
begin
select halls.count_place, sessions.film_name into count_place1,filmname from sessions join halls on sessions.hall_id = halls.hall_id  where sessions.session_id = session_id1;
dbms_output.put_line('���������� ����: ' || count_place1);
dbms_output.put_line('�����: ' || filmname);
dbms_output.put_line('������ ������� ����:');
for fcurs in curs
loop
dbms_output.put_line(fcurs.place_number);
end loop;
open places for select place_number from BOOKING where session_id = session_id1;
end  search_place;
---4
procedure BOKKING_USER(user_name1 varchar2,session_id1 number,place_number1 number,message out varchar2)
is
user_num number;
session_num number;
session_status varchar2(12);
place_count number;
check_count_place number;
begin
select a.count_place into check_count_place from halls a join sessions b on a.hall_id = b.hall_id where b.session_id = session_id1 and  ROWNUM = 1;
select count(*) into user_num from users where user_name = user_name1;
select count(*) into session_num  from sessions where session_id = session_id1;
select session_status into session_status  from sessions where session_id = session_id1;
select count(*) into place_count  from BOOKING where session_id = session_id1 and place_number = place_number1;
if place_number1 < check_count_place+1
then
 dbms_output.put_line('����� ����������');
if user_num > 0
then
  dbms_output.put_line('������');
  if session_num > 0
  then
      dbms_output.put_line('����� ����');
     if session_status = 'not started'
     then
        dbms_output.put_line('����� �� �������');
        if place_count = 0
        then
          dbms_output.put_line('����� �������� � ��������������');
          message := 'you reserved place';
          insert into BOOKING(user_name,session_id,place_number) values(user_name1,session_id1,place_number1);
        else
          dbms_output.put_line('����� ������');
           message := 'place occupied';
        end if;
    else
      dbms_output.put_line('����� �������');
      message := 'session start';
      end if;
  else 
    dbms_output.put_line('������ ������ ���');
    message := 'no search session';
    end if;
else
dbms_output.put_line('������ ������ ���');
message := 'loggin incorrect';
end if;
else
dbms_output.put_line('����� �� ����������');
message := 'no search place';
end if;
exception
when others then
dbms_output.put_line(sqlerrm);
end BOKKING_USER;
---5
 procedure Info_films(strings out sys_refcursor)
is 
cursor curs is select * from films;
begin
for fcurs in curs
loop
dbms_output.put_line('���: '||fcurs.Film_name||' �������: '||fcurs.Director||' ����: '||fcurs.Genre_name||' �����������������: '||fcurs.Duration_film||' ���: '||fcurs.Year_film||' ��������: '||fcurs.Film_opis);
end loop;
open strings for
select * from Films;
end Info_films;

---6
procedure Info_Genre(Genre_name1 in varchar, strings out sys_refcursor)
is 
cursor curs is select * from genre where Genre_name = Genre_name1;
begin
for fcurs in curs
loop
dbms_output.put_line('���: '||fcurs.Film_name||' ����: '||fcurs.Genre_name);
end loop;
open strings for
select * from Genre where Genre_name = Genre_name1;
end Info_Genre;
----7
procedure Info_Direcor(Film_name1 in varchar2, strings out sys_refcursor,message out varchar2)
is 
cursor curs is select * from director where contains(Film_name, '%'||Film_name1||'%') > 0;
num number;
begin
select count(*) into num from director where contains(Film_name, '%'||Film_name1||'%') > 0;
message := 'no search';
if num = 0
then
message :='no search';
dbms_output.put_line(message);
else
for fcurs in curs
loop
dbms_output.put_line('���: '||fcurs.Director_name||' �������� ������: '||fcurs.Film_name||' �������� ���������: '||fcurs.Director_opis);
end loop;
open strings for
select * from director where contains(Film_name, '%'||Film_name1||'%') > 0;
message :='good';
end if;
exception
when others
then 
  raise_application_error(-20001,'An error was encountered - '||SQLCODE||' -ERROR- '||SQLERRM);  
end Info_Direcor;

---8
procedure search_film_for_name(Film_name1 in varchar2,strings out sys_refcursor,message out varchar2)
is 
cursor curs is select * from films where contains(Film_name, '%'||Film_name1||'%') > 0;
num number;
index_name varchar2(20) := 'film_name_film';
begin
execute immediate 'alter index ' || index_name || ' rebuild';
select count(*) into num from films where contains(Film_name, '%'||Film_name1||'%') > 0;
message := 'no search';
if num = 0
then
message :='no search';
dbms_output.put_line(message);
else
for fcurs in curs
loop
dbms_output.put_line('��������: '||fcurs.Film_name||' �������: '||fcurs.Director||' ����: '||fcurs.Genre_name||' �����������������: '||fcurs.Duration_film||' ���: '||fcurs.Year_film||' ��������: '||fcurs.Film_opis);
end loop;
open strings for
select * from films where contains(Film_name, '%'||Film_name1||'%') > 0;
message :='good';
end if;
exception
when others
then 
  raise_application_error(-20001,'An error was encountered - '||SQLCODE||' -ERROR- '||SQLERRM);  
end search_film_for_name;

---9
procedure info_cinema_v(strings out sys_refcursor)
is cursor curs is select * from cinema_v;
begin
for fcurs in curs
loop
dbms_output.put_line('��������: '||fcurs.cinema_name||' ����� ��������: '||fcurs.cinema_telephone||' ��� ���������: '||fcurs.cinema_locate);
end loop;
open strings for
 select * from cinema_v;
exception
when others
then 
  raise_application_error(-20001,'An error was encountered - '||SQLCODE||' -ERROR- '||SQLERRM);  
end info_cinema_v;

--10
procedure info_halls_v(strings out sys_refcursor)
is cursor curs is select * from halls_v;
begin
for fcurs in curs
loop
dbms_output.put_line('Id: '||fcurs.hall_id||' ��������: '||fcurs.hall_name||' ���������� ����: '||fcurs.count_place);
end loop;
open strings for
 select * from halls_v;
exception
when others
then 
  raise_application_error(-20001,'An error was encountered - '||SQLCODE||' -ERROR- '||SQLERRM);  
end info_halls_v;

--11
 procedure check_booking_user(user_name1 in varchar2,cur out sys_refcursor)
is cursor curs is select c.session_id,c.film_name,c.session_start,c.session_price,b.place_number from users a join booking b on a.user_name = b.user_name join sessions c on b.session_id = c.session_id where a.user_name = user_name1 and c.session_status = 'not started';
begin
for fcurs in curs
loop
dbms_output.put_line('ID: '||fcurs.session_id||' �������� ������: '||fcurs.film_name||' ������ ������: '||fcurs.session_start||' ����: '||fcurs.session_price||' ����� �����: '||fcurs.place_number);
end loop;
open cur for
select c.session_id,c.film_name,c.session_start,c.session_price,b.place_number from users a join booking b on a.user_name = b.user_name join sessions c on b.session_id = c.session_id where a.user_name = user_name1 and c.session_status = 'not started';
exception
when others
then 
  raise_application_error(-20001,'An error was encountered - '||SQLCODE||' -ERROR- '||SQLERRM);  
end check_booking_user;
end PACK_USER;











----����� ��������
---1-����� ������� �������
declare 
cur sys_refcursor;
fetc system.sessions%rowtype; --define the record
begin
    system.pack_user.Info_Sessions_start(cur,'not started');  
 LOOP
  FETCH cur INTO fetc;
  EXIT WHEN cur%NOTFOUND;
  dbms_output.put_line('����� ������: '||fetc.session_id||' ��������� '||fetc.cinema_name||' ���: '||fetc.hall_id||' ����� '||fetc.film_name||' ������: '||fetc.session_start||' �����: '||fetc.session_time||' ����: '||fetc.session_price||' ������: '||fetc.session_status);
END LOOP;
end;
------------
----2-����� ������ � �������� ������ ���
declare
session_id1 number;
message  varchar2(20);
status  varchar2(20);
begin
system.pack_user.Check_session(3,message,status);
dbms_output.put_line(message);
dbms_output.put_line(status);
end;

---------
----3-����� ��������������� ����
declare 
cur sys_refcursor;
count_place1 system.halls.count_place%type;
num  system.booking.place_number%type;
filmname  system.sessions.film_name%type;
begin
system.pack_user.search_place(1,cur,count_place1,filmname);
---- LOOP
--- FETCH cur INTO num;
---  EXIT WHEN cur%NOTFOUND;
---  dbms_output.put_line(num);
--- END LOOP;
end;

--------------
---4-������������ ����
declare
message varchar2(40);
begin
system.pack_user.bokking_user('an3all',3,2,message);
end;
commit;
select * from SYSTEM.booking;
select * from system.sessions;

---5-���������� � �������
DECLARE
   cur sys_refcursor;
   -- Film_name varchar2(1000);
   -- Director varchar2(1000);
   -- Genre_name varchar2(20);
   -- Duration_film number;
   -- Year_film DATE;
   -- Film_opis varchar2(2000);
BEGIN
    system.pack_user.Info_films(cur);  
   -- LOOP
     --  EXIT WHEN cur%notfound;
     -- FETCH cur INTO Film_name, Director,Genre_name,Duration_film,Year_film,Film_opis;
     --  dbms_output.put_line('���: '||Film_name||' �������: '||Director||' ����: '||Genre_name||' �����������������: '||Duration_film||' ���: '||Year_film||' ��������: '||Film_opis);
   -- END LOOP;     
END;

---6-���������� � ������ 

DECLARE
    cur sys_refcursor;
    --Film_name varchar2(1000);
    --Genre_name varchar2(20);
BEGIN
    system.pack_user.Info_genre('drama', cur);  
     
      --FETCH cur INTO Genre_name,Film_name;
       --dbms_output.put_line('���: '||Film_name||' ����: '||Genre_name);       
END;


---7-���������� � �������� �� �������� ������
---��� ������ ������ ��� ������  ��������
begin
    ctx_ddl.create_preference('my_wordlist1', 'BASIC_WORDLIST');
    ctx_ddl.create_preference('my_lexer1', 'AUTO_LEXER');
    ctx_ddl.set_attribute('my_lexer1', 'INDEX_STEMS','YES');
end;
create index film_namex on Director (Film_name) indextype is ctxsys.context parameters ('LEXER my_lexer1 WORDLIST my_wordlist1');
ALTER INDEX film_namex REBUILD;
select * from director where contains(Film_name, 'Venom%') > 0;
---
DECLARE
    cur sys_refcursor;
    --Film_name varchar2(1000);
    --Director_name varchar2(20);
    --Director_opis varchar2(1000);
    message varchar2(20);
BEGIN

 system.pack_user.Info_Direcor('de',cur,message);
-- if cur%isopen then
--LOOP
--FETCH cur INTO Director_name,Film_name,Director_opis;
--EXIT WHEN cur%notfound;
--dbms_output.put_line('���: '||Director_name||' �������� ������: '||Film_name||' �������� ���������: '||Director_opis||' '|| message);
--END LOOP;
--close cur;
--else
--.put_line(message);
--end if;
END;

---8-��������� ������ ���������� � ������ �� ��������
------��� ������ ������ ��� ������  �������
GRANT EXECUTE ON CTX_DDL TO System;
begin
    ctx_ddl.create_preference('my_wordlist2', 'BASIC_WORDLIST');
    ctx_ddl.create_preference('my_lexer2', 'AUTO_LEXER');
    ctx_ddl.set_attribute('my_lexer2', 'INDEX_STEMS','YES');
end;
create index film_name_film on films (Film_name) indextype is ctxsys.context parameters ('LEXER my_lexer2 WORDLIST my_wordlist2');
select * from films;
select * from films where contains(Film_name, 've%') > 0;
----
declare
message varchar2(20);
cur sys_refcursor;
begin
system.pack_user.search_film_for_name('Ve',cur,message);
end;
 
---9-��������� ��������� � �����������
declare 
strings sys_refcursor;
begin
system.pack_user.info_cinema_v(strings);
end;

---10-��������� ��������� � �����
declare 
strings sys_refcursor;
begin
system.pack_user.info_halls_v(strings);
end;

---11-��������� ��������� ������� ������������� ������������
declare
cur sys_refcursor;
begin
system.pack_user.check_booking_user('an3all',cur);
end;