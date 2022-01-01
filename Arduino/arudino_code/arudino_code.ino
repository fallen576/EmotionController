#include <LiquidCrystal.h>

LiquidCrystal lcd = LiquidCrystal(2, 3, 4, 5, 6, 7);
const int RED_PIN = 9;
const int GREEN_PIN = 10;
const int BLUE_PIN = 11;
int DISPLAY_TIME = 50;
String colorInput;

void setup()
{
  lcd.begin(16, 2);
  pinMode(RED_PIN, OUTPUT);
  pinMode(GREEN_PIN, OUTPUT);
  pinMode(BLUE_PIN, OUTPUT);
  Serial.begin(9600);
}

void serialEvent() {
  colorInput = String(Serial.readString());

  String data = "EsadV.9090EangryV.3875EhappyV.20948302";
  String emot = getValue(getValue(colorInput, 'E', 1), 'V', 0);
  String val = getValue(getValue(colorInput, 'E', 1), 'V', 1);
  String emot2 = getValue(getValue(colorInput, 'E', 2), 'V', 0);
  String val2 = getValue(getValue(colorInput, 'E', 2), 'V', 1);
  write(emot + " " + val, emot2 + " " + val2);
  
  if (emot == "angry"){
    //write("red");
    digitalWrite(RED_PIN, HIGH);
    digitalWrite(GREEN_PIN, LOW);
    digitalWrite(BLUE_PIN, LOW);
  }
  else if (emot == "happy") {
    //write("gren");
    digitalWrite(RED_PIN, LOW);
    digitalWrite(GREEN_PIN, HIGH);
    digitalWrite(BLUE_PIN, LOW);
  }
  else if (emot == "sad") {
    //write("blue");
    digitalWrite(RED_PIN, LOW);
    digitalWrite(GREEN_PIN, LOW);
    digitalWrite(BLUE_PIN, HIGH);
  }
  else {
    //write("neutral");
    showSpectrum();
  }
  
  delay(5000);
  digitalWrite(RED_PIN, LOW);
  digitalWrite(GREEN_PIN, LOW);
  digitalWrite(BLUE_PIN, LOW);
}

String getValue(String data, char separator, int index)
{
    int found = 0;
    int strIndex[] = { 0, -1 };
    int maxIndex = data.length() - 1;

    for (int i = 0; i <= maxIndex && found <= index; i++) {
        if (data.charAt(i) == separator || i == maxIndex) {
            found++;
            strIndex[0] = strIndex[1] + 1;
            strIndex[1] = (i == maxIndex) ? i+1 : i;
        }
    }
    return found > index ? data.substring(strIndex[0], strIndex[1]) : "";
}

void write(String msg1, String msg2) {
  lcd.setCursor(0, 0);
  lcd.print(msg1);
  lcd.setCursor(0, 1);
  lcd.print(msg2);
}

void loop()
{
  //lol
}

void showSpectrum()
{
  int x;

  for (x = 0; x < 768; x++)
  {
    showRGB(x);
    delay(10);
  }
}

void showRGB(int color)
{
  int redIntensity;
  int greenIntensity;
  int blueIntensity;

  if (color <= 255)   
  {
    redIntensity = 255 - color;   
    greenIntensity = color;   
    blueIntensity = 0;          
  }
  else if (color <= 511)     
  {
    redIntensity = 0;      
    greenIntensity = 255 - (color - 256);
    blueIntensity = (color - 256);     
  }
  else
  {
    redIntensity = (color - 512);
    greenIntensity = 0;                   
    blueIntensity = 255 - (color - 512);  
  }

  analogWrite(RED_PIN, redIntensity);
  analogWrite(BLUE_PIN, blueIntensity);
  analogWrite(GREEN_PIN, greenIntensity);
}
