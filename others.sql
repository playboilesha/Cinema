--------------запись данных из текстового файла и в текстовый

spool 'D:\6 сем\kurs bd\filmselect.csv'
SELECT /*csv*/ * FROM films;
spool off




GRANT READ, WRITE ON DIRECTORY infilee TO system
CREATE or replace DIRECTORY infilee AS 'D:\6 сем\kurs bd';

-----Процедура записи в файл
CREATE OR REPLACE PROCEDURE INFILE AS
  CURSOR c_data IS
    SELECT *
    FROM   films;   
  v_file  UTL_FILE.FILE_TYPE;
BEGIN
  v_file := UTL_FILE.FOPEN(location     => 'INFILEE',
                           filename     => 'text.txt',
                           open_mode    => 'w',
                           max_linesize => 32767);
  FOR cur_rec IN c_data LOOP
    UTL_FILE.PUT_LINE(v_file,
                      cur_rec.film_name    || ',' ||
                      cur_rec.director    || ',' ||
                      cur_rec.genre_name     || ',' ||
                      cur_rec.duration_film      || ',' ||
                      cur_rec.year_film || ',' ||
                      cur_rec.film_opis );
  END LOOP;
  UTL_FILE.FCLOSE(v_file);  
EXCEPTION
  WHEN OTHERS THEN
    UTL_FILE.FCLOSE(v_file);
    RAISE;
END;

begin
INFILE();
end;

select * from films;

-----импорт в файл хмл
CREATE or replace DIRECTORY infilexml AS 'D:\6 сем\kurs bd\xml';

CREATE OR REPLACE PROCEDURE INFILE_XML AS
MYCLOB CLOB;
v_file  UTL_FILE.FILE_TYPE;
BEGIN
  select dbms_xmlgen.getxml('select * from films') into MYCLOB from DUAL;
    v_file := UTL_FILE.FOPEN(location     => 'INFILEXML',
                             filename     => 'input.xml',
                             open_mode    => 'w',
                             max_linesize => 32767);
      UTL_FILE.PUT(v_file,MYCLOB);
  
    UTL_FILE.FCLOSE(v_file);
  
EXCEPTION
  WHEN OTHERS THEN
    UTL_FILE.FCLOSE(v_file);
    RAISE;
END;
begin
INFILE_XML();
end;

--из хмл в бд
create or replace procedure exportXmlToFIlms
as
begin
insert into films(film_name,director,genre_name,duration_film,year_film,film_opis)
select * from xmltable('/ROWSET/ROW' 
  passing xmltype(BFILENAME('INFILEXML','output.xml'),
  nls_charset_id('char_cs'))
  columns film_name varchar2(1000) PATH 'FILM_NAME',
          director varchar2(1000) PATH 'DIRECTOR',
          genre_name varchar2(20) PATH 'GENRE_NAME',
          duration_film number PATH 'DURATION_FILM',
          year_film date PATH 'YEAR_FILM',
          film_opis varchar2(2000) PATH 'FILM_OPIS');
          commit;
exception            
  when others then
    dbms_output.put_line(sqlerrm);
end;


begin
exportXmlToFIlms();
end;

---запоолнения 100000
-----фукция рандомной строки (пароль)
create or replace function random_str(v_length number) return varchar2 is
    my_str varchar2(4000);
begin
    for i in 1..v_length loop
        my_str := my_str || dbms_random.string(
            case when dbms_random.value(0, 1) < 0.5 then 'l' else 'x' end, 1);
    end loop;
    return my_str;
end;

select random_str(15) from dual;

---логин
create or replace function RandomString(p_Characters varchar2,
                                        p_length number) return varchar2 is
    l_res varchar2(256);
begin
    select substr(listagg(substr(p_Characters, level, 1)) within group (
            order by dbms_random.value), 1, p_length)
    into l_res
    from dual connect by level <= length(p_Characters);
    return l_res;
end;
select RandomString('qwertyuiopasdfghjklzxcvbnm',14) from dual;

-- заполение 100000 таблицы юсер
declare 
str varchar2(40);
str1 varchar2(40);
msg varchar(40);
begin
FOR Lcntr IN 1..100000
LOOP
str := random_str(15);
str1:=RandomString('qwertyuiopasdfghjklzxcvbnm1234567890',10);
register_user(RandomString('qwertyuiopasdfghjklzxcvbnm1234567890',10),Lcntr,Lcntr,msg);
END LOOP;
end;

select count(*) from users;

---проверка производительности
select a.user_password,b.place_number,b.session_id from users a join  booking b on a.user_name = b.user_name
where a.user_name like ('a%');
explain plan for select a.user_password from users a join  booking b on a.user_name = b.user_name;
select plan_table_output from table(dbms_xplan.display());
create index user_name_index1 on users(user_password);

DROP index user_name_index1 ;

-------------секционированние 
drop table tests
create table tests (
t_id number(20),
n varchar(20)
)
partition by list(t_id)
(
partition li1 values(1),
partition li2 values(2),
partition li3 values(Default)
);
select count(*) from  tests;

select * from tests where t_id = 1;

explain plan for select * from tests where t_id = 1 ;
select plan_table_output from table(dbms_xplan.display());

select *  from tests partition(li1);

explain plan for select *  from tests partition(li1) ;
select plan_table_output from table(dbms_xplan.display());


-----заполнение
declare 
num number;
str1 varchar2(40);
msg varchar(40);
begin
FOR Lcntr IN 1..100000
LOOP
select round(dbms_random.value(1,100)) into num from dual;
str1:=RandomString('qwertyuiopasdfghjklzxcvbnm1234567890',10);
insert into tests values(num,str1);
END LOOP;
end;

create tablespace ts_xxx
datafile 'D:\app\user\prost\ts_XXX'
size 7 m 
autoextend on next 5 m
maxsize 20m
extent management local;

--2 zad

create temporary tablespace ts_xxx_TEMP1
tempfile 'D:\app\user\prost\ts_XXX_TEMP1'
size 5 m 
autoextend on next 3 m
maxsize 30m
extent management local;

--3zad
select TABLESPACE_NAME, STATUS, contents logging from SYS.DBA_TABLESPACES
--4 zad
ALTER SESSION SET "_ORACLE_SCRIPT" = TRUE;
create role RL_XXXCORE;



grant create session,
create table, 
create view,
create procedure to RL_XXXCORE;
--zad5
select * from dba_sys_privs where grantee = 'RL_XXXCORE';
select * from dba_roles; 




--zad 6 

create profile PF_XXXCORE limit
password_life_time 180
sessions_per_user 3
failed_login_attempts 7
password_lock_time 1  
password_reuse_time 10
password_grace_time default
connect_time 180
idle_time 30 --минут простоя

--zad 7 
select * from dba_profiles where profile = 'PF_XXXCORE';
select * from dba_profiles where profile = 'DEFAULT';
select * from dba_profiles

--zad 8
create user XXXCORE1 identified by 12345 
default tablespace ts_xxx quota unlimited on ts_xxx
temporary tablespace  ts_xxx_TEMP1
profile PF_XXXCORE
account unlock
password expire

alter profile PF_XXXCORE limit 
  password_life_time unlimited;

ALTER SESSION SET "_ORACLE_SCRIPT" = TRUE;
grant RL_XXXCORE to 




delete users XXXCORE

--11 zad
create tablespace l4_QDATA
datafile 'D:\app\user\prost\XXX_QDATA.dbf'
size 10 m 
autoextend on next 5 m
maxsize 20m
extent management local
offline;

select * from sys.dba_tablespaces

alter tablespace XXX_QDATA online

select * from dba_profiles
select * from dba_users
alter user XXXCORE1 
 quota 1000m on SIM_QDATA_5_1;
