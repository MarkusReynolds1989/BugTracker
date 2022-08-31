create table if not exists ticket
(
    TicketId        int auto_increment primary key,
    WorkerId        int                     not null,
    Title           varchar(45)             not null,
    Description     varchar(300)            not null,
    Resolution      varchar(300) default '' null,
    StatusIndicator int          default 0  not null,
    ActiveIndicator tinyint(1)   default 1  not null,
    LoggerId        int                     null
);

