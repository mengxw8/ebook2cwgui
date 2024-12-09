# 简介
这个版本是基于[ebook2cw](https://fkurz.net/ham/ebook2cw.html)进行的二次开发,感谢DJ5CW (原DJ1YFK)前辈，目前本软件只提供windows中文版本，主要用作CW学习使用。

# 软件截图
## 主界面
![首页](https://github.com/mengxw8/ebook2cwgui/blob/master/doc/img/index.png)
## 汉化仿制的ebook2cwGUI界面
![txt转音频](https://github.com/mengxw8/ebook2cwgui/blob/master/doc/img/Convert.png)
## 抄收练习工具
目前集成了数字，字母，数字字母混合，符号，英文文章，新闻，和专八单词随机练习的功能，后续预计加入Koch练习法
![抄收练习](https://github.com/mengxw8/ebook2cwgui/blob/master/doc/img/copy.png)
## 抄收练习工具
目前还没有头绪，后续再说，目前还在学抄收

### 抄收练习设备
自动键也好，手键也罢，在我看来都是一个开关，控制的是电路的通断信号，我们需要把电路的通断信号，通过一个转换，达到电脑程序能够识别的状态，目前主流的有两种方案，一种是Lakey支持的方案，大致就是将开关信号转化为鼠标的单击信号，通常默认手键是转化为左键单击，如果是自动键的话就转换为左击和右击来区分按下的是哪个。大致释义如下：
![电键原理](https://github.com/mengxw8/ebook2cwgui/blob/master/doc/img/TrainerSchematicDiagram.jpg)

### 抄收练习设备设计
Lakey支持的方案很简单，网上有很多通过改装鼠标来实现的，不过我没那个动手能力，还是算了。我想到一种方案就是利用南京沁恒微电子的`CH552G`芯片来实现对鼠标的模拟，这玩意儿便宜，而且资料丰富,照抄就是。大致原理图如下：
![练习器设计原理图](https://github.com/mengxw8/ebook2cwgui/blob/master/doc/img/schematicDiagram.png)
剩下的事情就交给`嘉立创`就好，嘉立创YYDS,免费打样，再网购点电容电阻焊上,成品效果大概如图:
![成品3D](https://github.com/mengxw8/ebook2cwgui/blob/master/doc/img/schematicDiagram3D.png)
和网上卖的也大差不差的，虽然丑点，功能能够正常实现不就行了吗，毕竟又不是专业的，要求不高。

### 抄收练习设备固件
这个固件好写，非常好写无非就是K1高电平的时候认为鼠标左键被按下，低电平的时候认为鼠标被松开，为了更直观的展示是否按下还是松开，我还特意加了个灯，K1高电平的时候拉高LED—R的电平就是，非常简单，[代码如下](https://github.com/mengxw8/ebook2cwgui/blob/master/firmware/CW/CW.ino)：
```
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

```arduino

编译，烧录，测试，搞定。
当然，如果你制作的和我的一样板子，连编译都省了，直接下载我编译后的[固件](https://github.com/mengxw8/ebook2cwgui/blob/master/firmware/CW/build/CH55xDuino.mcs51.ch552/CW.ino.hex)


# 特别声明
1. 本软件基于开源软件开发，现已全部开源，请遵守相关开源协议使用
2. 本软件无偿提供，其他都是骗子
3. 本人很菜，有bug很正常，有建议或者意见欢迎提issues
4. 如有大佬愿意参与开发，欢迎提PR
5. 别问为啥不XXXX，因为不会