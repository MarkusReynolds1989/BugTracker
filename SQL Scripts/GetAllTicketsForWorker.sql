create definer = markus@`%` procedure GetAllTicketsForWorker(IN UserId int)
begin
    select * from Ticket where worker_id = UserId;
end;

