

int switchState1=0;
int switchState2=0;
int switchState3=0;
bool hasBeenPressed1 = false;
bool hasBeenPressed2 = false;
bool hasBeenPressed3 = false;

void setup() {
  // put your setup code here, to run once:
  pinMode(2,INPUT);
  pinMode(4,INPUT);
  Serial.begin(9600);
  Serial.setTimeout(10);
}

void loop() {
  // put your main code here, to run repeatedly:
  switchState1 = digitalRead(2);
  switchState2 = digitalRead(4);
  switchState3 = digitalRead(7);
  if(switchState1 == HIGH && hasBeenPressed1 == false) {
    hasBeenPressed1 = true;
    Serial.println("sound1");
  } else{
    if(switchState1 == LOW)
    {
      hasBeenPressed1 = false;
    }
  }
  if(switchState2 == HIGH && hasBeenPressed2 == false) {
    hasBeenPressed2 = true;
    Serial.println("sound2");
  } else {
    if(switchState2 == LOW) {
      hasBeenPressed2 = false;
    }
  }
  if(switchState3 == HIGH && hasBeenPressed3 == false) {
    hasBeenPressed3 = true;
    Serial.println("sound3");
  } else {
    if(switchState3 == LOW) {
      hasBeenPressed3 = false;
    }
  }
  
}
