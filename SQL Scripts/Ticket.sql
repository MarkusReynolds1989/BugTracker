create table if not exists Ticket
(
	ticket_id int auto_increment
		primary key,
	worker_id int not null,
	title varchar(45) not null,
	description varchar(300) not null,
	resolution varchar(300) default '' null,
	status_ind enum('Open', 'InProgress', 'Closed') default 'Open' not null,
	active_ind tinyint(1) default 1 not null,
	logger_id int null
);

