create definer = markus@`%` procedure GetTicket(IN TicketId int)
begin
    select * from Ticket where ticket_id = TicketId;
end;

