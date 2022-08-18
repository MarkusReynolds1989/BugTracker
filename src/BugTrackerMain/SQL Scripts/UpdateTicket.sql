create definer = markus@`%` procedure UpdateTicket(IN TicketId int, IN WorkerId int, IN ThisTitle varchar(45), IN ThisDescription varchar(300), IN ThisResolution varchar(300), IN StatusInd int)
begin
    update Ticket
    set worker_id   = WorkerId,
        title       = ThisTitle,
        description = ThisDescription,
        resolution  = ThisResolution,
        status_ind  = StatusInd
    where ticket_id = TicketId;
end;

