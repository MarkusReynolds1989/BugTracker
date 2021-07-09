create definer = markus@`%` procedure UpdateUser(IN UserId int, IN FirstName varchar(45), IN LastName varchar(45), IN ThisPassWord varchar(100), IN ThisEmail varchar(45), IN ActiveInd tinyint(1), IN AuthLevel enum('Guest', 'User', 'Admin'))
begin
    update User
    set first_name = FirstName,
        last_name  = LastName,
        password   = ThisPassWord,
        email      = ThisEmail,
        active_ind = ActiveInd,
        auth_level = AuthLevel
    where user_id = UserId;
end;

