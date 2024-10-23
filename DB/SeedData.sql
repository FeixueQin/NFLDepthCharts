-- Seed data for Positions Table
INSERT INTO Positions (name, abbreviation) VALUES 
    ('Left Wide Receiver', 'LWR'),
    ('Right Wide Receiver', 'RWR'),
    ('Slot Receiver', 'SF'),
    ('Left Tackle', 'LT'),
    ('Left Guard', 'LG'),
    ('Center', 'C'),
    ('Right Guard', 'RG'),
    ('Right Tackle', 'RT'),
    ('Tight End', 'TE'),
    ('Quarterback', 'QB'),
    ('Running Back', 'RB');

-- Seed data for Players Table
INSERT INTO Players (playerNumber, name, positionId) VALUES 
    (13, 'Mike Evans', 1),
    (14, 'Jaelon Darden', 1),
    (21, 'Scott Miller', 1),
    (18, 'Tyler Johnson', 2),
    (16, 'Breshad Perriman', 2),
    (15, 'Cyril Grayson', 3),
    (76, 'Donovan Smith', 4),
    (74, 'Ali Marpet', 5),
    (66, 'Ryan Jensen', 6),
    (65, 'Alex Cappa', 7),
    (78, 'Tristan Wirfs', 8),
    (80, 'OJ Howard', 9),
    (84, 'Cameron Brate', 9),
    (87, 'Rob Gronkowski', 9),
    (12, 'Tom Brady', 10),
    (11, 'Blaine Gabbert', 10),
    (2, 'Kyle Trask', 10),
    (7, 'Leonard Fournette', 11),
    (27, 'Ronald Jones II', 11),
    (25, 'Giovani Bernard', 11);
