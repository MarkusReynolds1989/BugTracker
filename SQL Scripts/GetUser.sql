create definer = markus@`%` procedure GetUser(IN UserId int)
begin
    select * from User where user_id = UserId;
end;

