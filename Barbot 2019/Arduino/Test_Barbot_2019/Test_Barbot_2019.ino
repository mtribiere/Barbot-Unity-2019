void setup() {
  Serial.begin(9600);

}

void loop() {

 delay(15000);
 Serial.println("!P?");
 delay(10000);
 Serial.println("!R1?");
 delay(10000);
 Serial.println("!D?");

}
