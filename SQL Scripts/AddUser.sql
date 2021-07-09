create definer = markus@`%` procedure AddUser(IN UserName varchar(45), IN FirstName varchar(45), IN LastName varchar(45), IN ThisPassword varchar(100), IN ThisEmail varchar(45), IN AuthLevel enum('Guest', 'User', 'Admin'))
begin
    insert into User (user_name, first_name, last_name, password, email, auth_level)
    values (UserName, FirstName, Lastname, ThisPassword, ThisEmail, AuthLevel);
end;

