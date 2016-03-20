CREATE DATABASE job_portal
CHARACTER SET utf8 COLLATE utf8_unicode_ci;

USE job_portal;

CREATE TABLE users (
	id int primary key auto_increment not null,
	username varchar(50) charset utf8 not null, 
	fullname varchar(50) charset utf8 null
);

CREATE TABLE salaries (
	id int primary key auto_increment not null,
	from_value decimal not null, 
	to_value decimal not null
);


CREATE TABLE job_ads (
	id int primary key auto_increment not null,
	title varchar(50) charset utf8 not null, 
	description text charset utf8 null, 
	author_id int not null, 
	salary_id int not null,
	constraint foreign key (author_id) 
    	references users(id) 
    	on delete cascade 
    	on update cascade,
	constraint foreign key (salary_id) 
    	references salaries(id) 
    	on delete cascade 
    	on update cascade
);


CREATE TABLE job_ad_applications (
	id int primary key auto_increment not null,
	job_ad_id int not null, 
	user_id int not null,
	state int null,
	constraint foreign key (job_ad_id) 
    	references job_ads(id) 
    	on delete cascade 
    	on update cascade,
	constraint foreign key (user_id) 
    	references users(id) 
    	on delete cascade 
    	on update cascade
);

-- ---------------------------------------------------
USE job_portal;

SELECT u.username, u.fullname, ja.title 'Job', s.from_value 'From Value', s.to_value 'To Value'
FROM job_ad_applications jap
LEFT JOIN users u 
	ON u.id = jap.user_id
LEFT JOIN job_ads ja
	ON ja.id = jap.job_ad_id
LEFT JOIN salaries s
	ON s.id = ja.salary_id
ORDER BY u.username ASC, ja.title ASC;




username, fullname, Job, From Value, To Value
gosho, Georgi Manchev, C++ Developer, 2000.00, 3000.00
gosho, Georgi Manchev, Game Developer, 600.00, 800.00
gosho, Georgi Manchev, Java Developer, 1000.00, 1200.00
jivka, Jivka Goranova, .NET Developer, 1300.00, 1500.00
jivka, Jivka Goranova, Java Developer, 1000.00, 1200.00
minka, Minka Dryzdeva, .NET Developer, 1300.00, 1500.00
minka, Minka Dryzdeva, JavaScript Developer, 1500.00, 2000.00
petrohana, Peter Petromanov, Unity Developer, 550.00, 700.00








