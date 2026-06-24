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
select * from S
