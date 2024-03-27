USE master
CREATE DATABASE HappyPetDb
    ON (FILENAME = 'C:\Users\Hp\source\repos\DBSD_Final\DBSD_Final\AppData\HappyPetDb.mdf'),   
       (FILENAME = 'C:\Users\Hp\source\repos\DBSD_Final\DBSD_Final\AppData\HappyPetDb.ldf')   
    FOR ATTACH
    
--shrink DB log file
USE HappyPetDb
--select * FROM sys.database_files
ALTER DATABASE HappyPetDb SET RECOVERY SIMPLE
GO
DBCC SHRINKFILE (HappyPetDb_log, 7)