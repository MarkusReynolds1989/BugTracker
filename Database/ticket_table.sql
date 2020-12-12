CREATE TABLE IF NOT EXISTS Ticket (
  ticket_id INT NOT NULL AUTO_INCREMENT,
  worker_id INT NOT NULL,
  title VARCHAR(45) NOT NULL,
  description VARCHAR(45) NOT NULL,
  resolution VARCHAR(45) NULL,
  status_ind INT NOT NULL,
  logger_id INT NOT NULL,
  PRIMARY KEY (ticket_id),
  UNIQUE INDEX `ticket_id_UNIQUE` (`ticket_id` ASC) VISIBLE,
  UNIQUE INDEX `user_id_UNIQUE` (`worker_id` ASC) VISIBLE,
  INDEX `user_id_idx` (`worker_id` ASC, `logger_id` ASC) VISIBLE,
  UNIQUE INDEX `logger_id_UNIQUE` (`logger_id` ASC) VISIBLE,
  CONSTRAINT `user_id`
    FOREIGN KEY (`worker_id` , `logger_id`)
    REFERENCES `mydb`.`User` (`user_id` , `user_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)