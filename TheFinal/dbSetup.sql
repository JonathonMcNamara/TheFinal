CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

-- STUB CREATE KEEPS TABLE
CREATE TABLE IF NOT EXISTS keeps(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  creatorId VARCHAR(255) NOT NULL,
  name VARCHAR(255) NOT NULL,
  description TEXT NOT NULL,
  img VARCHAR(255) NOT NULL,
  views INT NOT NULL DEFAULT 0,
  kept INT NOT NULL DEFAULT 0,

  Foreign Key (creatorId) REFERENCES accounts(id)
) default charset utf8;
-- 

-- STUB USE THIS TO CREATE KEEPS
INSERT INTO keeps
(name, description, img, creatorId)
VALUES
("Gojo", "He speaks for himself", "https://img.webnovel.com/bookcover/19459283006520105/300/300.jpg", "62f692c85d4c5d880f69ac3a");
-- 

DROP TABLE keeps;

-- STUB SELECT ALL KEEPS
SELECT
k.*,
a.*
FROM keeps k
JOIN accounts a ON a.id = k.creatorId;
-- 


-- STUB CREATE VAULTS TABLE

CREATE TABLE IF NOT EXISTS vaults(
  id INT AUTO_INCREMENT NOT NULL PRIMARY KEY,
  creatorId VARCHAR(255) NOT NULL,
  name VARCHAR(255) NOT NULL,
  description text NOT NULL,
  isPrivate bool NOT NULL DEFAULT false,

  FOREIGN KEY (creatorId) REFERENCES accounts(id)
) default charset utf8;
-- 

-- STUB CREATE VAULTS
INSERT INTO vaults
(name, description, isPrivate, creatorId)
VALUES
("Anime Favorites", "Characters from my favorite anime", true, "62f692c85d4c5d880f69ac3a");
-- 

-- STUB SELECT VAULTS WITH THEIR CREATOR
SELECT
v.*,
a.* 
FROM vaults v
JOIN accounts a ON a.id = v.creatorId;
-- 

-- STUB CREATE VAULT KEEP TABLE
CREATE TABLE IF NOT EXISTS vaultkeeps(
  id INT AUTO_INCREMENT NOT NULL PRIMARY KEY,
  creatorId VARCHAR(255) NOT NULL,
  vaultId INT NOT NULL,
  keepId INT NOT NULL,

  FOREIGN KEY (creatorId) REFERENCES accounts(id),
  FOREIGN KEY (vaultId) REFERENCES vaults(id),
  FOREIGN KEY (keepId) REFERENCES keeps(id)
) default charset utf8;

DROP TABLE vaultkeeps;