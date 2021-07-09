create table if not exists User
(
	user_id int auto_increment
		primary key,
	user_name varchar(45) not null,
	first_name varchar(45) not null,
	last_name varchar(45) not null,
	password varchar(100) not null,
	email varchar(45) not null,
	active_ind tinyint(1) default 1 not null,
	auth_level enum('Guest', 'User', 'Admin') null,
	constraint user_name
		unique (user_name)
);

