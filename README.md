[![Yukikoi Melt](https://images2.imgbox.com/0b/df/66PTpxUz_o.png)](https://t.me/Yukikoi_Melt)

<h3 align="center">实验性的自定义osu!(Yukikoi-lazer)</h3>

<h3 align="center">节奏只需*点击一下*即可！</h3>

<p align="center">
<img width="500" alt="osu! logo" src="assets/lazer.png">
</p>

<h3 align="center">好大一个osu(</h3>


## 构建

#### 通过 IDE 构建

打开项目时选择与平台相关的 `.slnf` 文件（而不是主 `.sln` 文件）加载解决方案, 这可以减少依赖项加载并隐藏其他平台的资源文件.

有效的 `.slnf` 文件包括：

`osu.Desktop.slnf`(桌面端(Macos/Windows/Linux))

`osu.Android.slnf`(Android)

`osu.iOS.slnf`(某高端会员制全拟真液态UI操作系统)

构建项目时建议先将解决方案配置从`Debug`(默认)切换至`Release`,不然就准备迎接个位数帧率`osu`吧(雾.

如果你之前没有编译过移动端项目, 可能需要预先安装 Android/iOS 工具链, 运行以下命令来完成构建所需的 Android/iOS 工具链安装.

```shell
dotnet workload restore 
```

#### 通过 CLI 构建

你也可以通过以下命令构建`osu`:

```shell
dotnet run --project osu.Desktop
```

添加 `-c Release`参数到构建指令中,`Debug`构建出的版本会有严重的性能问题.

如果出现构建错误,可以尝试使用`dotnet restore`恢复NuGet包.
