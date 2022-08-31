create table if not exists user
(
    UserId              int auto_increment
        primary key,
    UserName            varchar(45)          not null,
    FirstName           varchar(45)          not null,
    LastName            varchar(45)          not null,
    Password            varchar(100)         not null,
    Email               varchar(45)          not null,
    ActiveIndicator     tinyint(1) default 1 not null,
    AuthenticationLevel int        default 0 null,
    constraint UserName
        unique (UserName)
);

