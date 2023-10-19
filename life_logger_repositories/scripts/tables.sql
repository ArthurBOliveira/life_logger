CREATE TABLE Activity (
    Id CHAR(36) PRIMARY KEY,
    Date DATETIME,
    Active TINYINT(1),

    IdType CHAR(36),
    IdLabel CHAR(36),
    Details TEXT
);