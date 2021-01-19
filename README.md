# Aess.AutoFixture.Extensions
A small collection of handy extensions for AutoFixture

![Build](https://github.com/Alyssaess/Aess.AutoFixture.Extensions/workflows/Build%20AutoFixtureExtensions%20Project/badge.svg?branch=master&event=push)
![Release](https://github.com/Alyssaess/Aess.AutoFixture.Extensions/workflows/Release%20AutoFixtureExtensions%20Package/badge.svg?branch=master&event=workflow_run)

## Project Description

This extends the classes AutoFixture.Fixture and AutoFixture.Dsl.IPostprocessComposer to offer the ``BuildMany<T>()`` method.

## Overview

These extension methods allow add the ``.BuildMany<T>()`` method to the AutoFixture.Fixture class and various methods to the AutoFixture.Dsl.IPostProcessComposer class allowing you to fluently build objects in your unit tests.

The methods provided can be chained together in the same way that the they can be chained together for the ``Build<T>()`` method that is in the original AutoFixture package.

## .NET platforms compatibility table

| Product                    | .NET Standard               |
| -------------------------- | ------------------------    |
| Aess.AutoFixture.Extensions | :heavy_check_mark: 2.0 |

## Availability

<a href="https://www.nuget.org/packages/Aess.AutoFixture.Extensions/">
<img src="https://www.nuget.org/Content/gallery/img/logo-header.svg" alt="NuGet" height="20"/>
</a>

[GitHub](https://github.com/Alyssaess/Aess.AutoFixture.Extensions)

## Contributing

If you would like to contribute, use the contact owners link on NuGet or raise a feature request on GitHub.

## Usage

### BuildMany

This is used in a similar way to the ``CreateMany<T>()`` method in AutoFixture:  
```c#
public void TestMethod()
{
    var fixture = new ();
    var items = _fixture.BuildMany<TestClass>()
                        .Create();
    // items is of type IEnumerable<TestClass> and has a count of 3 by default
}
```

To specify the number of objects you wish to build, pass in an integer:
```c#
public void TestMethod()
{
    var fixture = new ();
    var items = _fixture.BuildMany<TestClass>(5)
                        .Create();
    // items is of type IEnumerable<TestClass> and has a count of 5
}
```
### Create

This creates the requested objects:
```c#
public void TestMethod()
{

    var fixture = new (){OmitAutoProperties = false};
    var items = _fixture.BuildMany<TestClass>()
                        .Create();

}
```
### With

This is used in a similar way to the ``With<T>()`` method in AutoFixture:
```c#
public void TestMethod()
{
    var fixture = new ();
    var items = _fixture.BuildMany<TestClass>()
                        .With(p => p.Name, "Jane Doe")
                        .Create();
                        
    // All of the items in the collection, will have the property "Name" set to the value "Jane Doe"
}
```

You can pass a function into the method, if you want a new value generated each time:
```c#
public void TestMethod()
{
    var itemCount = 0;
    
    string NextName()
    {
        itemCount++;
        return $"Name {i}";
    }
    
    var fixture = new ();
    var items = _fixture.BuildMany<TestClass>()
                        .With(p => p.Name, () => NextName())
                        .Create();
                        
    // When each object is created, it will call the local function "NextName" and the name of 
    // each item will be "Name 1", "Name 2", etc. 
}
```
### Without

Use this method to explicitly not set a property value:
```c#
public void TestMethod()
{
    
    var fixture = new ();
    var items = _fixture.BuildMany<TestClass>()
                        .Without(p => p.Name)
                        .Create();
                        
    // When each object is created, it will set values of all properties, except the property "Name" 
}
```

### WithAutoProperties

Ensures properties are set when creating the object.  This overrides the OmitAutoProperties property of the Fixture class:
```c#
public void TestMethod()
{
    
    var fixture = new (){OmitAutoProperties = true};
    var items = _fixture.BuildMany<TestClass>()
                        .WithAutoProperties(p => p.Name)
                        .Create();
                        
    // When each object is created, it will set values of all properties, even though OmitAutoProperties is true 
}
```
### OmitAutoProperties

Ensures properties are not set when creating the object.  This overrides the OmitAutoProperties property of the Fixture class:
```c#
public void TestMethod()
{
    
    var fixture = new (){OmitAutoProperties = false};
    var items = _fixture.BuildMany<TestClass>()
                        .OmitAutoProperties()
                        .Create();
                        
    // When each object is created, it will not set values of all properties, even though OmitAutoProperties is false 
}
```