Lion Client SDK for .NET 
===========================



快速开始
-----------

1. 安装Lion Client-SDK到你的项目中

        Install-Package Lion.Client.SDK

2. 导入命名空间

        using Lion.Client.SDK;

3. 在项目中创建LionClient对象

         LionClient lionClient = new LionClient("your environment sdk");

创建你的第一个功能开关
-----------------------

1. 在Lion门户上创建你需要控制的功能。
2. 在你的业务系统里，通过创建好的功能开关唯一标示，来验证功能是否开启

        
        bool showFeature = lionClient.BoolVariation("your.feature.key");
        if (showFeature) {
          // 显示功能的业务逻辑代码
        }
        else {
          // 关闭功能的业务逻辑代码
        }


