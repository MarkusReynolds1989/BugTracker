create definer = markus@`%` procedure GetAllTickets()
begin
    select * from Ticket;
end;

