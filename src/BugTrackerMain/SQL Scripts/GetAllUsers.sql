create definer = markus@`%` procedure GetAllUsers()
begin
    select * from User;
end;

