CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

-- SECTION albums
CREATE TABLE albums(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  ownerId VARCHAR(255) NOT NULL,
  title VARCHAR(100) NOT NULL,
  category VARCHAR(50) NOT NULL,

  FOREIGN KEY (ownerId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

ALTER TABLE albums
ADD COLUMN coverImg VARCHAR(500);

ALTER TABLE albums
ADD COLUMN archived BOOLEAN NOT NULL DEFAULT false;

DROP Table albums;

INSERT INTO albums
(title, category, `ownerId`)
VALUES
('Side of fries', 'Food', '631b5b5fa7f0b66bb817725a');

DELETE from accounts WHERE id = '634844a08c9d1ba02348913d';

SELECT
alb.*,
acct.*
FROM albums alb
JOIN accounts acct ON alb.ownerId = acct.id;

-- SECTION Pictures

CREATE TABLE pictures(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  ownerId VARCHAR(255) NOT NULL,
  albumId INT NOT NULL,
  imgUrl VARCHAR(500) NOT NULL,

  FOREIGN KEY (ownerId) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN KEY (albumId) REFERENCES albums(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

INSERT INTO pictures
(`imgUrl`,`albumId`, `ownerId`)
VALUES
('https://images.unsplash.com/photo-1523626752472-b55a628f1acc?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=735&q=80', 12, '634844a08c9d1ba02348913d');

SELECT
*
FROM pictures pic
JOIN accounts acct ON acct.id = pic.ownerId
JOIN albums alb ON alb.id = pic.albumId
WHERE albumId = 11;







