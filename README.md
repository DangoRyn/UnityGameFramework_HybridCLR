# UnityGameFramework_Huatuo

[huatuo](https://github.com/focus-creative-games/huatuo)是一个特性完整、零成本、高性能、低内存的近乎完美的Unity全平台原生c#热更方案。

- huatuo扩充了il2cpp的代码，使它由纯AOT runtime变成‘AOT+Interpreter’ 混合runtime，进而原生支持动态加载assembly，使得基于il2cpp backend打包的游戏不仅能在Android平台，也能在IOS、Consoles等限制了JIT的平台上高效地以AOT+interpreter混合模式执行。从底层彻底支持了热更新。
---

[Luban](https://github.com/focus-creative-games/luban)是你的**最佳游戏配置解决方案**。

luban高效地处理游戏开发中常见的excel、json、xml之类的数据，检查数据错误，生成c#等各种语言的代码，导出成bytes或json等多种格式。

luban统一了游戏配置开发工作流，极大提升了策划和程序的工作效率。

## 核心特性

- 强大的数据解析和转换能力 {excel(csv,xls,xlsx)、json、bson、xml、yaml、lua、unity ScriptableObject} => {binary、json、bson、xml、lua、yaml、erlang、 custom format}
- 增强的excel格式，可以简洁地配置出像简单列表、子结构、结构列表，以及任意复杂的深层次的嵌套结构。
- 完备的类型系统，**支持OOP类型继承**，搭配excel、json、lua、xml等格式数据**灵活优雅**表达行为树、技能、剧情、副本之类复杂GamePlay数据
- 支持生成c#、java、go、c++、lua、python、javascript、typescript、erlang、rust、gdscript 代码
- 支持生成 protobuf(schema + binary + json)、flatbuffers(schema + json)、msgpack(binary)
- 强大的数据校验能力。ref引用检查、path资源路径、range范围检查等等
- 完善的本地化支持。静态文本值本地化、动态文本值本地化、时间本地化、main-patch多地区版本
- 强大灵活的自定义能力，支持自定义代码模板和数据模板
- **通用型生成和缓存工具**。也可以用于生成协议、数据库之类的代码，甚至可以用作对象缓存服务
- **良好支持主流引擎、全平台、主流热更新方案、主流前后端框架**。支持Unity、Unreal、Cocos2x、Godot、微信小游戏等主流引擎。工具自身跨平台，能在Win,Linux,Mac平台良好工作。

完整特性请参见 [feature](https://focus-creative-games.github.io/luban/generate_code_data/)

---

[Game Framework](https://github.com/EllanJiang/UnityGameFramework)是一个基于 Unity 引擎的游戏框架，主要对游戏开发过程中常用模块进行了封装，很大程度地规范开发过程、加快开发速度并保证产品质量。
在最新的 Game Framework 版本中，包含以下 19 个内置模块，后续我们还将开发更多的扩展模块供开发者使用。

1. **全局配置 (Config)** - 存储一些全局的只读的游戏配置，如玩家初始速度、游戏初始音量等。

2. **数据结点 (Data Node)** - 将任意类型的数据以树状结构的形式进行保存，用于管理游戏运行时的各种数据。

3. **数据表 (Data Table)** - 可以将游戏数据以表格（如 Microsoft Excel）的形式进行配置后，使用此模块使用这些数据表。数据表的格式是可以自定义的。

4. **调试器 (Debugger)** - 当游戏在 Unity 编辑器中运行或者以 Development 方式发布运行时，将出现调试器窗口，便于查看运行时日志、调试信息等。用户还可以方便地将自己的功能注册到调试器窗口上并使用。

5. **下载 (Download)** - 提供下载文件的功能，支持断点续传，并可指定允许几个下载器进行同时下载。更新资源时会主动调用此模块。

6. **实体 (Entity)** - 我们将游戏场景中，动态创建的一切物体定义为实体。此模块提供管理实体和实体组的功能，如显示隐藏实体、挂接实体（如挂接武器、坐骑，或者抓起另一个实体）等。实体使用结束后可以不立刻销毁，从而等待下一次重新使用。

7. **事件 (Event)** - 游戏逻辑监听、抛出事件的机制。Game Framework 中的很多模块在完成操作后都会抛出内置事件，监听这些事件将大大解除游戏逻辑之间的耦合。用户也可以定义自己的游戏逻辑事件。

8. **文件系统 (File System)** - 虚拟文件系统使用类似磁盘的概念对零散文件进行集中管理，优化资源加载时产生的内存分配，甚至可以对资源进行局部片段加载，这些都将极大提升资源加载时的性能。

9. **有限状态机 (FSM)** - 提供创建、使用和销毁有限状态机的功能，一些适用于有限状态机机制的游戏逻辑，使用此模块将是一个不错的选择。

10. **本地化 (Localization)** - 提供本地化功能，也就是我们平时所说的多语言。Game Framework 在本地化方面，不但支持文本的本地化，还支持任意资源的本地化，比如游戏中释放烟花特效也可以做出几个多国语言的版本，使得中文版里是“新年好”字样的特效，而英文版里是“Happy New Year”字样的特效。

11. **网络 (Network)** - 提供使用 Socket 长连接的功能，当前我们支持 TCP 协议，同时兼容 IPv4 和 IPv6 两个版本。用户可以同时建立多个连接与多个服务器同时进行通信，比如除了连接常规的游戏服务器，还可以连接语音聊天服务器。如果想接入 ProtoBuf 之类的协议库，只要派生自 Packet 类并实现自己的消息包类即可使用。

12. **对象池 (Object Pool)** - 提供对象缓存池的功能，避免频繁地创建和销毁各种游戏对象，提高游戏性能。除了 Game Framework 自身使用了对象池，用户还可以很方便地创建和管理自己的对象池。

13. **流程 (Procedure)** - 是贯穿游戏运行时整个生命周期的有限状态机。通过流程，将不同的游戏状态进行解耦将是一个非常好的习惯。对于网络游戏，你可能需要如检查资源流程、更新资源流程、检查服务器列表流程、选择服务器流程、登录服务器流程、创建角色流程等流程，而对于单机游戏，你可能需要在游戏选择菜单流程和游戏实际玩法流程之间做切换。如果想增加流程，只要派生自 ProcedureBase 类并实现自己的流程类即可使用。

14. **资源 (Resource)** - 为了保证玩家的体验，我们不推荐再使用同步的方式加载资源，由于 Game Framework 自身使用了一套完整的异步加载资源体系，因此只提供了异步加载资源的接口。不论简单的数据表、本地化字典，还是复杂的实体、场景、界面，我们都将使用异步加载。同时，Game Framework 提供了默认的内存管理策略（当然，你也可以定义自己的内存管理策略）。多数情况下，在使用 GameObject 的过程中，你甚至可以不需要自行进行 Instantiate 或者是 Destroy 操作。

15. **场景 (Scene)** - 提供场景管理的功能，可以同时加载多个场景，也可以随时卸载任何一个场景，从而很容易地实现场景的分部加载。

16. **配置 (Setting)** - 以键值对的形式存储玩家数据，对 UnityEngine.PlayerPrefs 进行封装，也可以将这些数据直接存储在磁盘上。

17. **声音 (Sound)** - 提供管理声音和声音组的功能，用户可以自定义一个声音的音量、是 2D 声音还是 3D 声音，甚至是直接绑定到某个实体上跟随实体移动。

18. **界面 (UI)** - 提供管理界面和界面组的功能，如显示隐藏界面、激活界面、改变界面层级等。不论是 Unity 内置的 uGUI 还是其它类型的 UI 插件（如 NGUI），只要派生自 UIFormLogic 类并实现自己的界面类即可使用。界面使用结束后可以不立刻销毁，从而等待下一次重新使用。

19. **Web 请求 (Web Request)** - 提供使用短连接的功能，可以用 Get 或者 Post 方法向服务器发送请求并获取响应数据，可指定允许几个 Web 请求器进行同时请求。
---

# 手把手教你GF运行热更新
- 首先进入HuatuoData目录
- 运行 init_huatuo_repos.bat 或 相应 .sh
- 进行 set_version_2020.3.33.bat 或 相应 .sh。
- 运行 init_local_il2cpp_data.bat或者相应.sh文件。注意！有可能需要修改脚本，设置你的unity安装路径！

如果LocalIl2CppData目录成功创建，没有遇到任何错误，恭喜我们的huatuo部署完成了。

使用unity打开，目前使用版本2020.3.33。
- 注意：由于不同版本之间il2cpp存在差异，所以需要精准匹配版本，LTS版本之间il2cpp差异较小有可能可以公用3.33的il2cpp。

执行`Huatuo->Generate->MethodBridge_arm64/MethodBridge_x64`。

这是因为经测试在Android下，热更代码中调用gameframework部分相关函数是会出现异常的，原因是AOT-interpreter桥接函数缺失引起的，但是win平台中并不会提示。

类似错误: ExecutionEngineException: GetManaged2NativeMethodPointer not support. xxxx 
具体参考:[huatuo默认桥接函数集](https://focus-creative-games.github.io/huatuo/performance/method_bridge/#huatuo%E9%BB%98%E8%AE%A4%E6%A1%A5%E6%8E%A5%E5%87%BD%E6%95%B0%E9%9B%86)。


MethodBridgeGenerator::PrepareCustomMethodSignatures已经补上这个"i4i8i8i8i4i1i8"这个函数签名。

``` cs
        private List<string> PrepareCustomMethodSignatures()
        {
            return new List<string>
            {
                // "vi8i8",
                "S108i8i8",
                "i4i8i8i8i4i1i8",
            };
        }
```

将生成的文件`Assets/../Library/Huatuo/MethodBridge_x64.cpp`和`MethodBridge_arm64`复制到`Assets/../HuatuoData/LocalIl2CppData/il2cpp/libil2cpp/huatuo/interpreter`
（注意：有时候我们修改il2cpp源码的时候，并没有生效，需要清缓存，清理`Library/Il2cppBuildCache`目录。）

然后我们选择对应要编译的平台，注意目前只能编译64位的程序，勾选32位程序将无法正确编译，且需要将Use Incremental GC关闭，编译选择il2cpp，api选择.net 4.x。
热更代码位于`Assets/GameMain/Scripts/Hotfix`中
选择`Huatuo->CompileDLL->{target}`，将会自动编译热更代码，并且拷贝至`GameMain/Hotfix`目录下。
`GameMain/MetadataAssemblys`路径中存放用于作为补充元数据的dll，使用过程中需要向ProcedureLoadHotfix中s_AotDllList中添加相应的dll。
用于补充元数据裁剪后的dll使用参考：[基于补充元数据的泛型函数实例化技术](https://focus-creative-games.github.io/huatuo/performance/generic_limit/#%E5%9F%BA%E4%BA%8E%E8%A1%A5%E5%85%85%E5%85%83%E6%95%B0%E6%8D%AE%E7%9A%84%E6%B3%9B%E5%9E%8B%E5%87%BD%E6%95%B0%E5%AE%9E%E4%BE%8B%E5%8C%96%E6%8A%80%E6%9C%AF-huatuo%E7%9A%84%E4%B8%93%E5%88%A9%E6%8A%80%E6%9C%AF)

`Game Framework->Resource Tools->Resource Builder`选择对应平台打包，
然后将打包路径下`Package/{version}/{target}`目录下文件拷贝至StreamingAssets中，总共6文件GameData、GameFrameworkVersion、Hotfix、MetadataAssemblys、Resources、UI
具体GF相关使用，以及资源更新配置参考官网：[GF官网连接](https://gameframework.cn/)

最后开始unity中对应平台的打包即可，最后看到显示“测试”UI，则说明热更成功了。

遇到热更相关错误，务必先阅读官方常见错误处理文档：
[Huatuo常见错误处理](https://focus-creative-games.github.io/huatuo/common_errors/#%E9%81%87%E5%88%B0-missingmethodexception-aot-generic-method-isn-t-instantiated-in-aot-module-xxx-%E9%94%99%E8%AF%AF)

