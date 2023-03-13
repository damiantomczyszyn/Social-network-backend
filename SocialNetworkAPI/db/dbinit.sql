-- MySQL Script generated by MySQL Workbench
-- Sat Apr 23 19:40:48 2022
-- Model: New Model    Version: 1.0
-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema social-network
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema social-network
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `social-network` DEFAULT CHARACTER SET utf8 ;
USE `social-network` ;

-- -----------------------------------------------------
-- Table `social-network`.`users`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `social-network`.`users` (
  `UserID` INT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(20) NOT NULL,
  `Surname` VARCHAR(20) NOT NULL,
  `PasswordHash` VARCHAR(50) NOT NULL,
  `EmailAddress` VARCHAR(35) NOT NULL,
  `TelephoneNumber` VARCHAR(15) NULL DEFAULT NULL,
  `JoinDate` DATE NOT NULL,
  `BirthdayDate` DATE NOT NULL,
  `AllowInvites` TINYINT NULL DEFAULT 1,
  `WorkingPlace` VARCHAR(45) NULL,
  `LearningPlace` VARCHAR(45) NULL,
  PRIMARY KEY (`UserID`),
  UNIQUE INDEX `UserID_UNIQUE` (`UserID` ASC) VISIBLE,
  UNIQUE INDEX `TelephoneNumber_UNIQUE` (`TelephoneNumber` ASC) VISIBLE,
  UNIQUE INDEX `EmailAddress_UNIQUE` (`EmailAddress` ASC) VISIBLE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `social-network`.`group`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `social-network`.`group` (
  `GroupID` INT NOT NULL AUTO_INCREMENT,
  `CreatorID` INT NOT NULL,
  `Name` VARCHAR(20) NOT NULL,
  `Description` VARCHAR(45) NULL DEFAULT NULL,
  `IsClosed` TINYINT NULL DEFAULT NULL,
  `ConversationID` INT NULL DEFAULT NULL,
  PRIMARY KEY (`GroupID`),
  INDEX `fk_group_users1_idx` (`CreatorID` ASC) VISIBLE,
  CONSTRAINT `fk_group_users1`
    FOREIGN KEY (`CreatorID`)
    REFERENCES `social-network`.`users` (`UserID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `social-network`.`posts`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `social-network`.`posts` (
  `PostID` INT NOT NULL AUTO_INCREMENT,
  `SenderID` INT NOT NULL,
  `GroupID` INT NULL,
  `ProfileID` INT NULL,
  `Content` MEDIUMTEXT NULL DEFAULT NULL,
  `DataTime` DATETIME NOT NULL,
  `messagescol` VARCHAR(45) NULL,
  PRIMARY KEY (`PostID`),
  INDEX `fk_posts_users_idx` (`SenderID` ASC) VISIBLE,
  INDEX `fk_posts_group1_idx` (`GroupID` ASC) VISIBLE,
  CONSTRAINT `fk_posts_users`
    FOREIGN KEY (`SenderID`)
    REFERENCES `social-network`.`users` (`UserID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_posts_group1`
    FOREIGN KEY (`GroupID`)
    REFERENCES `social-network`.`group` (`GroupID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `social-network`.`comments`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `social-network`.`comments` (
  `CommentID` INT NOT NULL AUTO_INCREMENT,
  `SenderID` INT NOT NULL,
  `PostID` INT NOT NULL,
  `Content` MEDIUMTEXT NULL DEFAULT NULL,
  `DateTime` DATETIME NOT NULL,
  `WasRemoved` TINYINT NULL DEFAULT NULL,
  PRIMARY KEY (`CommentID`),
  UNIQUE INDEX `CommentID_UNIQUE` (`CommentID` ASC) VISIBLE,
  INDEX `fk_comments_users1_idx` (`SenderID` ASC) VISIBLE,
  INDEX `fk_comments_posts1_idx` (`PostID` ASC) VISIBLE,
  CONSTRAINT `fk_comments_users1`
    FOREIGN KEY (`SenderID`)
    REFERENCES `social-network`.`users` (`UserID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_comments_posts1`
    FOREIGN KEY (`PostID`)
    REFERENCES `social-network`.`posts` (`PostID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `social-network`.`comment votes`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `social-network`.`comment votes` (
  `CommentVoteID` INT NOT NULL AUTO_INCREMENT,
  `CommentID` INT NOT NULL,
  `VotingUserID` INT NOT NULL,
  `IsUp` TINYINT NULL DEFAULT NULL,
  PRIMARY KEY (`CommentVoteID`),
  INDEX `fk_comment votes_users1_idx` (`VotingUserID` ASC) VISIBLE,
  UNIQUE INDEX `CommentVoteID_UNIQUE` (`CommentVoteID` ASC) VISIBLE,
  CONSTRAINT `fk_comment votes_users1`
    FOREIGN KEY (`VotingUserID`)
    REFERENCES `social-network`.`users` (`UserID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_comment votes_comments1`
    FOREIGN KEY (`CommentID`)
    REFERENCES `social-network`.`comments` (`CommentID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `social-network`.`friends`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `social-network`.`friends` (
  `User1ID` INT NOT NULL,
  `User2ID` INT NOT NULL,
  `FriendshipStartDate` DATE NULL DEFAULT NULL,
  INDEX `User1ID_idx` (`User1ID` ASC) INVISIBLE,
  INDEX `User2ID_idx` (`User2ID` ASC) VISIBLE,
  UNIQUE INDEX `ID constraint` (`User1ID` ASC, `User2ID` ASC) VISIBLE,
  CONSTRAINT `User1ID`
    FOREIGN KEY (`User1ID`)
    REFERENCES `social-network`.`users` (`UserID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `User2ID`
    FOREIGN KEY (`User2ID`)
    REFERENCES `social-network`.`users` (`UserID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `social-network`.`group participant`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `social-network`.`group participant` (
  `GroupID` INT NOT NULL AUTO_INCREMENT,
  `UserID` INT NOT NULL,
  `IsAdmin` TINYINT NULL DEFAULT NULL,
  `Nick` VARCHAR(20) NULL DEFAULT NULL,
  INDEX `fk_group participant_users1_idx` (`UserID` ASC) VISIBLE,
  UNIQUE INDEX `ID constraint` (`UserID` ASC, `GroupID` ASC) VISIBLE,
  CONSTRAINT `fk_group participant_users1`
    FOREIGN KEY (`UserID`)
    REFERENCES `social-network`.`users` (`UserID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_group participant_group1`
    FOREIGN KEY (`GroupID`)
    REFERENCES `social-network`.`group` (`GroupID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `social-network`.`conversation participant`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `social-network`.`conversation participant` (
  `ParticipantID` INT NOT NULL AUTO_INCREMENT,
  `UserID` INT NOT NULL,
  `ConversationID` INT NOT NULL,
  `LastViewDate` TIMESTAMP NULL,
  INDEX `fk_conversation participant_users1_idx` (`ConversationID` ASC) VISIBLE,
  PRIMARY KEY (`ParticipantID`),
  INDEX `fk_conversation participant_users2_idx` (`UserID` ASC) VISIBLE,
  CONSTRAINT `fk_conversation participant_users1`
    FOREIGN KEY (`ConversationID`)
    REFERENCES `social-network`.`users` (`UserID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_conversation participant_users2`
    FOREIGN KEY (`UserID`)
    REFERENCES `social-network`.`users` (`UserID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);


-- -----------------------------------------------------
-- Table `social-network`.`conversations`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `social-network`.`conversations` (
  `ConversationID` INT NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`ConversationID`));


-- -----------------------------------------------------
-- Table `social-network`.`messages`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `social-network`.`messages` (
  `MessageID` INT NOT NULL AUTO_INCREMENT,
  `ConversationID` INT NOT NULL,
  `ConversationParticipantID` INT NOT NULL,
  `WasRemoved` TINYINT NULL DEFAULT NULL,
  `DateTime` DATETIME NOT NULL,
  `Content` MEDIUMTEXT NULL DEFAULT NULL,
  `WasEdited` TINYINT NULL,
  `LastEditTime` TIMESTAMP NULL,
  PRIMARY KEY (`MessageID`),
  UNIQUE INDEX `DateTime_UNIQUE` (`DateTime` ASC) VISIBLE,
  INDEX `fk_messages_conversation participant2_idx` (`ConversationParticipantID` ASC) VISIBLE,
  INDEX `fk_messages_conversations2_idx` (`ConversationID` ASC) VISIBLE,
  CONSTRAINT `fk_messages_conversation participant2`
    FOREIGN KEY (`ConversationParticipantID`)
    REFERENCES `social-network`.`conversation participant` (`ParticipantID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_messages_conversations2`
    FOREIGN KEY (`ConversationID`)
    REFERENCES `social-network`.`conversations` (`ConversationID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `social-network`.`post view`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `social-network`.`post view` (
  `PostViewID` INT NOT NULL AUTO_INCREMENT,
  `PostID` INT NOT NULL,
  `UserID` INT NOT NULL,
  `DateTime` DATETIME NOT NULL,
  PRIMARY KEY (`PostViewID`),
  UNIQUE INDEX `PostViewID_UNIQUE` (`PostViewID` ASC) VISIBLE,
  INDEX `fk_senderid_idx` (`UserID` ASC) VISIBLE,
  CONSTRAINT `fk_post view_posts1`
    FOREIGN KEY (`PostID`)
    REFERENCES `social-network`.`posts` (`PostID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_senderid`
    FOREIGN KEY (`UserID`)
    REFERENCES `social-network`.`users` (`UserID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `social-network`.`post votes`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `social-network`.`post votes` (
  `PostVoteID` INT NOT NULL AUTO_INCREMENT,
  `PostID` INT NOT NULL,
  `VotingUserID` INT NOT NULL,
  `IsUp` TINYINT NULL DEFAULT NULL,
  INDEX `fk_post votes_users1_idx` (`VotingUserID` ASC) VISIBLE,
  PRIMARY KEY (`PostVoteID`),
  CONSTRAINT `fk_post votes_posts1`
    FOREIGN KEY (`PostID`)
    REFERENCES `social-network`.`posts` (`PostID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_post votes_users1`
    FOREIGN KEY (`VotingUserID`)
    REFERENCES `social-network`.`users` (`UserID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `social-network`.`blocked users`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `social-network`.`blocked users` (
  `BlockingUserID` INT NOT NULL,
  `BlockedUserID` INT NOT NULL,
  `BlockedDate` DATE NULL DEFAULT NULL,
  INDEX `User1ID_idx` (`BlockingUserID` ASC) INVISIBLE,
  INDEX `User2ID_idx` (`BlockedUserID` ASC) VISIBLE,
  UNIQUE INDEX `ID constraint` (`BlockingUserID` ASC, `BlockedUserID` ASC) VISIBLE,
  CONSTRAINT `User1ID0`
    FOREIGN KEY (`BlockingUserID`)
    REFERENCES `social-network`.`users` (`UserID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `User2ID0`
    FOREIGN KEY (`BlockedUserID`)
    REFERENCES `social-network`.`users` (`UserID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `social-network`.`invites`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `social-network`.`invites` (
  `InvitingUserID` INT NOT NULL,
  `InvitedUserID` INT NOT NULL,
  `InviteDate` DATE NULL DEFAULT NULL,
  INDEX `User1ID_idx` (`InvitingUserID` ASC) INVISIBLE,
  INDEX `User2ID_idx` (`InvitedUserID` ASC) VISIBLE,
  UNIQUE INDEX `ID constraint` (`InvitingUserID` ASC, `InvitedUserID` ASC) VISIBLE,
  CONSTRAINT `User1ID00`
    FOREIGN KEY (`InvitingUserID`)
    REFERENCES `social-network`.`users` (`UserID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `User2ID00`
    FOREIGN KEY (`InvitedUserID`)
    REFERENCES `social-network`.`users` (`UserID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `social-network`.`group join request`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `social-network`.`group join request` (
  `RequestingUserID` INT NOT NULL,
  `GroupID` INT NOT NULL,
  INDEX `fk_group join request_group1_idx` (`GroupID` ASC) VISIBLE,
  INDEX `fk_group join request_users1_idx` (`RequestingUserID` ASC) VISIBLE,
  CONSTRAINT `fk_group join request_users1`
    FOREIGN KEY (`RequestingUserID`)
    REFERENCES `social-network`.`users` (`UserID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_group join request_group1`
    FOREIGN KEY (`GroupID`)
    REFERENCES `social-network`.`group` (`GroupID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);


-- -----------------------------------------------------
-- Table `social-network`.`adresses`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `social-network`.`adresses` (
  `users_UserID` INT NOT NULL,
  `Country` VARCHAR(35) NULL,
  `City` VARCHAR(35) NULL,
  `Street` VARCHAR(45) NULL,
  `HouseNumber` VARCHAR(45) NULL,
  `ApartmentNumber` VARCHAR(45) NULL,
  PRIMARY KEY (`users_UserID`),
  CONSTRAINT `fk_timestamps_users1`
    FOREIGN KEY (`users_UserID`)
    REFERENCES `social-network`.`users` (`UserID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
