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

USE DB_MBG;
GO

DELETE FROM StokKelas
WHERE Kelas NOT IN ('7', '8', '9');
GO

UPDATE StokMBG
SET Jumlah = (SELECT SUM(Jumlah) FROM StokKelas);
GO

USE DB_MBG;
GO

CREATE PROCEDURE sp_ReportPengambilan
    @inKelas VARCHAR(10),
    @inTanggal DATE
AS
BEGIN
    SELECT
        NIS,
        Nama,
        Kelas,
        ISNULL(Alergi, '-') AS Alergi,
        Status
    FROM
        Pengambilan
    WHERE
        Kelas = @inKelas
        AND Tanggal = @inTanggal
    ORDER BY
        Nama;
END
GO

CREATE PROCEDURE sp_ReportMBG
    @inKelas CHAR(1),
    @inTanggal DATE
AS
BEGIN
    SELECT
        p.NIS,
        p.Nama,
        p.Kelas,
        p.Alergi,
        p.Status,
        j.Tanggal,
        j.JamMulai,
        j.JamSelesai
    FROM Pengambilan p
    JOIN Jadwal j ON p.Kelas = j.Kelas
    WHERE p.Kelas = @inKelas
      AND j.Tanggal = @inTanggal
END


ALTER PROCEDURE sp_ReportMBG
    @inKelas CHAR(1),
    @inTanggal DATE
AS
BEGIN
    SELECT
        p.NIS,
        p.Nama,
        p.Kelas,
        p.Alergi,
        p.Status,
        j.Tanggal,
        j.JamMulai,
        j.JamSelesai
    FROM Pengambilan p
    JOIN JadwalPengambilan j ON p.Kelas = j.Kelas
    WHERE p.Kelas = @inKelas
      AND j.Tanggal = @inTanggal
END
SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES

EXEC sp_ReportMBG '9', '2026-05-17'

CREATE TRIGGER trg_KurangiStok
ON Pengambilan
AFTER UPDATE
AS
BEGIN
    -- Cek jika status berubah jadi 'Sudah Diambil'
    IF EXISTS (
        SELECT 1 FROM inserted 
        WHERE Status = 'Sudah Diambil'
    )
    AND EXISTS (
        SELECT 1 FROM deleted 
        WHERE Status = 'Belum Diambil'
    )
    BEGIN
        DECLARE @kelas CHAR(1)
        SELECT @kelas = Kelas FROM inserted

        -- Kurangi stok kelas
        UPDATE StokKelas 
        SET Jumlah = Jumlah - 1 
        WHERE Kelas = @kelas

        -- Update total stok
        UPDATE StokMBG 
        SET Jumlah = (SELECT SUM(Jumlah) FROM StokKelas)
        WHERE ID = 1
    END
	-- Cek stok sebelum
SELECT * FROM StokKelas
SELECT * FROM StokMBG

-- Update status siswa
UPDATE Pengambilan 
SET Status = 'Sudah Diambil' 
WHERE NIS = '2022'

-- Cek stok sesudah
SELECT * FROM StokKelas
SELECT * FROM StokMBG
END

EXEC sp_ReportMBG '9', '2026-06-21'

SELECT * FROM vwJadwal
ALTER PROCEDURE spInsertPengambilan
    @NIS VARCHAR(20),
    @Nama VARCHAR(100),
    @Kelas CHAR(1),
    @Alergi VARCHAR(100),
    @Status VARCHAR(20)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION

        -- Cek apakah NIS sudah ada
        IF EXISTS (SELECT 1 FROM Pengambilan WHERE NIS = @NIS)
        BEGIN
            ROLLBACK TRANSACTION
            RAISERROR('NIS sudah terdaftar!', 16, 1)
            RETURN
        END

        -- Insert data
        INSERT INTO Pengambilan (NIS, Nama, Kelas, Alergi, Status)
        VALUES (@NIS, @Nama, @Kelas, @Alergi, @Status)

        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION
        THROW
    END CATCH
END

DELETE FROM JadwalPengambilan 
WHERE Tanggal < '2026-5-17'
SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'Pengambilan'

UPDATE Pengambilan 
SET Status = 'Belum Diambil',
    Tanggal = NULL,
    Jam = NULL


	ALTER TABLE Pengambilan 
ALTER COLUMN Tanggal DATE NULL

ALTER TABLE Pengambilan 
ALTER COLUMN Jam TIME NULL