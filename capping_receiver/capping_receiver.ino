#define SOT '<'
#define EOT '>'

bool receiving = false;
String received_data = "";

void setup() {
  Serial.begin(115200);
  pinMode(LED_BUILTIN,OUTPUT);
  pinMode(2,OUTPUT);
  digitalWrite(2,HIGH);

}

void loop() {
  if (Serial.available() > 0) {
    char incoming_char = Serial.read();
    int PIN;
    

    // First we look if we are at the Start of transfer variable
    if (incoming_char == SOT) {
      received_data = "";
      receiving = true;
      
    }
    // we then see if we are at the endof transfer variable
    else if (incoming_char == EOT) {
      receiving = false;
      
      PIN = received_data.toInt();  
      pinMode(PIN,OUTPUT);
      delay(500);
      digitalWrite(PIN,LOW);
      // Process received data
      //Serial.println(PIN);
      delay(500);
      digitalWrite(PIN,HIGH);
      delay(1000);
      
      Serial.println(SOT);
      Serial.println(EOT);
      
    }
    // Finally if we are at the actual package we instead save the received characters
    else if (receiving) {
      received_data += incoming_char;
      
      
    }

  }
}
