# 简介
本软件是基于[ebook2cw](https://fkurz.net/ham/ebook2cw.html)进行的二次开发,感谢DJ5CW (原DJ1YFK)前辈，目前本软件只提供windows中文版本，主要用作CW学习使用。
主要功能：
1、音频转换：把文本文件按照自己的需要转换成各种速率的莫尔斯电码音频文件，方便自己随时练习
2、抄收练习：随机生成自己所需的各种莫尔斯电码音频，可对速度，内容进行自定义
3、拍发练习：跟随背景音和内容进行莫尔斯电码拍发练习

# 软件截图
## 主界面
这个主要以类似工具箱的形式，把各种杂七杂八的功能杂糅在一起，可以按照自己所需要的功能选择使用
![首页](https://github.com/mengxw8/ebook2cwgui/blob/master/doc/img/index.png)
## 汉化仿制的ebook2cwGUI界面
![txt转音频](https://github.com/mengxw8/ebook2cwgui/blob/master/doc/img/Convert.png)
## 抄收练习工具
目前集成了数字，字母，数字字母混合，符号，英文文章，新闻，和专八单词随机练习，Koch练习法等功能。量大管饱！
![抄收练习](https://github.com/mengxw8/ebook2cwgui/blob/master/doc/img/copy.png)
## 发报练习工具
目前借鉴了一下Lakey的模式，结合金山打字的练习方法，通过可视化加背景音的方式方便跟练，跟随电脑报进行拍发练习，既容易保证内容的完整性，又容易养成良好的大小间隔，有助于更好的掌握时值关系。
![发报练习](https://github.com/mengxw8/ebook2cwgui/blob/master/doc/img/send.png)

## 中文编码速查
目前有些友台在使用下来，有把中文解释为数字的需求，特此开发了此功能，方便相互转换
![中文编码速查](https://github.com/mengxw8/ebook2cwgui/blob/master/doc/img/ChineseQuery.png)

## 摩尔斯电码播放器
目前主要的功能就是实现英文和数字的自定义编码播放，可以自定义任何编码，支持把自定义编码的内容导出为音频文件。目前暂时不支持播放中文和特殊的符号
![播放器](https://github.com/mengxw8/ebook2cwgui/blob/master/doc/img/player.png)



### 发报练习设备
自动键也好，手键也罢，在我看来都是一个开关，控制的是电路的通断信号，我们需要把电路的通断信号，通过一个转换，达到电脑程序能够识别的状态，目前主流的有两种方案，一种是Lakey支持的方案，大致就是将开关信号转化为鼠标的单击信号，通常默认手键是转化为左键单击，如果是自动键的话就转换为左击和右击来区分按下的是哪个。大致释义如下：
![电键原理](https://github.com/mengxw8/ebook2cwgui/blob/master/doc/img/TrainerSchematicDiagram.jpg)

### 抄收练习设备设计
Lakey支持的方案很简单，网上有很多通过改装鼠标来实现的，不过我没那个动手能力，还是算了。我想到一种方案就是利用南京沁恒微电子的`CH552G`芯片来实现对鼠标的模拟，这玩意儿便宜，本身就是适合拿来当做鼠标芯片用，而且资料丰富,照抄就是。大致原理图如下：
![练习器设计原理图](https://github.com/mengxw8/ebook2cwgui/blob/master/doc/img/schematicDiagram.png)
剩下的事情就交给`嘉立创`就好，嘉立创YYDS,免费打样，再网购点电容电阻焊上,成品效果大概如图:
![成品3D](https://github.com/mengxw8/ebook2cwgui/blob/master/doc/img/schematicDiagram3D.png)
和网上卖的也大差不差的，虽然丑点，功能能够正常实现不就行了吗，毕竟又不是专业的，要求不高。

### 抄收练习设备固件
这个固件好写，非常好写无非就是K1高电平的时候认为鼠标左键被按下，低电平的时候认为鼠标被松开，为了更直观的展示是否按下还是松开，我还特意加了个灯，K1高电平的时候拉高LED—R的电平就是，非常简单，C语言开发环境太复杂了，根本用不上，直接上arduino就行，[代码如下](https://github.com/mengxw8/ebook2cwgui/blob/master/firmware/CW/CW.ino)：
```arduino
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

```

编译，烧录，测试，搞定。
当然，如果你制作的和我的一样板子，连编译都省了，直接下载我编译后的[固件](https://gitee.com/mengxw8/ebook2cwgui/blob/master/firmware/CW/build/CH55xDuino.mcs51.ch552/CW.ino.hex)

此时，把练习器插进电脑，然后把鼠标移动到发报按钮上，操作手键，电脑上的软件就会开始动作了。


# 特别声明
1. 本软件基于开源软件开发，现已全部开源，请遵守相关开源协议使用
2. 本软件无偿提供，其他都是骗子
3. 本人很菜，有bug很正常，有建议或者意见欢迎提issues
4. 如有大佬愿意参与开发，欢迎提PR
5. 别问为啥不XXXX，因为不会
6. 为啥和XXXX有点像，嘘……，抄的。
7. AI真好用，AI真牛逼

## 当前版本补充

### 新增界面功能

- 支持浅色、深色、高对比度和跟随系统四种主题。
- 主界面支持 Ctrl+1、Ctrl+2、Ctrl+3 切换主题，Ctrl+P 打开摩尔斯播放器。
- 播放器支持 Enter 开始、Space 暂停/继续、R 重播、S 打开编码设置、Esc 停止。
- 所有窗体已整理 TabIndex，按 Tab 时按从上到下、同一行从左到右选择控件。

### 播放器信道模拟

播放器的“噪声设置”支持启用/关闭噪声，并可调整信噪比、信道模式和模拟强度：

- 白噪声：模拟接收机底噪。
- 衰落信道：模拟短波信号强弱变化。
- 脉冲干扰：模拟突发性电磁干扰。
- 多径回声：模拟多径传播造成的回波。

信道模拟会同时作用于实时播放；导出 MP3 时也会使用当前噪声和音调设置。

### 多路播放

多路播放支持主声部和辅助声部，每个声部可以设置文本、速度、频率、音量、循环和静音，适合叠加背景音、节拍或干扰声进行训练。

### 构建和运行

项目需要 Windows、Visual Studio 2022 或 .NET 8 SDK。构建命令：

    dotnet build
    dotnet publish -c Release -r win-x64 --self-contained

发布目录需要保留 ebook2cw.exe、db/CW.db、text/ 和 word/Level8.json。新闻练习需要网络连接，其他内置练习和播放器功能可以离线使用。提交修改后建议执行：

    dotnet build --no-restore

### 项目目录

- morse/：摩尔斯配置、实时播放、信道模拟和音频导出。
- db/：SQLite 数据库和实体定义。
- text/：内置文章练习文本。
- word/：单词练习数据。
- firmware/：CH552G USB HID 固件和编译产物。
- tools/：内容生成、文章处理和 ebook2cw 调用。

问题反馈请提交 [Issues](https://github.com/mengxw8/ebook2cwgui/issues)。


## v1.0.0.5 之后的版本记录

以下内容按 Git 标签和后续提交整理，旧版本的功能介绍和硬件说明保持不变。

### v1.0.0.5

- 增加自定义编码设置，支持自定义报头、报尾和报文。
- 增加文本播放、重播、暂停、静音和额外词间隔设置。
- 支持数字短码练习和数字短码音频导出。
- 增加中文电码、业余无线电简语查询和一行一行字幕导出。
- 改进播放器初始化、速度计算、时间序列和文件切换逻辑。

### v1.0.0.6

- 优化中文电码快查和查询历史。
- 改进自定义编码立即生效、重播和停止逻辑。
- 增加波形选择，支持正弦波、锯齿波和方波。
- 修复数字短码、字幕时间、文章换行和音频生成相关问题。

### v1.0.0.7

- 增加播放报文时的文本高亮和校对体验。
- 改进字幕生成和换行兼容性。
- 优化抄收练习的内容选择、布局和停止逻辑。

### v1.0.0.8

- 增加实时播放、抄收练习、数字短码和导出音频的噪声功能。
- 支持在导出音频时保留噪声效果。
- 修复波形选择不生效、信噪比档位音量过小等问题。

### v1.0.0.9

- 增加启动时检查 GitHub Release 新版本。
- 增加多路摩尔斯播放，支持主声部、辅助声部、循环、静音及独立参数。
- 增加多路混合音频导出。
- 增加多路播放中的随机报文生成。
- 改进分屏抄收布局、数字短码分屏和窗口尺寸。
- GitHub Actions 支持自动构建并上传 framework-dependent 与 self-contained 安装包。

### v1.0.0.9 之后

- 修复莫尔斯点划和字符间隔的时值计算，统一实时播放、导出音频和字幕的节奏。
- 改进新闻请求、RSS 解析、数据库路径和 ebook2cw 外部进程错误处理。
- 增加浅色、深色、高对比度和跟随系统主题。
- 增加主窗口和播放器快捷键，并完善所有窗口的 Tab 键顺序。
- 播放器增加白噪声、衰落信道、脉冲干扰、多径回声等信道模拟，以及可调信道强度。
- 修复主界面主题控件布局，主题设置改为 Visual Studio WinForms 设计器可编辑的控件。
- 更新 README，补充当前功能、快捷键、构建和目录说明。
