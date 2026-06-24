CREATE VIEW vwPengambilan
AS
SELECT
    ID,
    NIS,
    Nama,
    Kelas,
    Alergi,
    Tanggal,
    Jam,
    Status
FROM Pengambilan

CREATE VIEW vwJadwal
AS
SELECT
    ID,
    Kelas,
    Tanggal,
    JamMulai,
    JamSelesai
FROM JadwalPengambilan

CREATE VIEW vwStokKelas
AS
SELECT
    ID,
    Kelas,
    Jumlah
FROM StokKelas

CREATE PROCEDURE spInsertPengambilan
    @NIS VARCHAR(50),
    @Nama VARCHAR(100),
    @Kelas VARCHAR(10),
    @Alergi VARCHAR(100),
    @Status VARCHAR(50)
AS
BEGIN
    INSERT INTO Pengambilan
    (
        NIS,
        Nama,
        Kelas,
        Alergi,
        Tanggal,
        Jam,
        Status
    )
    VALUES
    (
        @NIS,
        @Nama,
        @Kelas,
        @Alergi,
        GETDATE(),
        CONVERT(TIME, GETDATE()),
        @Status
    )
END

CREATE PROCEDURE spUpdatePengambilan
    @NIS VARCHAR(50),
    @Nama VARCHAR(100),
    @Kelas VARCHAR(10),
    @Alergi VARCHAR(100),
    @Status VARCHAR(50)
AS
BEGIN
    UPDATE Pengambilan
    SET
        Nama=@Nama,
        Kelas=@Kelas,
        Alergi=@Alergi,
        Status=@Status
    WHERE NIS=@NIS
END

CREATE PROCEDURE spDeletePengambilan
    @NIS VARCHAR(50)
AS
BEGIN
    DELETE FROM Pengambilan
    WHERE NIS=@NIS
END

CREATE PROCEDURE spSearchPengambilan
    @NIS VARCHAR(50)
AS
BEGIN
    IF EXISTS
    (
        SELECT *
        FROM Pengambilan
        WHERE NIS=@NIS
    )
    BEGIN
        SELECT *
        FROM Pengambilan
        WHERE NIS=@NIS
    END
    ELSE
    BEGIN
        PRINT 'Data tidak ditemukan'
    END
END


CREATE PROCEDURE spInsertJadwal
    @Kelas VARCHAR(10),
    @Tanggal DATE,
    @JamMulai TIME,
    @JamSelesai TIME
AS
BEGIN
    INSERT INTO JadwalPengambilan
    (
        Kelas,
        Tanggal,
        JamMulai,
        JamSelesai
    )
    VALUES
    (
        @Kelas,
        @Tanggal,
        @JamMulai,
        @JamSelesai
    )
END

CREATE PROCEDURE spUpdateJadwal
    @Kelas VARCHAR(10),
    @Tanggal DATE,
    @JamMulai TIME,
    @JamSelesai TIME
AS
BEGIN
    UPDATE JadwalPengambilan
    SET
        JamMulai=@JamMulai,
        JamSelesai=@JamSelesai
    WHERE
        Kelas=@Kelas
        AND Tanggal=@Tanggal
END

CREATE PROCEDURE spDeleteJadwal
    @Kelas VARCHAR(10),
    @Tanggal DATE
AS
BEGIN
    DELETE FROM JadwalPengambilan
    WHERE
        Kelas=@Kelas
        AND Tanggal=@Tanggal
END

CREATE TABLE StokKelas
(
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Kelas VARCHAR(20),
    Jumlah INT
)

INSERT INTO StokKelas VALUES ('7A', 100)
INSERT INTO StokKelas VALUES ('7B', 90)
INSERT INTO StokKelas VALUES ('8A', 80)
INSERT INTO StokKelas VALUES ('8B', 75)

select * from StokKelas

CREATE DATABASE DB_MBG;
USE DB_MBG;
CREATE TABLE Admin (
    id_admin INT PRIMARY KEY IDENTITY(1,1),
    username VARCHAR(50) NOT NULL,
    password VARCHAR(255) NOT NULL,
    nama_admin VARCHAR(100) NOT NULL
);
CREATE TABLE Siswa (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    NIS VARCHAR(20) NOT NULL UNIQUE,
    Nama VARCHAR(50) NOT NULL,
    Kelas VARCHAR(20),
    Alergi VARCHAR(20)
);
CREATE TABLE Pengambilan (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    NIS VARCHAR(20),
    Nama VARCHAR(100),
    Kelas VARCHAR(50),
    Tanggal DATE,
    Jam TIME,
    Status VARCHAR(50)
);
CREATE TABLE Petugas_Piket (
    id_petugas INT PRIMARY KEY IDENTITY(1,1),
    username VARCHAR(50) NOT NULL,
    password VARCHAR(255) NOT NULL,
    nama_petugas VARCHAR(100) NOT NULL
);
INSERT INTO Pengambilan (NIS, Nama, Kelas, Tanggal, Jam, Status)
VALUES ('12345', 'Budi', 'XI RPL', '2026-04-17', '12:30:00', 'Sudah Diambil');
ALTER TABLE Pengambilan
ADD TanggalAmbil DATE;

ALTER TABLE Pengambilan
ADD JamAmbil TIME;
INSERT INTO Pengambilan (NIS, StatusAmbil)
VALUES 
('2024001', 'Belum'),
('2024002', 'Belum'),
('2024003', 'Belum');


INSERT INTO Siswa (NIS, Nama, Kelas, Alergi)
VALUES 
('2024001', 'Budi Santoso', 'X RPL 1', 'Tidak Alergi'),
('2024002', 'Siti Aminah', 'X RPL 2', 'Alergi'),
('2024003', 'Andi Pratama', 'XI RPL 1', 'Tidak Alergi'),
('2024004', 'Dewi Lestari', 'XI RPL 2', 'Alergi'),
('2024005', 'Rizky Maulana', 'XII RPL 1', 'Tidak Alergi');

select * from Siswa
INSERT INTO Users (Username, Pass, RoleUser)
VALUES 
('admin', '123', 'Admin'),
('petugas', '123', 'Petugas'),
('siswa', '123', 'Siswa');
SELECT 
    S.NIS,
    S.Nama,
    S.Kelas,
    S.Alergi,
    P.StatusAmbil
FROM Siswa S
LEFT JOIN Pengambilan P ON S.NIS = P.NIS

select * from Siswa

select * from Pengambilan

SELECT 
S.NIS,
S.Nama,
S.Kelas,
S.Alergi,
ISNULL(P.StatusAmbil, 'Belum Ada') AS StatusAmbil
FROM Siswa S
LEFT JOIN Pengambilan P ON S.NIS = P.NIS
WHERE S.NIS = '';

CREATE TABLE Admin (
    id_admin INT PRIMARY KEY IDENTITY(1,1),
    username VARCHAR(50) NOT NULL,
    password VARCHAR(255) NOT NULL,
    nama_admin VARCHAR(100) NOT NULL
);


SELECT * FROM Pengambilan

SELECT * FROM JadwalPengambilan

select * from StokKelas

DELETE FROM StokKelas
WHERE ID='4'

INSERT INTO StokKelas VALUES
('7',100),
('8',100),
('9',100)

select * from Siswa
from Siswa

SELECT NIS,Nama,Kelas FROM Pengambilan

SELECT * FROM JadwalPengambilan

SELECT NIS,Nama,Kelas FROM Pengambilan

UPDATE StokKelas SET Jumlah=100

UPDATE Pengambilan
SET Status='Belum Diambil'