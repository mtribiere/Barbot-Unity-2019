void setup() {
  Serial.begin(9600);

}

void loop() {

 delay(10000);
 Serial.println("!P?");
 delay(10000);
 Serial.println("!R2?");
 delay(10000);
 Serial.println("!D?");

}
