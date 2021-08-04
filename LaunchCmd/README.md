# LaunchCmd

## 介绍

通过命令行启动应用，但是需要自行配置

## 使用

1. startup

    ```json
    {
        "startup": [
        "v2"
        ],
        "data": [
        {
            "name": "v2",
            "path": "d:\\test\\hello.exe"
        }
        ]
    }
    ```

    这里的startup 表示的是在`LaunchCmd` 在启动时也要跟着启动的程序（不是系统自动启动，而且还需要输入`S`）,数据类型是个字符串数组，存储的数据应该在`data` 里面定义。

2. jar

    很显然，exe 格式的文件在上面已经展示过了。

    这个jar 的格式的文件有两种类型，一种是带有图形化界面的，一种是不带有图形化界面的。

    1. 如果是图形化界面的

    ```json
    {
        "name": "v2",
        "path": "d:\\test\\hello.jar"
    }
        ```

    非常方便，和exe 没有什么区别。

    2. 如果是控制台程序，

    首先，控制台程序无法在`LaunchCmd` 中运行，必须单独开设一个黑窗口。

    ```json
    {
        "name":"cr",
        "working":"D:\\web 服务\\CoRe",
        "path":"java -jar \"D:\\web 服务\\CoRe\\CoRe-0.0.2-SNAPSHOT.jar\"",
        "type":"cmd"
    }
    ```

    正如你看到的那样，`path` 里面存储的并不是一个文件或者文件夹，而是一个命令。

    `working` 属性可以不用设置，除非要启动的应用需要读取相对路径中的文件。`type` 设置为`cmd` ，此`jar` 便可以以黑窗口的形式执行。（启动形式是通过`cmd /c`）

    [CoRe](https://github.com/storytellerF/CoRe) 是我的另一个开源项目。

3. 批处理

    批处理可以通过像上面的

    ```json
    {
        "name":"ss",
        "path":"D:\\mygz\\start.cmd",
        "type":"cmd"
    }
    ```

    设置。很显然，这里没有设置`working`。

4. 特殊命令

    `P` 可以打印当前的配置情况。

    `Q` 可以退出`LaunchCmd`

    `v2 -q` 启动v2之后关闭`LaunchCmd`

    `S` 启动`startup` 定义的程序。

## 注意

启动Java 程序的时候，需要jre 或者jdk ，这个需要用户自行下载。国内用户推荐到国内的镜像站下载。

安装完成之后需要设置为环境变量，不过这不是必须的。

```json
{
  "javaHome":"your jdk path",
  "startup": [
    "v2"
  ],
  "data": [
    {
      "name": "v2",
      "path": "d:\\test\\hello.exe"
    }
  ]
}
```

完全可以通过这种方式设置。如果不设置，默认使用的是jdk12。路径为`C:\\Program Files\\Java\\jdk-12.0.1\\bin\\java.exe`
