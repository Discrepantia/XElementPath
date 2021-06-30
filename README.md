# XPath

Use it kind a like XPath to work through XML Documents

## example

```
var content = xmldoc.GetElements($"//Child[@objectType = 'Type1' && @name = 'Name1']/Content").ToList();
var contentText = xmldoc.GetElements($"//Child[@objectType = 'Type1' && @name = 'Name1']/Content/.").ToList();
```
```
<Object>
    <Child name="Name1" objectType="Type1">
        <Content>
            Text for Child1
        </Content>
    </Child>
    <Child name="Name2" objectType="Type1">
        <Content>
            Text for Child2
        </Content>
    </Child>
    <Child name="Name1" objectType="Type2">
        <Content>
            Text for Chilld3
        </Content>
    </Child>
</Object>
