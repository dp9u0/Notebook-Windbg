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
  * [中断](#%E4%B8%AD%E6%96%AD)
  * [Lab](#lab)
    * [Hang](#hang)
    * [Crash](#crash)
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
* `.shell [Options] [ShellCommand]` : 执行外部命令 .shell -ci "!eestack" grep Monitor.Enter

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
  * `s [-[[Flags]Type]] Range Pattern` : 按指定类型格式搜索数据 `s-u 0000020044434700 L1000 "Application"`
  * `s -[[Flags]]<sa|su> Range` : 搜索  ASCII / UNICODE `s-[l5]su 0000020044434700 L1000`
  * `s -[[Flags]]v Range Object` : 搜索数据结构
* `r` : 设置/获取寄存器的值 `r eax` 表达式中使用寄存器 `@ebx`
  * [寄存器](https://docs.microsoft.com/zh-cn/windows-hardware/drivers/debugger/register-syntax)
  * [伪寄存器](https://docs.microsoft.com/zh-cn/windows-hardware/drivers/debugger/pseudo-register-syntax)

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

* `sx*` : 通过 sx [管理事件中断](https://docs.microsoft.com/zh-cn/windows-hardware/drivers/debugger/sx--sxd--sxe--sxi--sxn--sxr--sx---set-exceptions-),中断事件
列表查看[控制异常和事件](https://docs.microsoft.com/zh-cn/windows-hardware/drivers/debugger/controlling-exceptions-and-events)

例如当创建线程时,输出 `sxn ct`

## Extension

扩展命令通过扩展插件,增加更多的命令供执行,可以通过 `.load`/`.loadby` 加载扩展.下面介绍一些比较常用的扩展.

* `!ext.analyze` : analyze
* `!ext.gle [-all]` : 显示最后一个错误
* `!ext.for_each_<frame|function|local|module|register>` : for example `!for_each_register -c:!address ${@#RegisterValue}`
* `!ext.peb` `!ext.teb`
* `!runaway [Flags]` : 显示线程时间

### SOS

### SOSEX

## 中断

* `sx*` : 通过 sx [管理事件中断](https://docs.microsoft.com/zh-cn/windows-hardware/drivers/debugger/sx--sxd--sxe--sxi--sxn--sxr--sx---set-exceptions-),中断事件
列表查看[控制异常和事件](https://docs.microsoft.com/zh-cn/windows-hardware/drivers/debugger/controlling-exceptions-and-events)

1. 例如当创建线程时,输出 `sxn ct`
2. 例如发生 Access Violation(First Chance) 时 打印 `rcx` : `sxe -c '.echo @rcx' av`

## Lab

使用 VS2019 打开解决方案 `./CrashMe.sln`,运行项目.

### Hang

控制台

```shell
./run.bat
adplus –hang –pn dotnet.exe
# >命令行输入 hang 创建hang线程
```

cdb 执行 :

```shell
.loadby sos coreclr # 加载 sos 插件
~* kL # 输出堆栈
!syncblk
.shell -ci "!eestack" grep MonEnter
```

### Crash

```shell
./run.bat
adplus -crash -pn dotnet.exe
# >命令行输入 ex f 程序抛出异常
```

```shell
cdb -z <dumpFile>
!pe
!u <IP shown in the exceptionstack> # 反汇编抛出异常位置附近代码
# 输出类似下面
# 00007ffb`2aa6529e 33d2            xor     edx,edx
# >>> 00007ffb`2aa652a0 3909        cmp     dword ptr [rcx],ecx
# 00007ffb`2aa652a2 e819a1fc43      call    System_Private_CoreLib!System.String.Equals(System.String)$##60002F9 (00007ffb`6ea2f3c0)
```

`rcx` 处记录`` `ptr [rcx]` 会引发 av( Access Violation) 中断,CLR借此抛出异常

可以 cdb直接attach 到进程.通过 `sxn -c '.echo @rcx' av`设置av中断并打印寄存器值,然后CrashMe 中触发 Finalize 抛出NullReferenceException.

## Others

* [Dump File](https://docs.microsoft.com/zh-cn/windows-hardware/drivers/debugger/user-mode-dump-files)