CREATE DATABASE DB_MBG;
USE DB_MBG;

CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username VARCHAR(30),
    Pass VARCHAR(30),
    RoleUser VARCHAR(20)
);

INSERT INTO Users (Username, Pass, RoleUser)
VALUES 
('admin', '123', 'Admin'),
('petugas', '123', 'Petugas'),
('siswa', '123', 'Siswa');

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