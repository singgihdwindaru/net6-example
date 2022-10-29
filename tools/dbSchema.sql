CREATE TABLE forecast (
    id BIGINT auto_increment NOT NULL,
    date datetime not null,
    temperatureC int NOT NULL,
    temperatureF int NOT NULL,
    summary varchar(250) NULL,
    CONSTRAINT forecast_PK PRIMARY KEY (id)
)