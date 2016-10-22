/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50626
Source Host           : localhost:3306
Source Database       : pigeonmanage

Target Server Type    : MYSQL
Target Server Version : 50626
File Encoding         : 65001

Date: 2016-10-22 20:58:45
*/

CREATE DATABASE `pigeonmanage` /*!40100 DEFAULT CHARACTER SET utf8 */


SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for pigeondiseasemanage
-- ----------------------------
DROP TABLE IF EXISTS `pigeondiseasemanage`;
CREATE TABLE `pigeondiseasemanage` (
  `KeyID` int(32) NOT NULL AUTO_INCREMENT,
  `鸽子keyID` int(255) NOT NULL,
  `发病日期` datetime NOT NULL,
  `疑似疾病` varchar(255) NOT NULL DEFAULT '',
  `确诊疾病` varchar(255) NOT NULL DEFAULT '',
  `基本症状` varchar(255) NOT NULL DEFAULT '',
  PRIMARY KEY (`KeyID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of pigeondiseasemanage
-- ----------------------------

-- ----------------------------
-- Table structure for pigeoninfo
-- ----------------------------
DROP TABLE IF EXISTS `pigeoninfo`;
CREATE TABLE `pigeoninfo` (
  `KeyID` int(11) NOT NULL AUTO_INCREMENT,
  `编号` varchar(50) NOT NULL,
  `性别` varchar(5) NOT NULL,
  `孵化日期` datetime NOT NULL,
  `配种方法` varchar(255) NOT NULL,
  `配前运动` varchar(255) NOT NULL,
  `配种日期` varchar(255) NOT NULL,
  PRIMARY KEY (`KeyID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of pigeoninfo
-- ----------------------------

-- ----------------------------
-- Table structure for pigeonmedicationmanage
-- ----------------------------
DROP TABLE IF EXISTS `pigeonmedicationmanage`;
CREATE TABLE `pigeonmedicationmanage` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `疾病管理ID` int(11) NOT NULL,
  `用药日期` datetime NOT NULL,
  `药物名称` varchar(255) NOT NULL,
  `生产厂家` varchar(255) NOT NULL,
  `用药途径` varchar(255) NOT NULL,
  `用药疗程` int(32) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of pigeonmedicationmanage
-- ----------------------------

-- ----------------------------
-- Table structure for productionmanage
-- ----------------------------
DROP TABLE IF EXISTS `productionmanage`;
CREATE TABLE `productionmanage` (
  `ID` int(32) NOT NULL AUTO_INCREMENT,
  `鸽子KeyID` int(32) NOT NULL,
  `窝次` int(32) NOT NULL,
  `产蛋日期` datetime NOT NULL,
  `产蛋量` int(32) NOT NULL,
  `正常蛋` int(32) NOT NULL DEFAULT '0',
  `双黄蛋` int(32) NOT NULL DEFAULT '0',
  `无黄蛋` int(32) NOT NULL DEFAULT '0',
  `软壳单` int(32) NOT NULL DEFAULT '0',
  `异物蛋` int(32) NOT NULL DEFAULT '0',
  `异状蛋` int(32) NOT NULL DEFAULT '0',
  `蛋包蛋` int(32) NOT NULL DEFAULT '0',
  `第一次照蛋正常蛋数量` int(32) NOT NULL DEFAULT '0',
  `无精蛋` int(32) NOT NULL DEFAULT '0',
  `死精蛋` int(32) NOT NULL DEFAULT '0',
  `第二次照蛋正常蛋数量` int(32) NOT NULL DEFAULT '0',
  `死胚蛋` int(32) NOT NULL DEFAULT '0',
  `岀雏数` int(32) NOT NULL DEFAULT '0',
  `健雏数` int(32) DEFAULT '0',
  `残疾数` int(32) NOT NULL DEFAULT '0',
  `死亡数` int(32) NOT NULL DEFAULT '0',
  `孵化类型` varchar(255) NOT NULL,
  `批次` varchar(255) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of productionmanage
-- ----------------------------
