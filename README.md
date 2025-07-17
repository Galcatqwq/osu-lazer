[![Yukikoi Melt](https://images2.imgbox.com/0b/df/66PTpxUz_o.png)](https://t.me/Yukikoi_Melt)

<h3 align="center">实验性的自定义osu!(Yukikoi-lazer)</h3>

<h3 align="center">节奏只需*点击一下*即可！</h3>

<p align="center">
<img width="500" alt="osu! logo" src="assets/lazer.png">
</p>

<h3 align="center">好大一个osu(</h3>


## 构建

#### 通过 IDE 构建

打开项目时选择所需平台的 `.slnf` 文件（而不是 `.sln` 文件）加载解决方案, 这可以减少依赖项加载并隐藏其他平台的资源文件.

预设的 `.slnf` 文件：

`osu.Desktop.slnf`(桌面端(Macos/Windows/Linux))

`osu.Android.slnf`(Android)

`osu.iOS.slnf`(某高端会员制全拟真液态UI操作系统)

构建项目时预先切换解决方案配置从`Debug`(默认)至`Release` ,`Debug`仅适用于调试(事实上我已经把osu.Game.Tests杨了所以也没什么可以调试的).

如果你之前没有编译过移动端项目, 可能需要预先安装 Android 工具链( IOS 我懒得写,自己找命令,或者直接`dotnet restore`), 运行以下命令来完成构建所需的 Android 工具链安装.

```shell
dotnet workload install android
```

#### 通过 CLI 构建

你也可以通过以下命令构建`osu`:

```shell
dotnet run --project osu.Desktop
```

添加 `-c Release`参数到构建指令中,`Debug`构建出的版本会有严重的性能问题.

如果出现构建错误,可以尝试使用`dotnet restore`恢复NuGet包.
