INSERT INTO Marcas (NomeMarca) VALUES
('Toyota'),
('Honda'),
('Ford'),
('BMW'),
('Mercedes'),
('Volkswagen'),
('Renault'),
('Peugeot'),
('Hyundai'),
('Kia');

INSERT INTO Modelos (Modelo, MarcaID) VALUES
('Corolla', (SELECT MarcaID FROM Marcas WHERE NomeMarca = 'Toyota')),
('Civic', (SELECT MarcaID FROM Marcas WHERE NomeMarca = 'Honda')),
('Focus', (SELECT MarcaID FROM Marcas WHERE NomeMarca = 'Ford')),
('Serie 1', (SELECT MarcaID FROM Marcas WHERE NomeMarca = 'BMW')),
('A180', (SELECT MarcaID FROM Marcas WHERE NomeMarca = 'Mercedes')),
('Golf', (SELECT MarcaID FROM Marcas WHERE NomeMarca = 'Volkswagen')),
('Clio', (SELECT MarcaID FROM Marcas WHERE NomeMarca = 'Renault')),
('208', (SELECT MarcaID FROM Marcas WHERE NomeMarca = 'Peugeot')),
('i20', (SELECT MarcaID FROM Marcas WHERE NomeMarca = 'Hyundai')),
('Rio', (SELECT MarcaID FROM Marcas WHERE NomeMarca = 'Kia'));

INSERT INTO Carros (Ano, Placa, UltimaInspecao, Vendido, ModeloID) VALUES
(2020, 'AAA-0001', '2025-03-10', 0, (SELECT ModeloID FROM Modelos WHERE Modelo = 'Corolla')),
(2019, 'AAA-0002', '2023-11-05', 1, (SELECT ModeloID FROM Modelos WHERE Modelo = 'Civic')),
(2021, 'AAA-0003', '2024-06-12', 0, (SELECT ModeloID FROM Modelos WHERE Modelo = 'Focus')),
(2018, 'AAA-0004', '2023-08-22', 1, (SELECT ModeloID FROM Modelos WHERE Modelo = 'Serie 1')),
(2020, 'AAA-0005', '2024-01-15', 0, (SELECT ModeloID FROM Modelos WHERE Modelo = 'A180')),
(2017, 'AAA-0006', '2022-12-30', 1, (SELECT ModeloID FROM Modelos WHERE Modelo = 'Golf')),
(2022, 'AAA-0007', '2025-04-08', 0, (SELECT ModeloID FROM Modelos WHERE Modelo = 'Clio')),
(2021, 'AAA-0008', '2024-02-19', 0, (SELECT ModeloID FROM Modelos WHERE Modelo = '208')),
(2019, 'AAA-0009', '2023-10-03', 1, (SELECT ModeloID FROM Modelos WHERE Modelo = 'i20')),
(2020, 'AAA-0010', '2024-05-25', 0, (SELECT ModeloID FROM Modelos WHERE Modelo = 'Rio'));

SELECT 
    c.CarroID,
    ma.NomeMarca,
    m.Modelo,
    c.Ano,
    c.Vendido
FROM Carros c
JOIN Modelos m ON c.ModeloID = m.ModeloID
JOIN Marcas ma ON m.MarcaID = ma.MarcaID;