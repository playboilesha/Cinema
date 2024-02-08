LOAD DATA
INFILE 'film.txt'
APPEND
INTO TABLE films
FIELDS TERMINATED BY ',' OPTIONALLY ENCLOSED BY '"'
TRAILING NULLCOLS
(
  film_name,
  Director,
  Genre_name,
  Duration_film,
  Year_film DATE "YYYY",
  FIlm_opis
)