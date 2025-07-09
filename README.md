[![Yukikoi Melt](https://images2.imgbox.com/0b/df/66PTpxUz_o.png)](https://t.me/Yukikoi_Melt)

<h3 align="center">节奏只需*点击一下*即可！</h3>

<h3 align="center">点击[此处](https://osu.ppy.sh)访问官方网站owo</h3>

<p align="center">
<img width="500" alt="osu! logo" src="assets/lazer.png">
</p>

<h3 align="center">好大一个osu(</h3>


### 构建

#### 通过 IDE 构建

建议通过特定与平台的 .slnf 文件（而不是主 .sln 文件）加载解决方案.这将减少依赖项加载并隐藏不需要关注的平台资源.

有效的 .slnf 文件包括：

osu.Desktop.slnf（最常用）

osu.Android.slnf

osu.iOS.slnf

已为常见IDE配置了运行预设.可以直接使用 IDE 提供的 "生成/运行" 功能来启动项目

若你是第一次为移动端构建程序，可能需要预先安装 Android/iOS 工具链

运行以下命令来完成构建所需的 Android/iOS 工具链安装

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
