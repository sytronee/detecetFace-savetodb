CREATE TABLE IF NOT EXISTS scanneddata (
    id SERIAL PRIMARY KEY,
    cameraid VARCHAR(12),
    detectedtime VARCHAR(20),
    confidence DOUBLE PRECISION,
    bbox_x SMALLINT,
    bbox_y SMALLINT,
    bbox_w SMALLINT,
    bbox_h SMALLINT
);