/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50704
Source Host           : localhost:3306
Source Database       : pigeonmanage

Target Server Type    : MYSQL
Target Server Version : 50704
File Encoding         : 65001

Date: 2016-10-21 21:58:04
*/

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
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of pigeondiseasemanage
-- ----------------------------
INSERT INTO `pigeondiseasemanage` VALUES ('2', '3', '2016-10-18 00:00:00', '胃痛', '头痛', 'xix');
INSERT INTO `pigeondiseasemanage` VALUES ('3', '1', '2016-10-19 00:00:00', 'du', 'zi', 'lala');
INSERT INTO `pigeondiseasemanage` VALUES ('4', '3', '2016-10-19 00:00:00', 'du', 'zi', 'la');
INSERT INTO `pigeondiseasemanage` VALUES ('5', '1', '2016-10-20 00:00:00', '爱吃', '吃屎珍', '吃屎');
INSERT INTO `pigeondiseasemanage` VALUES ('6', '3', '2016-10-20 00:00:00', '爱吃', '吃屎珍', '吃屎');

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
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of pigeoninfo
-- ----------------------------
INSERT INTO `pigeoninfo` VALUES ('1', '001', '太监', '2015-10-31 00:00:00', '自然', '是', '2016-10-01 0:00:00');
INSERT INTO `pigeoninfo` VALUES ('3', '002', '公', '2001-01-01 00:00:00', '人工', '否', '2000-01-01 0:00:00');

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
) ENGINE=InnoDB AUTO_INCREMENT=68 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of pigeonmedicationmanage
-- ----------------------------
INSERT INTO `pigeonmedicationmanage` VALUES ('64', '6', '2016-10-21 00:00:00', '1', '2', '口服,', '3');
INSERT INTO `pigeonmedicationmanage` VALUES ('65', '5', '2016-10-21 00:00:00', '1', '2', '口服,', '3');
INSERT INTO `pigeonmedicationmanage` VALUES ('66', '6', '2016-10-21 00:00:00', '1', '2', '口服,', '3');
INSERT INTO `pigeonmedicationmanage` VALUES ('67', '6', '2016-10-21 00:00:00', '1', '2', '口服,', '3');

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
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of productionmanage
-- ----------------------------
INSERT INTO `productionmanage` VALUES ('1', '0', '1', '2016-10-21 21:52:53', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20', '21');
