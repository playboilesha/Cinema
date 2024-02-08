CREATE OR REPLACE
PACKAGE PACK_LOGIN_USER
as
procedure register_user(user_name1 in varchar2,user_password1 in varchar2, user_password2 in varchar2,message out varchar2);
procedure login_users(user_name1 in varchar2,user_password1 in varchar2,message out varchar2, status out varchar2);
end  PACK_LOGIN_USER;





create or replace package body PACK_LOGIN_USER
as
--1
procedure register_user(user_name1 in varchar2,user_password1 in varchar2, user_password2 in varchar2,message out varchar2)
is
input_string      VARCHAR2(4000) := user_password1;
work_string       VARCHAR2(4000);
encrypted_string  VARCHAR2(4000);
num_user number;
BEGIN
if user_password1 = user_password2
then
select count(*) into num_user from users where user_name = user_name1;
if num_user = 0
then
work_string := RPAD
                ( input_string
                , (TRUNC(LENGTH(input_string) / 8) + 1 ) * 8
                , CHR(0)
                );
DBMS_OBFUSCATION_TOOLKIT.DESENCRYPT
           (
             input_string     => work_string
           , key_string       => 'MagicKey'
           , encrypted_string => encrypted_string
           );
insert into users(user_name,user_password) values(user_name1,encrypted_string);
message := 'you are registered';
else
message :='login busy';
DBMS_OUTPUT.PUT_LINE('Такой логин уже есть');
end if;
else
message :='password dont match';
DBMS_OUTPUT.PUT_LINE('Пароли не совподают');
end if;
EXCEPTION
WHEN OTHERS THEN
   raise_application_error(-20001,'An error was encountered - '||SQLCODE||' -ERROR- '||SQLERRM);
END register_user;

--2
 procedure login_users(user_name1 in varchar2,user_password1 in varchar2,message out varchar2, status out varchar2)
is
input_string VARCHAR2(1000) := user_password1;
work_string VARCHAR2(1000);
encrypted_string  VARCHAR2(1000);
num number;
num2 number;
begin
select count(*) into num from users where user_name = user_name1;
if num > 0
then
work_string := RPAD 
(
   input_string
  , (TRUNC(LENGTH(input_string) / 8) + 1 ) * 8
  , CHR(0)
);
DBMS_OBFUSCATION_TOOLKIT.DESENCRYPT
           (
             input_string     => work_string
           , key_string       => 'MagicKey'
           , encrypted_string => encrypted_string
           );

select count(*) into num2 from users where user_password = encrypted_string;
if num2 > 0
then
message := 'login correct';
status := 'true';
DBMS_OUTPUT.PUT_LINE('Логин и пароль верен');
DBMS_OUTPUT.PUT_LINE('Логин '||user_name1|| ' '||'пароль '||user_password1);
else 
message := 'password incorrect';
status := 'false';
DBMS_OUTPUT.PUT_LINE('пароль не верен');
end if;
else 
message := 'login incorrect';
status := 'false';
DBMS_OUTPUT.PUT_LINE('логин не верен');
end if;
end login_users;
end  PACK_LOGIN_USER;



--1-регистрация
declare
a varchar2(40);
begin
PACK_LOGIN_USER.register_user('lesha2','12345','12345',a);
end;

select * from users;

--2-авторизация
declare
a varchar2(40);
b varchar2(40);
begin
PACK_LOGIN_USER.LOGIN_USERS('an3all','12345',a,b);
end;