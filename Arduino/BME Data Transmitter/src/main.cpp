#include <Arduino.h>
#include <Wire.h>
#include <Adafruit_BME280.h>
#include <Adafruit_Sensor.h>
Adafruit_BME280 sensor;

void setup()
{
  sensor.begin(0x76);
  Serial.begin(9600);
}

void loop()
{
  String outputData = String(sensor.readTemperature()) + " " + String(sensor.readHumidity()) + " " +  String(sensor.readPressure() / 133.3224);
  Serial.println(outputData);
  delay(2000);
}









