CREATE TABLE `Ticket` (
  `ticket_id` int(11) NOT NULL AUTO_INCREMENT,
  `worker_id` int(11) NOT NULL,
  `title` varchar(45) NOT NULL,
  `description` varchar(300) NOT NULL,
  `resolution` varchar(300) DEFAULT NULL,
  `status_ind` int(11) NOT NULL,
  `logger_id` int(11) NOT NULL,
  `active_ind` tinyint(4) NOT NULL DEFAULT 1,
  PRIMARY KEY (`ticket_id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4