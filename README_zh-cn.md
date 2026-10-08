# ebook2cwgui

Windows 桌面端摩尔斯电码（CW）学习与练习工具，基于 .NET 8 WinForms、NAudio 和 ebook2cw。

## 功能

### 练习

- 抄收练习：数字、字母、字母数字混合、符号、英文文章、新闻、单词和 Koch 课程。
- 数字专项练习：普通数字组、短码 5、短码 10。
- 拍发练习：跟随背景音和参考报文练习发送，支持手键、自动键及 CH552G USB HID 设备。
- 文章和新闻练习：使用 text/ 内置文章，或从 RSS 获取新闻。

### 音频

- 文章转音频：调用 ebook2cw，设置速度、频率、波形和输出格式。
- 摩尔斯播放器：播放文本文件，支持正弦波、锯齿波、方波、速度、频率、自定义编码、MP3 和 SRT 导出。
- 多路播放：支持主报文和辅助声部、循环、静音、独立速度、频率和音量。
- 信道模拟：可开关并调节信噪比、模式和强度，包括白噪声、衰落信道、脉冲干扰和多径回声。
- 中文电码、业余无线电简语查询。

### 界面操作

- 主题：浅色、深色、高对比度、跟随系统。
- 主窗口：Ctrl+1 浅色，Ctrl+2 深色，Ctrl+3 高对比度，Ctrl+P 打开播放器。
- 播放器：Enter 开始，Space 暂停/继续，R 重播，S 或 Esc 停止。
- 各窗体已设置 Tab 顺序，按 Tab 时按从上到下、同一行从左到右导航。

## 安装和运行

直接运行发布目录中的 CW.exe。运行目录需要包含 ebook2cw.exe、db/CW.db、text/ 和 word/Level8.json。新闻练习需要网络，其他内置练习可以离线使用。

## 开发和构建

环境要求：Windows、Visual Studio 2022 或 .NET 8 SDK。

    dotnet build
    dotnet publish -c Release -r win-x64 --self-contained

推送 v* 标签后，GitHub Actions 会构建 framework-dependent 和 self-contained 两个 win-x64 压缩包。提交前至少执行：

    dotnet build --no-restore

## 目录

| 路径 | 说明 |
|---|---|
| morse/ | 摩尔斯配置、实时播放、信道模拟和音频导出 |
| db/ | SQLite 数据库和实体 |
| text/ | 内置文章 |
| word/ | 单词数据 |
| firmware/ | CH552G USB HID 固件 |
| tools/ | 内容生成、文章处理和 ebook2cw 调用 |
| Resources/ | 内嵌字体等资源 |

## 硬件练习器

firmware/CW 包含 CH552G USB HID 固件示例：手键可映射为鼠标左键，自动键使用左右键区分输入。预编译文件位于对应的 build 目录，烧录前请确认硬件连接和芯片型号。

## 许可证和反馈

本项目基于开源软件二次开发，具体许可见 LICENSE.txt。感谢 ebook2cw 及相关开源项目作者。问题请提交 Issues：https://github.com/mengxw8/ebook2cwgui/issues。