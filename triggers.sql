---trigger insert genre after insert films
CREATE OR REPLACE TRIGGER Insert_genre
AFTER INSERT ON Films
FOR EACH ROW
BEGIN
    INSERT INTO Genre
    (Genre_name, Film_name)
    VALUES (:new.Genre_name, :new.Film_name);
END Insert_genre;



---trigger delete genre ofter delete films
CREATE OR REPLACE TRIGGER delete_genre
AFTER delete ON Films
FOR EACH ROW
BEGIN
DELETE FROM Genre
WHERE film_name = :old.Film_name and Genre_name =:old.Genre_name;
END delete_genre;


select * from sessions;
----trigger insert cinema insert session_id cinema name
CREATE OR REPLACE TRIGGER insert_session
AFTER insert ON sessions
declare
max_id number;
BEGIN
select max(session_id)into max_id from sessions;
insert into sessions(session_id,cinema_name) values(max_id+1,'an3all');
END insert_session;

-----------trigger после добавление в бронирование дооваляет в проверку юсеров
CREATE OR REPLACE TRIGGER Insert_check_user
AFTER INSERT ON Booking
FOR EACH ROW
declare
id_check_user1 number;
first_id number;
BEGIN
select max(check_user_id) into id_check_user1 from check_user;
select count(*) into first_id  from check_user;
if first_id = 0
then
    INSERT INTO check_user
    (check_user_id, user_name, session_id,place_number)
    VALUES (1,:new.user_name, :new.session_id, :new.place_number);
else
    INSERT INTO check_user
    (check_user_id, user_name, session_id,place_number)
    VALUES (id_check_user1+1,:new.user_name, :new.session_id, :new.place_number);
end if;
END Insert_check_user;

---после входа выполняет процедуру проверки статуса сеанса
create or replace 
trigger trlogon
AFTER LOGON ON database
begin
session_status();
end;