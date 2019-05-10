# Notebook-Windbg

* [Notebook-Windbg](#notebook-windbg)
  * [Meta-Command](#meta-command)
  * [Base](#base)
    * [Module And Symbol](#module-and-symbol)
    * [StackTrace](#stacktrace)
    * [Memory](#memory)
    * [Threads](#threads)
    * [Assembly](#assembly)
    * [Debug](#debug)
  * [Extension](#extension)
    * [SOS](#sos)
    * [SOSEX](#sosex)
  * [Lab](#lab)
  * [Others](#others)

* 元命令 : 元命令以点号 ：`.` 开始,提供调试器和调试会话控制功能
  * 控制调试目标、调试会话或者调试器 .sleep .restart .create .attach
  * 控制模块 .load .loadby .unload .unloadall .chain
  * 远程调试
    * .remote 链接到远程调试服务器
    * .server 启动远程调试服务器
  * 内建的命令程序关键字 .if .else　等
* 基本命令 : 特征是没有特征,提供调试功能,例如 b*(breakpoint) g*(goto) l*(list) k*(stacktrace) p*(step) d*(dmup) e*(edit) ~*(thread) 等
* 扩展命令 : 扩展命令通过 `!` 开始 调用扩展命令方式为 `![扩展名].<扩展命令>` 多数情况下扩展名可以忽略.

## Meta-Command

Meta Command 主要涉及 windbg 相关的命令,用于执行 windbg 本身的一些操作,例如加载扩展,启动远程调试等.

* `.load .loadby .unload .unloadall .chain` : 扩展管理

```shell
.load <path_to_ext/ext.dll> # 加载扩展
.loadby <ext> <module name> # 根据 module 路径加载扩展
.chain # 列出已经加载的扩展
```

* `.reload` : 重新加载模块

个人理解这个命令应该放在基础命令中,跟模块相关

* `.symfix` `.sympath` : 会话符号文件配置
* `.time` `.ttime` : 执行时间,线程时间
* `.eventlog` : 显示新的 Microsoft Win32 调试事件,如模块加载,进程创建和终止和线程创建和终止
* `.help` 帮助

## Base

Base Command 用于执行调试相关操作.

* `?` 帮助

### Module And Symbol

* `lm` : [列出模块](https://docs.microsoft.com/zh-cn/windows-hardware/drivers/debugger/lm--list-loaded-modules-)
  * `o` : 仅显示加载的模块
  * `l` : 仅显示加载符号信息的模块
  * `v` : 将导致显示详细信息
  * `m <pattern>` : 匹配 pattern
* `x <module>!<symbol pattern>` : 查找符号
* `ld` : 加载符号

### StackTrace

* `k` StackTrace

```shell
[~Thread] k [b|p|P|v] [c] [n] [f] [L] [M] [FrameCount]
[~Thread] k [b|p|P|v] [c] [n] [f] [L] [M] = BasePtr [FrameCount]
[~Thread] k [b|p|P|v] [c] [n] [f] [L] [M] = BasePtr StackPtr InstructionPtr
[~Thread] kd [WordCount]
```

### Memory

* `d[type] [<range>] [/c column] [Lcount]` : 显示内存
* `e[type] <address> <value>` : 编辑内存
* `s` : [搜索内存](https://docs.microsoft.com/zh-cn/windows-hardware/drivers/debugger/s--search-memory-)
* `r` : 设置寄存器的值 `r eax = @ebx`

[伪寄存器](https://docs.microsoft.com/zh-cn/windows-hardware/drivers/debugger/pseudo-register-syntax)

### Threads

* `~` : 线程操作

```shell
# `[tid]` : `# 当前`
~ # 列出线程
~ [tid] e <command string> # 线程上执行命令
~ [f|u|n|m] # 冻结 激活 挂起 恢复
```

* `|` 进程操作

```shell
| # 列出当前调试的进程信息
| pid s # 切换调试的进程
```

* `||` : 系统操作

### Assembly

* `a` : 汇编
* `u` : 反汇编

### Debug

* `b` : 断点管理
  * `ba` : 数据断点
  * `b<c|d|e> <bps>` : clear/disable/enable breakpoint(s)
  * `b<p|u|m>` : create breakpoints
    * `bp <address>` : 根据地址设置断点
    * `bm <module>!<symbol pattern>` : 根据符号设置断点
  * `bl` : list breakpoints
* `g` : 前进 `F5`
* `gu` : step out
* `p[a|c|ct|h|t]` : 步进[step over]`[地址|调用|调用或return|分支|return]`
* `t[a|c|ct|h|t]` : 执行[step into]`[地址|调用|调用或return|分支|return]`

p t  的区别是 p 会将 call methodxxx 作为一条指令, t 会跟踪到 method 方法内部. 当没有

## Extension

扩展命令通过扩展插件,增加更多的命令供执行,可以通过 `.load`/`.loadby` 加载扩展.下面介绍一些比较常用的扩展.

### SOS

### SOSEX

## Lab

## Others

* [Dump File](https://docs.microsoft.com/zh-cn/windows-hardware/drivers/debugger/user-mode-dump-files)