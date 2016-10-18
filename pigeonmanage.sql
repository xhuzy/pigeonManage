/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50626
Source Host           : localhost:3306
Source Database       : pigeonmanage

Target Server Type    : MYSQL
Target Server Version : 50626
File Encoding         : 65001

Date: 2016-10-18 23:08:31
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
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of pigeondiseasemanage
-- ----------------------------
INSERT INTO `pigeondiseasemanage` VALUES ('1', '1', '2016-10-17 22:04:45', '艾滋病', '癌症', '吃屎');
INSERT INTO `pigeondiseasemanage` VALUES ('2', '3', '2016-10-18 23:00:25', '胃痛', '头痛', '哈哈哈');

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
INSERT INTO `pigeoninfo` VALUES ('1', '001', '母', '2015-10-31 00:00:00', '自然', '是', '2016-10-1');
INSERT INTO `pigeoninfo` VALUES ('3', '002', '公', '2001-01-01 00:00:00', '人工', '否', '2000-01-01');

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
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of pigeonmedicationmanage
-- ----------------------------
INSERT INTO `pigeonmedicationmanage` VALUES ('1', '1', '2016-10-04 22:22:21', '青霉素', '中国', '注射', '1');
INSERT INTO `pigeonmedicationmanage` VALUES ('2', '1', '2016-10-18 22:22:59', '红霉素', '美国', '口服', '2');
INSERT INTO `pigeonmedicationmanage` VALUES ('3', '2', '2016-10-12 23:01:16', '路霉素', '朝鲜', '膜拜', '3');
INSERT INTO `pigeonmedicationmanage` VALUES ('4', '2', '2016-10-04 23:01:51', '狗美术', '日本', '吞', '4');
INSERT INTO `pigeonmedicationmanage` VALUES ('5', '2', '2016-10-26 23:02:22', '猫美术', '天朝', '呼吸', '5');

-- ----------------------------
-- Table structure for productionmanage
-- ----------------------------
DROP TABLE IF EXISTS `productionmanage`;
CREATE TABLE `productionmanage` (
  `ID` int(32) NOT NULL AUTO_INCREMENT,
  `鸽子编号` varchar(255) NOT NULL,
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
  `第一次照蛋日期` datetime NOT NULL,
  `第一次照蛋正常蛋数量` int(32) NOT NULL DEFAULT '0',
  `无精蛋` int(32) NOT NULL DEFAULT '0',
  `死精蛋` int(32) NOT NULL DEFAULT '0',
  `第二次照蛋日期` datetime NOT NULL,
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
