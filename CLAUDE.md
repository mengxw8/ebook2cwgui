# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## 构建与运行

```bash
# 构建（Debug）
dotnet build

# 发布独立可执行文件（Release, win-x64, 无 .NET 依赖）
dotnet publish -c Release -r win-x64 --self-contained

# 快速发布（同 build.bat）
dotnet publish -c Release -r win-x64 --self-contained
```

项目基于 .NET 8 + Windows Forms，需要 Windows 平台编译和运行。无测试项目。

## 技术栈

- **GUI**: Windows Forms (.NET 8 `net8.0-windows`)
- **音频**: NAudio（实时合成/播放）+ NAudio.Lame（MP3 编码）
- **数据库**: SQLite，通过 SqlSugarCoreNoDrive ORM 访问（`db/CW.db`）
- **ORM 实体**: `db/entity/` 下，使用 `[SugarTable]` / `[SugarColumn]` 特性标注
- **外部依赖**: 调用 `ebook2cw.exe`（开源莫尔斯电码引擎）生成音频
- **网络**: AngleSharp 解析 HTML，用于抓取 RSS 新闻

## 架构概览

### 入口与导航
`Program.cs` → `Form1.cs` — 主窗口，所有功能通过按钮触发并弹出对应的子窗口（`ShowDialog()`）。

### 核心模块（根目录）
| 文件 | 职责 |
|---|---|
| `Constant.cs` | 所有莫尔斯码映射表（字母/数字/短码/符号/Koch 教程）、文件路径常量 |
| `MorseConfig.cs` (morse/) | 速度配置，用法：`MorseConfig.Create(speed)` 按 Paris 标准计算 di/da/间隔 |
| `MorsePlayer.cs` (morse/) | 实时音频合成引擎，继承 `WaveProvider16`，使用 `ConcurrentQueue<short>` 作为音频缓冲区，支持淡入淡出 |
| `MorseToMp3.cs` (morse/) | 将文本直接生成 MP3 + SRT 字幕文件（无需 ebook2cw.exe） |
| `Mp3Player.cs` (morse/) | 简单 MP3 文件播放/暂停/停止封装 |
| `CWTools.cs` (tools/) | 调用外部 `ebook2cw.exe` CLI 生成音频 |

### 数据库（db/）
- `SqliteUtil.cs` — `SqlSugarClient` 工厂，连接字符串指向 `./db/CW.db`
- `entity/Abbreviations.cs` — 简语表
- `entity/ChineseCode.cs` — 中文电码表
- `entity/Words.cs` — 单词表（含 A-Z 各字母计数字段）

### 枚举
- `WorkingMode.cs` — 练习模式：Number / Alphabet / AlphabetAndNumber / Symbol / Article / News / Word / Customize / Koch / ShortNumber5 / ShortNumber10
- `KeyType.cs` — 键类型：Ordinary（手键）/ Auto（自动键）

### 功能窗口
- `ArticleConvert` — 文本转莫尔斯音频（仿 ebook2cw GUI）
- `CopyingPractice` — 抄收练习（随机生成电码音频，用户听写）
- `SendPractice` — 拍发练习（跟随背景音拍发，通过 CH552G 硬件模拟鼠标输入）
- `NumberCopyingPractice` — 数字专项抄收
- `ChineseCodeQuickQuery` — 中文电码快查
- `AbbreviationQuickSearch` — 简语快查
- `Player` — 音频播放器
- `EncodingConfiguration` — 编码配置
- `AnswerBoard` — 答案展示面板

### 固件（firmware/）
基于 CH552G 芯片的 Arduino 项目，将电键开关信号转为 USB HID 鼠标事件（手键→左键，自动键→左/右键）。固件已预编译到 `firmware/CW/build/`。

### 数据目录
- `text/` — 内置练习文章（.txt）
- `word/Level8.json` — 专八单词数据
- `db/CW.db` — SQLite 数据库（随构建复制到输出目录）

## 关键开发约定

- .NET 8，启用 nullable 和 implicit usings，允许 unsafe 代码块
- 嵌入字体 `Resources/consola.ttf` 作为内嵌资源
- `ebook2cw.exe`、`db/CW.db`、`text/`、`word/` 目录在发布时复制到输出目录
- 没有单元测试——所有验证通过手动运行 GUI 完成
- 路径使用相对路径（`./text/`、`./db/`），依赖 `Environment.CurrentDirectory`
