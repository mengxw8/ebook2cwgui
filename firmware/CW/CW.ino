/*
  HID Keyboard mouse combo example


  created 2022
  by Deqing Sun for use with CH55xduino

  This example code is in the public domain.

*/

//For windows user, if you ever played with other HID device with the same PID C55D
//You may need to uninstall the previous driver completely


#ifndef USER_USB_RAM
#error "This example needs to be compiled with a USER USB setting"
#endif

#include "src/userUsbHidKeyboardMouse/USBHIDKeyboardMouse.h"

#define BUTTON1_PIN 32
#define BUTTON2_PIN 14


#define LED_BUILTIN 16

bool button1PressPrev = false;
bool button2PressPrev = false;



void setup() {
  USBInit();
  pinMode(BUTTON1_PIN, INPUT_PULLUP);
  pinMode(BUTTON2_PIN, INPUT_PULLUP);
  pinMode(LED_BUILTIN, OUTPUT);
  digitalWrite(LED_BUILTIN,LOW);
}

void loop() {
//模拟鼠标左键被按下
   bool button1Press = !digitalRead(BUTTON1_PIN);
     if (button1PressPrev != button1Press) {
    button1PressPrev = button1Press;
    if (button1Press) {
      Mouse_press(MOUSE_LEFT);
      //点灯
      digitalWrite(LED_BUILTIN,HIGH);
    }else {
      Mouse_release(MOUSE_LEFT);
      //灭灯
      digitalWrite(LED_BUILTIN,LOW);
    }
  }

  //button 2 is mapped to left click
  bool button2Press = !digitalRead(BUTTON2_PIN);
  if (button2PressPrev != button2Press) {
    button2PressPrev = button2Press;
    if (button2Press) {
      Mouse_press(MOUSE_RIGHT);
    }else {
      Mouse_release(MOUSE_RIGHT);
    }
  }
  delay(1);  //naive debouncing
}
