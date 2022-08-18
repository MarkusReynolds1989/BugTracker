create definer = markus@`%` procedure AuthenticateUser(IN UserName varchar(45), IN ThisPassword varchar(100))
begin
    select * from User where user_name = UserName and password = ThisPassword;
end;

