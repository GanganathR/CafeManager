-- ========================================
-- CafeManager - Seed Database Script
-- ========================================

-- Create Cafes table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Cafes' AND xtype='U')
BEGIN
    CREATE TABLE Cafes (
        Id UNIQUEIDENTIFIER PRIMARY KEY,
        Name NVARCHAR(100) NOT NULL,
        Description NVARCHAR(256) NOT NULL,
        Location NVARCHAR(100) NOT NULL
    );
END;

-- Create Employees table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Employees' AND xtype='U')
BEGIN
    CREATE TABLE Employees (
        Id NVARCHAR(20) PRIMARY KEY, -- Format UIXXXXXXX
        Name NVARCHAR(100) NOT NULL,
        EmailAddress NVARCHAR(150) NOT NULL,
        PhoneNumber NVARCHAR(20) NOT NULL,
        Gender NVARCHAR(10) NOT NULL,
        StartDate DATE NOT NULL,
        CafeId UNIQUEIDENTIFIER NULL,
        FOREIGN KEY (CafeId) REFERENCES Cafes(Id)
    );
END;

-- Seed Cafes


-- ======================
-- Seed Cafes
-- ======================
INSERT INTO Cafes (Id, Name, Description, Location)
VALUES
('41421601-41DE-4458-9983-FC50D4DF8D25', 'Latte House', 'Cosy cafe for friends', 'Singapore'),
('f7153ca4-4f9e-49cd-8e50-7a3517e210c0', 'Brew & Bite', 'Quick bites and coffee', 'Penang'),
('fe8a0296-342b-4cc4-b7ce-a1001996eb5b', 'Cafe Mocha', 'Best coffee in town', 'Singapore'),
('84c5c2db-9b35-4be5-92dc-1234567890ab', 'caf123', 'caf123456', 'Malaysia'),
('7781ac7a-4506-4399-831d-1234567890cd', 'Test Cafe', 'Test Cafe Desc', 'Penang');

-- ======================
-- Seed Employees
-- ======================
INSERT INTO Employees (Id, Name, EmailAddress, PhoneNumber, Gender, StartDate, CafeId)
VALUES
('UI0000001', 'Alice Tan', 'alice@example.com', '91234567', 'Female', '2025-08-19', '41421601-41DE-4458-9983-FC50D4DF8D25'),
('UI0000002', 'John Lim', 'john@example.com', '98765432', 'Male', '2023-06-15', '41421601-41DE-4458-9983-FC50D4DF8D25'),
('UI1294567', 'Test User', 'testmail@fd.lc', '84776003', 'Male', '2025-04-08', 'f7153ca4-4f9e-49cd-8e50-7a3517e210c0'),
('UI1234567', 'Nimal Per', 't@mail.com', '82345678', 'Male', '2025-07-03', NULL), -- Employee without assigned cafe
('UI0B48E4C', 'user02', 'l@mail.com', '85772204', 'Male', '2025-07-27', 'f7153ca4-4f9e-49cd-8e50-7a3517e210c0'),
('UIBF98BBA', 'User01', 's@mail.com', '8566005', 'Male', '2025-08-14', 'fe8a0296-342b-4cc4-b7ce-a1001996eb5b');
