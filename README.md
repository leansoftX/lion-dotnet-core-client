Lion Client SDK for .NET 
===========================



快速开始
-----------

1. 使用Nuget安装Lion Client SDK到你的项目中

        Install-Package Lion.Client.SDK

2. 导入命名空间

        using Lion.Client.SDK;

3. 在项目中创建LionClient对象

         LionClient lionClient = new LionClient("environment-sdk");

创建你的第一个功能开关
-----------------------

4. 在Lion门户上的对应业务系统里创建你需要控制的 “功能标记”。
5. 在你的业务系统里，通过创建好的 ”功能标记“ 唯一标示，来验证功能是否开启

        
		var user = new LionUser("user-unique-key");
        bool showFeature = lionClient.BoolVariation("feature-key",user,false);
		
        if (showFeature) {
          // 显示功能的业务逻辑代码
        }
        else {
          // 关闭功能的业务逻辑代码
        }

