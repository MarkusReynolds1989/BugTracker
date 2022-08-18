create definer = markus@`%` procedure GetAllTicketsForLogger(IN UserID int)
begin
    select * from Ticket where logger_id = UserID;
end;

