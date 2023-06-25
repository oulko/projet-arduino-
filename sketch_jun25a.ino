#include <kxtj3-1057.h> // bibliotheque de l'accelerometre integré a la carte RFthings UCA
float previousAcceleration = 0.0;
char data_rx; //data recue par visual studio



void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600) ;
  
}




void loop() {
  int axeX = analogRead(A3)+6 ;
  int axeY = analogRead(A4);
  int axeZ = analogRead(A5);
  Serial.print(axeX);
  Serial.print(",");
  Serial.print(axeY);
  Serial.print(",");
  Serial.print(axeZ);

  // Calcul de l'accélération générale
  float acceleration = sqrt(pow(axeX, 2) + pow(axeY, 2) + pow(axeZ, 2));

  // Vérification de l'accélération générale
  if (abs(acceleration - previousAcceleration) > 150) 
  {
    Serial.print("Intrusion détectée! ");
  }
  Serial.print(" | Accélération générale: ");
  Serial.println(acceleration);


  // Mise à jour de l'accélération précédente
  previousAcceleration = acceleration;

  data_rx = Serial.read(); //lis le data de C#

  delay(1);
}
