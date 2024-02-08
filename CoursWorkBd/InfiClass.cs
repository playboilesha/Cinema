using System;
using System.Collections.Generic;
using System.Text;

namespace CoursWorkBd
{
    public class Login1
    {
        public static string login { get; set; }

    }
    public class InfiClass
    {
        public string connect = @"Data Source=orcl3; User ID=system; Password=Alexsimik123465";
        public string connect1 = @"Data Source=orcl3; User ID=XXXCORE1; Password=Alexsimik123465";
        public string login { get; set; }
        public string ProcedureLoginUsers = @"LOGIN_USERS";
        public string ProcedureLoginUsersParam1 = "user_name1";
        public string ProcedureLoginUsersParam2 = "user_password1";
        public string ProcedureLoginUsersParam3 = "message";
        public string ProcedureLoginUsersParam4 = "status";
        public string ProcedureRegisterUser = @"REGISTER_USER";
        public string ProcedureRegisterUserParam1 = "user_name1";
        public string ProcedureRegisterUserParam2 = "user_password1";
        public string ProcedureRegisterUserParam3 = "user_password2";
        public string ProcedureRegisterUserParam4 = "message";
        public string ProcedureInfoFilms = @"Info_films";
        public string ProcedureInfoFilmsParam1 = "strings";
        public string ProcedureInfoGenre = @"Info_genre";
        public string ProcedureInfoGenreParam1 = "Genre_name1";
        public string ProcedureInfoGenreParam2 = "strings";
        public string ProcedureInfoDirector = @"Info_Direcor";
        public string ProcedureInfoDirectorParam1 = "Film_name1";
        public string ProcedureInfoDirectorParam2 = "strings";
        public string ProcedureInfoDirectorParam3 = "message";
        public string ProcedureInfoSession = @"Info_Sessions_start";
        public string ProcedureInfoSessionParam1 = "strings";
        public string ProcedureInfoSessionParam2 = "status";
        public string ProcedureCheck_session = @"Check_session";
        public string ProcedureCheck_sessionParam1 = "session_id1";
        public string ProcedureCheck_sessionParam2 = "message";
        public string ProcedureCheck_sessionParam3 = "status";
        public string ProcedureSearchPlace = @"search_place";
        public string ProcedureSearchPlaceParam1 = "session_id1";
        public string ProcedureSearchPlaceParam2 = "places";
        public string ProcedureSearchPlaceParam3 = "count_place1";
        public string ProcedureSearchPlaceParam4 = "filmname";
        public string ProcedureBokkingUser = @"BOKKING_USER";
        public string ProcedureBokkingUserParam1 = "user_name1";
        public string ProcedureBokkingUserParam2 = "session_id1";
        public string ProcedureBokkingUserParam3 = "place_number1";
        public string ProcedureBokkingUserParam4 = "message";
        public string ProcedureInsertFilm = @"insert_film";
        public string ProcedureInsertFilmParam1 = "film_name1";
        public string ProcedureInsertFilmParam2 = "director1";
        public string ProcedureInsertFilmParam3 = "genre_name1";
        public string ProcedureInsertFilmParam4 = "duration_film1";
        public string ProcedureInsertFilmParam5 = "year_film1";
        public string ProcedureInsertFilmParam6 = "film_opis1";
        public string ProcedureInsertFilmParam7 = "message";
        public string ProcedureDeleteFilm = @"delete_film";
        public string ProcedureDeleteFilmParam1 = "film_name1";
        public string ProcedureDeleteFilmParam2 = "message";
        public string ProcedureInsertSessions = @"insert_sessions";
        public string ProcedureInsertSessionParam1 = "hall_id1";
        public string ProcedureInsertSessionParam2 = "film_name1";
        public string ProcedureInsertSessionParam3 = "session_start1";
        public string ProcedureInsertSessionParam4 = "session_price1";
        public string ProcedureInsertSessionParam5 = "session_status1";
        public string ProcedureInsertSessionParam6 = "message";
        public string ProcedureDeleteSessions = @"delete_sessions";
        public string ProcedureDeleteSessionsParam1 = "session_id1";
        public string ProcedureDeleteSessionsParam2 = "message";
        public string ProcedureUpdateSessions = @"update_sessions";
        public string ProcedureUpdateSessionsParam1 = "session_id1";
        public string ProcedureUpdateSessionsParam2 = "session_status1";
        public string ProcedureUpdateSessionsParam3 = "message";
        public string ProcedureInsertDirector = @"insert_director";
        public string ProcedureInsertDirectorParam1 = "director_name1";
        public string ProcedureInsertDirectorParam2 = "film_name1";
        public string ProcedureInsertDirectorParam3 = "director_opis1";
        public string ProcedureInsertDirectorParam4 = "message";
        public string ProcedureDeleteDirector = @"delete_director";
        public string ProcedureDeleteDirectorParam1 = "director_name1";
        public string ProcedureDeleteDirectorParam2 = "message";
        public string ProcedureInfoCheckUser = @"info_check_v_user";
        public string ProcedureInfoCheckUserParam1 = "session_id1";
        public string ProcedureInfoCheckUserParam2 = "cur";
        public string ProcedureInfoCheckUserParam3 = "message";
        public string ProcedureUpdateCheckUser = @"update_check_user";
       public string ProcedureUpdateCheckUserParem1 = "check_user_id1";
        public string ProcedureUpdateCheckUserParem2 = "status1";
        public string ProcedureUpdateCheckUserParem3 = "message";
        public string ProcedureCheckBookingUser = @"check_booking_user";
        public string ProcedureCheckBookingUserParem1 = "user_name1";
        public string ProcedureCheckBookingUserParem2 = "cur";
     




    }
}
