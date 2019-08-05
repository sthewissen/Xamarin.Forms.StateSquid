<img src="https://github.com/sthewissen/Xamarin.Forms.StateSquid/blob/master/images/icon.png" width="150px" />

# Xamarin.Forms.StateSquid
A collection of attached properties that let you specify state views for any of your existing layouts.

[![Build status](https://sthewissen.visualstudio.com/StateSquid/_apis/build/status/StateSquid-CI)]() ![](https://img.shields.io/nuget/vpre/Xamarin.Forms.StateSquid.svg)

## Why StateSquid?

Displaying items when your app is in a specific state is a common pattern throughout any mobile app. People create loading views to overlay on an entire screen or maybe just a subsection of your screen needs an individual loader. When there's no data to display we create empty state views or when something goes wrong we need to build an error state view. By implementing the `StateLayout` bindable properties you can turn any layout element like a `Grid` or `StackLayout` into an individual state-aware element! StateSquid will take care of when to display which view.

<img src="https://raw.githubusercontent.com/sthewissen/Xamarin.Forms.StateSquid/master/images/sample.gif" />

## How to use it?

The project is up on NuGet at the following URL:

https://www.nuget.org/packages/Xamarin.Forms.StateSquid

Install this package into your shared project. There is no need to install it in your platform specific projects. After that you're good to go! Simply add the namespace declaration and the new `StateLayout` related attached properties should be available to you!

```
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms" 
   xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" 
   xmlns:state="clr-namespace:Xamarin.Forms.StateSquid; assembly=Xamarin.Forms.StateSquid" 
   x:Class="SampleApp.MainPage">

   <Grid state:StateLayout.State="{Binding CurrentState}">
      <state:StateLayout.LoadingTemplate>
         <DataTemplate>
            <Grid BackgroundColor="White">
               <StackLayout VerticalOptions="Center" HorizontalOptions="Center">
                  <ActivityIndicator Color="#1abc9c" IsRunning="{Binding CurrentState, Converter={StaticResource StateToBooleanConverter}, ConverterParameter={x:Static state:State.Loading}}" />
                  <Label Text="Loading..." HorizontalOptions="Center" />
               </StackLayout>
            </Grid>
         </DataTemplate>
      </state:StateLayout.LoadingTemplate>      
  
     ...
     
  </Grid>
  
</ContentPage>
```

The `State` property supports one of the following values:

- Loading
- Saving
- Success
- Error
- Empty
- None (this will show the default view).

## What can I do with it?

Your imagination is the only limit to what you can do. You can use this control to recreate your layout and make a skeleton loader our of it. Or you can just use a simple `Label` to indicate that your application is in a specific state. The following parts are what make this thing tick:

| Property | What it does | Extra info |
| ------ | ------ | ------ |
| `State` | Defines the current state of the layout and which template to show. | |
| `LoadingTemplate` | A data template that contains what is shown when loading. | A ```StateDataTemplate``` object. |
| `RepeatCount` | Repeats the specific `StateDataTemplate` by a given amount. | Ideal to use to show a list of multiple items for e.g. a skeleton loader. |
