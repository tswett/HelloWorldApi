This is a code sample I wrote for Terryberry in 2019.

This took me a while to do, since it involved using technologies which I haven't used before,
including ASP.NET Core and Simple Injector. Since I haven't worked with those technologies before,
I undoubtedly made a couple of mistakes, but I'm confident that I at least took a *reasonable*
approach to every problem I faced.

I'm not sure whether or not I did the architecture in the expected way; I have the `Greeting` entity
class and the `IGreetingService` interface in the `Sample.Domain` project, and the `GreetingService`
class in the `Sample.Infrastructure` project. This seems to match the infrastructure that was
suggested in the requirements document. There is no "GreetingRepository" interface or class, since
that didn't seem to be necessary for this project.

The requirements didn't say whether or not I should use test-driven development. For such a small
project, I decided that TDD wouldn't really be beneficial here.