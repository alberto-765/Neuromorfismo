-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Versión del servidor:         11.3.0-MariaDB - mariadb.org binary distribution
-- SO del servidor:              Win64
-- HeidiSQL Versión:             12.3.0.6589
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Volcando estructura de base de datos para webmedicina2
CREATE DATABASE IF NOT EXISTS `webmedicina2` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci */;
USE `webmedicina2`;

-- Volcando estructura para tabla webmedicina2.aspnetroleclaims
CREATE TABLE IF NOT EXISTS `aspnetroleclaims` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `RoleId` varchar(255) NOT NULL,
  `ClaimType` longtext DEFAULT NULL,
  `ClaimValue` longtext DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetRoleClaims_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Volcando datos para la tabla webmedicina2.aspnetroleclaims: ~0 rows (aproximadamente)

-- Volcando estructura para tabla webmedicina2.aspnetroles
CREATE TABLE IF NOT EXISTS `aspnetroles` (
  `Id` varchar(255) NOT NULL,
  `Name` varchar(256) DEFAULT NULL,
  `NormalizedName` varchar(256) DEFAULT NULL,
  `ConcurrencyStamp` longtext DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `RoleNameIndex` (`NormalizedName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Volcando datos para la tabla webmedicina2.aspnetroles: ~3 rows (aproximadamente)
INSERT INTO `aspnetroles` (`Id`, `Name`, `NormalizedName`, `ConcurrencyStamp`) VALUES
	('1', 'superAdmin', 'SUPERADMIN', 'fc7d6ce0-c792-480e-a577-e08b7ebcb723'),
	('2', 'admin', 'ADMIN', '152f9d7f-3d79-4fc8-a860-5262f7cee601'),
	('3', 'medico', 'MEDICO', '30c9c83b-950b-4269-9d4c-c6037a33d888');

-- Volcando estructura para tabla webmedicina2.aspnetuserclaims
CREATE TABLE IF NOT EXISTS `aspnetuserclaims` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` varchar(255) NOT NULL,
  `ClaimType` longtext DEFAULT NULL,
  `ClaimValue` longtext DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetUserClaims_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Volcando datos para la tabla webmedicina2.aspnetuserclaims: ~0 rows (aproximadamente)

-- Volcando estructura para tabla webmedicina2.aspnetuserlogins
CREATE TABLE IF NOT EXISTS `aspnetuserlogins` (
  `LoginProvider` varchar(255) NOT NULL,
  `ProviderKey` varchar(255) NOT NULL,
  `ProviderDisplayName` longtext DEFAULT NULL,
  `UserId` varchar(255) NOT NULL,
  PRIMARY KEY (`LoginProvider`,`ProviderKey`),
  KEY `IX_AspNetUserLogins_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Volcando datos para la tabla webmedicina2.aspnetuserlogins: ~0 rows (aproximadamente)

-- Volcando estructura para tabla webmedicina2.aspnetuserroles
CREATE TABLE IF NOT EXISTS `aspnetuserroles` (
  `UserId` varchar(255) NOT NULL,
  `RoleId` varchar(255) NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IX_AspNetUserRoles_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Volcando datos para la tabla webmedicina2.aspnetuserroles: ~1 rows (aproximadamente)
INSERT INTO `aspnetuserroles` (`UserId`, `RoleId`) VALUES
	('56f3a65b-4c05-4c41-a3f8-bb10ca34be20', '1');

-- Volcando estructura para tabla webmedicina2.aspnetusers
CREATE TABLE IF NOT EXISTS `aspnetusers` (
  `Id` varchar(255) NOT NULL,
  `UserName` varchar(256) DEFAULT NULL,
  `NormalizedUserName` varchar(256) DEFAULT NULL,
  `Email` varchar(256) DEFAULT NULL,
  `NormalizedEmail` varchar(256) DEFAULT NULL,
  `EmailConfirmed` tinyint(1) NOT NULL,
  `PasswordHash` longtext DEFAULT NULL,
  `SecurityStamp` longtext DEFAULT NULL,
  `ConcurrencyStamp` longtext DEFAULT NULL,
  `PhoneNumber` longtext DEFAULT NULL,
  `PhoneNumberConfirmed` tinyint(1) NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL,
  `LockoutEnd` datetime(6) DEFAULT NULL,
  `LockoutEnabled` tinyint(1) NOT NULL,
  `AccessFailedCount` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
  KEY `EmailIndex` (`NormalizedEmail`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Volcando datos para la tabla webmedicina2.aspnetusers: ~1 rows (aproximadamente)
INSERT INTO `aspnetusers` (`Id`, `UserName`, `NormalizedUserName`, `Email`, `NormalizedEmail`, `EmailConfirmed`, `PasswordHash`, `SecurityStamp`, `ConcurrencyStamp`, `PhoneNumber`, `PhoneNumberConfirmed`, `TwoFactorEnabled`, `LockoutEnd`, `LockoutEnabled`, `AccessFailedCount`) VALUES
	('56f3a65b-4c05-4c41-a3f8-bb10ca34be20', 'string', 'STRING', NULL, NULL, 0, 'AQAAAAIAAYagAAAAEM8wKAB/Ze+/a5XKLlRTfx97kF6azWlQfZjU+cqWm0sIb2MdECCJ4lbkrpTkleSlwA==', 'JIU4A5M3PSKWEZQNF67OOUDEN46QDK4Y', '8ae5fc12-fe75-4d75-a93e-535cab6da104', NULL, 0, 0, NULL, 1, 0);

-- Volcando estructura para tabla webmedicina2.aspnetusertokens
CREATE TABLE IF NOT EXISTS `aspnetusertokens` (
  `UserId` varchar(255) NOT NULL,
  `LoginProvider` varchar(255) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `Value` longtext DEFAULT NULL,
  PRIMARY KEY (`UserId`,`LoginProvider`,`Name`),
  CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Volcando datos para la tabla webmedicina2.aspnetusertokens: ~0 rows (aproximadamente)

-- Volcando estructura para tabla webmedicina2.epilepsias
CREATE TABLE IF NOT EXISTS `epilepsias` (
  `idEpilepsia` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(50) NOT NULL DEFAULT '',
  `FechaCreac` datetime(6) NOT NULL,
  `FechaUltMod` datetime(6) NOT NULL,
  PRIMARY KEY (`idEpilepsia`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Volcando datos para la tabla webmedicina2.epilepsias: ~2 rows (aproximadamente)
INSERT INTO `epilepsias` (`idEpilepsia`, `nombre`, `FechaCreac`, `FechaUltMod`) VALUES
	(1, 'Epilepsia1', '2024-01-03 00:00:00.000000', '2024-01-03 00:00:00.000000'),
	(2, 'Epilepsia2', '2024-01-03 00:00:00.000000', '2024-01-03 00:00:00.000000');

-- Volcando estructura para tabla webmedicina2.etapalt
CREATE TABLE IF NOT EXISTS `etapalt` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(50) NOT NULL,
  `Titulo` varchar(50) NOT NULL,
  `Label` varchar(50) NOT NULL,
  `MedicoCreadorIdMedico` int(11) NOT NULL,
  `MedicoUltModifIdMedico` int(11) NOT NULL,
  `FechaCreac` datetime(6) NOT NULL,
  `FechaUltMod` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_EtapaLT_MedicoCreadorIdMedico` (`MedicoCreadorIdMedico`),
  KEY `IX_EtapaLT_MedicoUltModifIdMedico` (`MedicoUltModifIdMedico`),
  CONSTRAINT `FK_EtapaLT_Medicos_MedicoCreadorIdMedico` FOREIGN KEY (`MedicoCreadorIdMedico`) REFERENCES `medicos` (`idMedico`) ON DELETE CASCADE,
  CONSTRAINT `FK_EtapaLT_Medicos_MedicoUltModifIdMedico` FOREIGN KEY (`MedicoUltModifIdMedico`) REFERENCES `medicos` (`idMedico`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Volcando datos para la tabla webmedicina2.etapalt: ~0 rows (aproximadamente)

-- Volcando estructura para tabla webmedicina2.evolucionlt
CREATE TABLE IF NOT EXISTS `evolucionlt` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Confirmado` tinyint(1) NOT NULL,
  `Fecha` datetime(6) NOT NULL,
  `MedicoUltModifIdMedico` int(11) NOT NULL,
  `EtapasLTId` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_EvolucionLT_EtapasLTId` (`EtapasLTId`),
  KEY `IX_EvolucionLT_MedicoUltModifIdMedico` (`MedicoUltModifIdMedico`),
  CONSTRAINT `FK_EvolucionLT_EtapaLT_EtapasLTId` FOREIGN KEY (`EtapasLTId`) REFERENCES `etapalt` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_EvolucionLT_Medicos_MedicoUltModifIdMedico` FOREIGN KEY (`MedicoUltModifIdMedico`) REFERENCES `medicos` (`idMedico`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Volcando datos para la tabla webmedicina2.evolucionlt: ~0 rows (aproximadamente)

-- Volcando estructura para tabla webmedicina2.farmacos
CREATE TABLE IF NOT EXISTS `farmacos` (
  `idFarmaco` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(50) NOT NULL DEFAULT '',
  `FechaCreac` datetime(6) NOT NULL,
  `FechaUltMod` datetime(6) NOT NULL,
  PRIMARY KEY (`idFarmaco`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Volcando datos para la tabla webmedicina2.farmacos: ~0 rows (aproximadamente)

-- Volcando estructura para tabla webmedicina2.medicos
CREATE TABLE IF NOT EXISTS `medicos` (
  `idMedico` int(11) NOT NULL AUTO_INCREMENT,
  `userLogin` varchar(50) NOT NULL,
  `nombre` varchar(50) NOT NULL,
  `apellidos` varchar(50) NOT NULL,
  `fechaNac` datetime NOT NULL,
  `sexo` varchar(1) NOT NULL DEFAULT '',
  `netuserId` varchar(255) NOT NULL,
  `FechaCreac` datetime(6) NOT NULL,
  `FechaUltMod` datetime(6) NOT NULL,
  PRIMARY KEY (`idMedico`),
  UNIQUE KEY `userLogin` (`userLogin`),
  KEY `Índice 2` (`netuserId`),
  CONSTRAINT `FK_medicos_aspnetusers` FOREIGN KEY (`netuserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Volcando datos para la tabla webmedicina2.medicos: ~1 rows (aproximadamente)
INSERT INTO `medicos` (`idMedico`, `userLogin`, `nombre`, `apellidos`, `fechaNac`, `sexo`, `netuserId`, `FechaCreac`, `FechaUltMod`) VALUES
	(1, 'string', 'Alberto', 'Mimbrero Gutiérrez', '2024-01-03 01:15:26', 'H', '56f3a65b-4c05-4c41-a3f8-bb10ca34be20', '2024-01-03 01:15:26.413000', '2024-01-03 01:15:26.413000');

-- Volcando estructura para tabla webmedicina2.medicospacientes
CREATE TABLE IF NOT EXISTS `medicospacientes` (
  `idMedPac` int(11) NOT NULL AUTO_INCREMENT,
  `idMedico` int(11) NOT NULL,
  `idPaciente` int(11) NOT NULL,
  PRIMARY KEY (`idMedPac`),
  KEY `FK_medicospacientes_pacientes` (`idPaciente`),
  KEY `idUsuario_idPaciente` (`idMedico`,`idPaciente`),
  CONSTRAINT `FK_medicospacientes_medicos` FOREIGN KEY (`idMedico`) REFERENCES `medicos` (`idMedico`) ON DELETE CASCADE,
  CONSTRAINT `FK_medicospacientes_pacientes` FOREIGN KEY (`idPaciente`) REFERENCES `pacientes` (`idPaciente`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci COMMENT='Relacion de que medicos pueden editar que pacientes';

-- Volcando datos para la tabla webmedicina2.medicospacientes: ~7 rows (aproximadamente)
INSERT INTO `medicospacientes` (`idMedPac`, `idMedico`, `idPaciente`) VALUES
	(1, 1, 2),
	(2, 1, 3),
	(3, 1, 4),
	(4, 1, 5),
	(5, 1, 6),
	(6, 1, 7),
	(7, 1, 8);

-- Volcando estructura para tabla webmedicina2.mutaciones
CREATE TABLE IF NOT EXISTS `mutaciones` (
  `idMutacion` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(50) NOT NULL DEFAULT '',
  `FechaCreac` datetime(6) NOT NULL,
  `FechaUltMod` datetime(6) NOT NULL,
  PRIMARY KEY (`idMutacion`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Volcando datos para la tabla webmedicina2.mutaciones: ~2 rows (aproximadamente)
INSERT INTO `mutaciones` (`idMutacion`, `nombre`, `FechaCreac`, `FechaUltMod`) VALUES
	(1, 'Mutacion1', '2024-01-03 00:00:00.000000', '2024-01-03 00:00:00.000000'),
	(2, 'Mutacion2', '2024-01-03 00:00:00.000000', '2024-01-03 00:00:00.000000');

-- Volcando estructura para tabla webmedicina2.pacientes
CREATE TABLE IF NOT EXISTS `pacientes` (
  `idPaciente` int(11) NOT NULL AUTO_INCREMENT,
  `NumHistoria` varchar(50) NOT NULL,
  `fechaNac` datetime NOT NULL DEFAULT curdate(),
  `sexo` enum('H','M') NOT NULL DEFAULT 'H',
  `talla` int(11) NOT NULL,
  `fechaDiagnostico` datetime NOT NULL DEFAULT curdate(),
  `fechaFractalidad` datetime NOT NULL DEFAULT curdate(),
  `farmaco` varchar(250) NOT NULL,
  `idEpilepsia` int(11) DEFAULT NULL,
  `idMutacion` int(11) DEFAULT NULL,
  `enfermRaras` varchar(1) NOT NULL DEFAULT '',
  `descripEnferRaras` text NOT NULL,
  `medicoUltMod` int(11) NOT NULL,
  `medicoCreador` int(11) NOT NULL,
  `FechaCreac` datetime(6) NOT NULL,
  `FechaUltMod` datetime(6) NOT NULL,
  `UltimaEtapaId` int(11) DEFAULT NULL,
  PRIMARY KEY (`idPaciente`),
  KEY `idMutacion` (`idMutacion`),
  KEY `idTipoEpilepsia` (`idEpilepsia`),
  KEY `medicoCreador` (`medicoCreador`),
  KEY `medicoUltMod` (`medicoUltMod`),
  KEY `IX_Pacientes_UltimaEtapaId` (`UltimaEtapaId`),
  CONSTRAINT `FK_Pacientes_EtapaLT_UltimaEtapaId` FOREIGN KEY (`UltimaEtapaId`) REFERENCES `etapalt` (`Id`),
  CONSTRAINT `FK_pacientes_epilepsias` FOREIGN KEY (`idEpilepsia`) REFERENCES `epilepsias` (`idEpilepsia`) ON DELETE SET NULL,
  CONSTRAINT `FK_pacientes_medicos` FOREIGN KEY (`medicoUltMod`) REFERENCES `medicos` (`idMedico`),
  CONSTRAINT `FK_pacientes_medicos_2` FOREIGN KEY (`medicoCreador`) REFERENCES `medicos` (`idMedico`),
  CONSTRAINT `FK_pacientes_mutaciones` FOREIGN KEY (`idMutacion`) REFERENCES `mutaciones` (`idMutacion`) ON DELETE SET NULL
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Volcando datos para la tabla webmedicina2.pacientes: ~7 rows (aproximadamente)
INSERT INTO `pacientes` (`idPaciente`, `NumHistoria`, `fechaNac`, `sexo`, `talla`, `fechaDiagnostico`, `fechaFractalidad`, `farmaco`, `idEpilepsia`, `idMutacion`, `enfermRaras`, `descripEnferRaras`, `medicoUltMod`, `medicoCreador`, `FechaCreac`, `FechaUltMod`, `UltimaEtapaId`) VALUES
	(2, 'AN1111111111', '2006-01-03 02:16:18', 'M', 114, '2024-01-03 00:00:00', '2024-01-03 00:00:00', 'SDFASF', 2, 2, 'S', 'ADFSDFSDF\n', 1, 1, '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000', NULL),
	(3, 'AN1111111112', '2006-01-03 02:17:54', 'H', 117, '2024-01-03 00:00:00', '2024-01-03 00:00:00', 'dfsdf', 2, 2, 'N', '', 1, 1, '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000', NULL),
	(4, 'AN1111111116', '2006-01-03 02:24:04', 'H', 92, '2024-01-03 00:00:00', '2024-01-03 00:00:00', '', NULL, 1, 'N', '', 1, 1, '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000', NULL),
	(5, 'AN5555555555', '2006-01-03 02:26:52', 'H', 132, '2024-01-03 00:00:00', '2024-01-03 00:00:00', '', NULL, 1, 'N', '', 1, 1, '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000', NULL),
	(6, 'AN9999999999', '2006-01-03 02:29:46', 'H', 97, '2024-01-03 00:00:00', '2024-01-03 00:00:00', '', NULL, 1, 'N', '', 1, 1, '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000', NULL),
	(7, 'AN7777777777', '2006-01-03 02:35:46', 'M', 50, '2024-01-03 00:00:00', '2024-01-03 00:00:00', '', NULL, 1, 'N', '', 1, 1, '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000', NULL),
	(8, 'AN7777777779', '2006-01-03 13:17:46', 'H', 50, '2024-01-03 00:00:00', '2024-01-03 00:00:00', 'XFG', 1, 2, 'N', '', 1, 1, '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000', NULL);

-- Volcando estructura para tabla webmedicina2.__efmigrationshistory
CREATE TABLE IF NOT EXISTS `__efmigrationshistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Volcando datos para la tabla webmedicina2.__efmigrationshistory: ~8 rows (aproximadamente)
INSERT INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
	('20231231170324_InicialMigration', '7.0.11'),
	('20231231170550_CheckConcurrencia', '7.0.11'),
	('20231231174507_SeedMutacionYEpilepsia2', '7.0.11'),
	('20240102011947_SSL', '7.0.11'),
	('20240103125331_CambiosNombres', '7.0.11'),
	('20240103125604_CambiosNombres2', '7.0.11'),
	('20240103154027_EliminacionTablaDatosEtapa', '7.0.11'),
	('20240103154333_CambioNombreCOlumnaUltimaEtapa', '7.0.11');

-- Volcando estructura para disparador webmedicina2.pacientes_after_insert
SET @OLDTMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION';
DELIMITER //
CREATE TRIGGER `pacientes_after_insert` AFTER INSERT ON `pacientes` FOR EACH ROW BEGIN
	INSERT INTO medicospacientes(IdMedico, IdPaciente) VALUES (NEW.medicoCreador, NEW.idPaciente);
END//
DELIMITER ;
SET SQL_MODE=@OLDTMP_SQL_MODE;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
