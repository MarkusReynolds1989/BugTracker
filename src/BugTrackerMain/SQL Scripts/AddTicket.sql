create definer = markus@`%` procedure AddTicket(IN WorkerId int, IN ThisTitle varchar(45), IN ThisDescription varchar(300), IN LoggerId int)
begin
    insert into Ticket (worker_id, title, description, logger_id)
    values (WorkerId, ThisTitle, ThisDescription, LoggerId);
end;

