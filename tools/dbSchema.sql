create database weather;
use weather;
CREATE TABLE forecast (
    id BIGINT auto_increment NOT NULL,
    date timestamp not null,
    temperatureC int NOT NULL,
    temperatureF int NOT NULL,
    summary varchar(250) NULL,
    CONSTRAINT forecast_PK PRIMARY KEY (id)
);

INSERT INTO weather.forecast (`date`, summary, temperatureC, temperatureF) values
(NOW(),'Freezing',27, 32 + (27 DIV 0.5556)),
(NOW(),'Mild',-9, 32 + (-9 DIV 0.5556)),
(NOW(),'Freezing',21, 32 + (21 DIV 0.5556)),
(NOW(),'Freezing',-17, 32 + (-17 DIV 0.5556));