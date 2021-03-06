USE Geography;
GO

-- Task 1	V

SELECT PeakName
FROM Peaks
ORDER BY PeakName

-- Task 2	V



-- Task 3	V (Broken)

SELECT 
	CountryName, 
	CountryCode, 
	(case CurrencyCode when 'EUR' then 'Euro' else 'Not Euro' end) [Currency]
FROM Countries
ORDER BY CountryName ASC

-- Task 4	V

SELECT c.CountryName [Country Name], c.IsoCode [ISO Code]
FROM Countries c
WHERE c.CountryName LIKE '%A%A%A%'
ORDER BY c.IsoCode

-- Task 5	V

SELECT p.PeakName, m.MountainRange [Mountain], p.Elevation
FROM Peaks p
INNER JOIN Mountains m
	ON m.Id = p.MountainId
ORDER BY p.Elevation DESC, p.PeakName ASC
 
-- Task 6	V

SELECT p.PeakName, m.MountainRange [Mountain], c.CountryName, con.ContinentName
FROM Peaks p
INNER JOIN Mountains m
	ON m.Id = p.MountainId
INNER JOIN MountainsCountries mc
	ON mc.MountainId = m.Id
INNER JOIN Countries c
	ON c.CountryCode = mc.CountryCode
INNER JOIN Continents con
	ON con.ContinentCode = c.ContinentCode
ORDER BY p.PeakName ASC, c.CountryName ASC

-- Task 7	V

SELECT r.RiverName [River], COUNT(c.CountryCode) [Countries Count]
FROM Rivers r
INNER JOIN CountriesRivers cr
	ON cr.RiverId = r.Id
INNER JOIN Countries c
	ON c.CountryCode = cr.CountryCode
GROUP BY r.RiverName
HAVING COUNT(c.CountryCode) > 2

-- Task 8	V

SELECT 
	MAX(p.Elevation) [MaxElevation], 
	MIN(p.Elevation) [MinElevation], 
	AVG(p.Elevation) [AverageElevation]
From Peaks p

-- Task 9	V (Broken)

SELECT 
	c.CountryName, 
	con.ContinentName, 
	COUNT(r.Id) [RiversCount], 
	ISNULL(SUM(r.Length), 0) [TotalLength]
FROM Countries c
LEFT JOIN Continents con
	ON con.ContinentCode = c.ContinentCode
LEFT JOIN CountriesRivers cr
	ON cr.CountryCode = c.CountryCode
LEFT JOIN Rivers r
	ON r.Id = cr.RiverId
GROUP BY c.CountryName, con.ContinentName
ORDER BY 
	[RiversCount] DESC, 
	[TotalLength] DESC, 
	c.CountryName ASC

-- Task 10	V

SELECT cu.CurrencyCode, cu.Description [Currency], COUNT(c.CountryCode) [NumberOfCountries]
FROM Currencies cu
LEFT JOIN Countries c
	ON c.CurrencyCode = cu.CurrencyCode
GROUP BY cu.CurrencyCode, cu.Description
ORDER BY COUNT(c.CountryCode) DESC, cu.Description ASC

-- Task 11	V

SELECT 
	con.ContinentName,
	SUM(c.AreaInSqKm) [CountriesArea],
	SUM(CAST(c.Population as numeric(18,0))) [CountriesPopulation]
FROM Continents con
LEFT JOIN Countries c
	ON con.ContinentCode = c.ContinentCode
GROUP BY con.ContinentName
ORDER BY CountriesPopulation DESC

-- Task 12	V (Broken)

SELECT 
	c.CountryName, 
	MAX(p.Elevation) [HighestPeakElevation],
	MAX(r.Length) [LongestRiverLength]
FROM Countries c
LEFT JOIN MountainsCountries mc
	ON c.CountryCode = mc.CountryCode
LEFT JOIN Mountains m
	ON mc.MountainId = m.Id
LEFT JOIN Peaks p
	ON m.Id = p.MountainId
LEFT JOIN CountriesRivers cr
	ON  c.CountryCode = cr.CountryCode
LEFT JOIN Rivers r
	ON cr.RiverId = r.Id 
GROUP BY c.CountryName
ORDER BY 
	HighestPeakElevation DESC, 
	LongestRiverLength DESC, 
	c.CountryName ASC

-- Task 13	V

SELECT 
	p.PeakName, 
	r.RiverName, 
	LOWER(p.PeakName) + LOWER(RIGHT(r.RiverName, LEN(r.RiverName ) - 1)) [Mix]
FROM Peaks p, Rivers r
WHERE LOWER(RIGHT(p.PeakName, 1)) = LOWER(LEFT(r.RiverName, 1))
ORDER BY Mix


-- Task 14	V (Broken)

SELECT 
	c.CountryName [Country],
	p.PeakName [Highest Peak Name],
	p.Elevation [Highest Peak Elevation],
	m.MountainRange [Mountain]
FROM Countries c
LEFT JOIN MountainsCountries mc
	ON mc.CountryCode = c.CountryCode
LEFT JOIN Mountains m
	ON m.Id = mc.MountainId
LEFT JOIN Peaks p
	ON p.MountainId = m.Id
WHERE 
	p.Elevation = (
		SELECT MAX(p.Elevation)
		FROM Peaks p
		LEFT JOIN Mountains m	
			ON p.MountainId = m.Id
		LEFT JOIN MountainsCountries mc
			ON m.Id = mc.MountainId
		WHERE c.CountryCode = mc.CountryCode) 
UNION
SELECT 
	c.CountryName [Country],
	'(no highest peak)' [Highest Peak Name],
	0 [Highest Peak Elevation],
	'(no mountain)' [Mountain]
FROM Countries c
LEFT JOIN MountainsCountries mc
	ON mc.CountryCode = c.CountryCode
LEFT JOIN Mountains m
	ON m.Id = mc.MountainId
LEFT JOIN Peaks p
	ON p.MountainId = m.Id
WHERE 
	(SELECT MAX(p.Elevation)
	FROM Peaks p
	LEFT JOIN Mountains m	
		ON p.MountainId = m.Id
	LEFT JOIN MountainsCountries mc
		ON m.Id = mc.MountainId
	WHERE c.CountryCode = mc.CountryCode) IS NULL
ORDER BY c.CountryName ASC, [Highest Peak Name] ASC

-- Task 15	V (Broken)

-- 1
CREATE TABLE Monasteries(
	Id int PRIMARY KEY IDENTITY not null,
	Name nvarchar(50) not null,
	CountryCode char(2) FOREIGN KEY REFERENCES Countries(CountryCode) not null
)

-- 2
INSERT INTO Monasteries(Name, CountryCode) VALUES
('Rila Monastery “St. Ivan of Rila”', 'BG'), 
('Bachkovo Monastery “Virgin Mary”', 'BG'),
('Troyan Monastery “Holy Mother''s Assumption”', 'BG'),
('Kopan Monastery', 'NP'),
('Thrangu Tashi Yangtse Monastery', 'NP'),
('Shechen Tennyi Dargyeling Monastery', 'NP'),
('Benchen Monastery', 'NP'),
('Southern Shaolin Monastery', 'CN'),
('Dabei Monastery', 'CN'),
('Wa Sau Toi', 'CN'),
('Lhunshigyia Monastery', 'CN'),
('Rakya Monastery', 'CN'),
('Monasteries of Meteora', 'GR'),
('The Holy Monastery of Stavronikita', 'GR'),
('Taung Kalat Monastery', 'MM'),
('Pa-Auk Forest Monastery', 'MM'),
('Taktsang Palphug Monastery', 'BT'),
('Sümela Monastery', 'TR')

-- 3
ALTER TABLE Countries
ADD IsDeleted varchar(5) not null default 'false' 

-- 4
UPDATE Countries
SET IsDeleted = 'true'
WHERE CountryCode IN (
	SELECT CountryCode
	FROM CountriesRivers
	GROUP BY CountryCode
	HAVING COUNT(RiverId) > 3
)

-- 5
SELECT m.Name [Monastery], c.CountryName [Country]
FROM Monasteries m
LEFT JOIN Countries c
	ON c.CountryCode = m.CountryCode
WHERE c.IsDeleted = 'false'
ORDER BY m.Name

-- Task 16	V

-- 1
UPDATE Countries
SET CountryName = 'Burma'
WHERE CountryName = 'Myanmar'

-- 2
INSERT INTO Monasteries
VALUES ('Hanga Abbey', (SELECT CountryCode FROM Countries WHERE CountryName = 'Tanzania'))

-- 3
INSERT INTO Monasteries
VALUES ('Myin-Tin-Daik', (SELECT CountryCode FROM Countries WHERE CountryName = 'Maynmar'))

-- 4
SELECT con.ContinentName, c.CountryName, COUNT(m.Name) [MonasteriesCount]
FROM Continents con
LEFt JOIN Countries c
	ON c.ContinentCode = con.ContinentCode
LEFT JOIN Monasteries m
	ON m.CountryCode = c.CountryCode
WHERE c.IsDeleted = 'false'
GROUP BY con.ContinentName, c.CountryName
ORDER BY MonasteriesCount DESC, c.CountryName ASC

-- Task 17	V

IF OBJECT_ID('fn_MountainsPeaksJSON') IS NOT NULL
  DROP FUNCTION fn_MountainsPeaksJSON
GO

CREATE  FUNCTION [dbo].[fn_MountainsPeaksJSON]()
RETURNS nvarchar(max)
AS
BEGIN

	DECLARE cu CURSOR FOR
		SELECT m.MountainRange [Mountain], p.PeakName [Peak], p.Elevation
		FROM Mountains m
		LEFT JOIN Peaks p
			ON p.MountainId = m.Id
		--ORDER BY m.MountainRange ASC, p.PeakName ASC

	DECLARE 
		@output nvarchar(max) = '{"mountains":[',
		@mName nvarchar(50), 
		@pName nvarchar(50), 
		@pElevation int,
		@lastMountain nvarchar(50) = '1'

	OPEN cu

	FETCH NEXT FROM cu 
	INTO @mName, @pName, @pElevation

	WHILE @@FETCH_STATUS = 0
	BEGIN
		--PRINT @output

		IF @mName = @lastMountain
		BEGIN
			SET @output = @output + ',{"name":"' + @pName + '","elevation":' + CAST(@pElevation as varchar(5)) + '}'
		END
		ELSE
		BEGIN
			IF @lastMountain = '1'
				SET @output = @output + '{"name":"' + @mName + '","peaks":[{"name":"' + @pName + '","elevation":' + CAST(@pElevation as varchar(5)) + '}'
			ELSE
				IF @pName IS NULL
					SET @output = @output + ']},{"name":"' + @mName + '","peaks":['
				ELSE
					SET @output = @output + ']},{"name":"' + @mName + '","peaks":[{"name":"' + @pName + '","elevation":' +  CAST(@pElevation as varchar(5)) + '}'
		END
		
		SET @lastMountain = @mName
		
		FETCH NEXT FROM cu 
		INTO @mName, @pName, @pElevation
	END

	CLOSE cu
	DEALLOCATE cu

	SET @output = @output + ']}]}'
	--PRINT @output

	RETURN @output
END
GO


SELECT dbo.fn_MountainsPeaksJSON()



