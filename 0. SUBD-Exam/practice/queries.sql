CREATE SCHEMA `subd_practice` ;

--- 1)

CREATE TABLE `subd_practice`.`category` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `description` VARCHAR(45) NULL,
  `priority` DOUBLE NULL,
  PRIMARY KEY (`id`));

  CREATE TABLE `subd_practice`.`user` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `password` VARCHAR(45) NULL,
  `twitter` VARCHAR(45) NULL,
  `age` INT NULL,
  PRIMARY KEY (`id`));

CREATE TABLE `subd_practice`.`tag` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `description` VARCHAR(45) NULL,
  `hash` VARCHAR(16) NULL,
  PRIMARY KEY (`id`))

--- 2)

ALTER TABLE `subd_practice`.`tag` 
ADD COLUMN `category_id` INT NULL AFTER `hash`,
ADD INDEX `category_id_idx` (`category_id` ASC);
ALTER TABLE `subd_practice`.`tag` 
ADD CONSTRAINT `category_id`
  FOREIGN KEY (`category_id`)
  REFERENCES `subd_practice`.`category` (`id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;


  ALTER TABLE `subd_practice`.`category` 
ADD COLUMN `user_id` INT NULL AFTER `priority`,
ADD INDEX `user_id_idx` (`user_id` ASC);
ALTER TABLE `subd_practice`.`category` 
ADD CONSTRAINT `user_id`
  FOREIGN KEY (`user_id`)
  REFERENCES `subd_practice`.`user` (`id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;


ALTER TABLE `subd_practice`.`article_18` ??
ADD CONSTRAINT `user_art_id`
  FOREIGN KEY (`user_art_id`)
  REFERENCES `subd_practice`.`user` (`id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;

--- Many to One --- Begin 
CREATE TABLE `subd_practice`.`tag_category` (
  `id` INT NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`id`));

ALTER TABLE `subd_practice`.`tag_category` 
ADD COLUMN `tag_id` INT NULL AFTER `id`,
ADD INDEX `tag_id_idx` (`tag_id` ASC);
ALTER TABLE `subd_practice`.`tag_category` 
ADD CONSTRAINT `tag_id`
  FOREIGN KEY (`tag_id`)
  REFERENCES `subd_practice`.`tag` (`id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;

ALTER TABLE `subd_practice`.`tag_category` 
CHANGE COLUMN `category_id` `categoty_many_tag_id` INT(11) NULL DEFAULT NULL ;
ALTER TABLE `subd_practice`.`tag_category` 
ADD CONSTRAINT `categoty_many_tag_id`
  FOREIGN KEY (`categoty_many_tag_id`)
  REFERENCES `subd_practice`.`category` (`id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
--- End


--- 3)

INSERT INTO `subd_practice`.`article_18` (`name`, `content`, `price`) VALUES ('Takata', 'asdasdad', '1238');
INSERT INTO `subd_practice`.`article_18` (`name`, `content`, `price`) VALUES ('Stefo', 'fasij', '12.415');

INSERT INTO `subd_practice`.`category` (`description`, `priority`) VALUES ('adfgdfg', '55.5');
INSERT INTO `subd_practice`.`category` (`description`, `priority`) VALUES ('agsgasdg', '852.2');
INSERT INTO `subd_practice`.`category` (`description`, `priority`) VALUES ('8469.6', '8469.6');

INSERT INTO `subd_practice`.`tag` (`description`, `hash`) VALUES ('safasdga', 'gashds');
INSERT INTO `subd_practice`.`tag` (`description`, `hash`) VALUES ('sadasdasfg', 'gasgasg');

INSERT INTO `subd_practice`.`user` (`password`, `twitter`, `age`) VALUES ('agfags', 'asgasg', '33');
INSERT INTO `subd_practice`.`user` (`password`, `twitter`, `age`) VALUES ('gasgasga', 'asgasg', '55');


--- 4)

SELECT user.* FROM user
LEFT join category on category.user_id = user.id
LEFT join tag on tag.category_id =  category.id
where tag.id = 1;


--- 6)

CREATE TABLE `subd_practice`.`category_part1` (
  `description` LONGTEXT NULL);

CREATE TABLE `subd_practice`.`category_part2` (
  `id` INT NOT NULL,
  `priority` DOUBLE NULL,
  `user_id` INT NULL,
  PRIMARY KEY (`id`));

INSERT INTO Category_part1(description) 
	SELECT description FROM Category

insert into Category_part2(id, priority, user_id) 
	select id, priority, user_id from Category;

ALTER TABLE `subd_practice`.`category_part2` 
ADD INDEX `user_id_idx` (`user_id` ASC);
ALTER TABLE `subd_practice`.`category_part2` 
ADD CONSTRAINT `user_id`
  FOREIGN KEY (`user_id`)
  REFERENCES `subd_practice`.`user` (`id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;

ALTER TABLE `subd_practice`.`tag` 
DROP FOREIGN KEY `category_id`;
ALTER TABLE `subd_practice`.`tag` 
ADD CONSTRAINT `category_id`
  FOREIGN KEY (`category_id`)
  REFERENCES `subd_practice`.`category_part2` (`id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;


 --- alter category to remove foreign keys

DROP TABLE `subd_practice`.`category`; 
---^ err with foreign key

--- 8)

SELECT article_18.* FROM article_18
LEFT JOIN user on user.id = article_18.user_art_id
LEFT JOIN category_part2 on category_part2.user_id = user.id
where category_part2.id = 1;

