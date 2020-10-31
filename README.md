# Moved to the Xamarin Community Toolkit

This project has been archived, as it has been integrated into the [Xamarin Community Toolkit](https://github.com/xamarin/XamarinCommunityToolkit). From there it can be maintained better by an evergrowing community of awesome contributors. The NuGet package itself is also deprecated from this point onwards. Please migrate over to the Xamarin Community Toolkit version when available.

---

<img src="https://github.com/sthewissen/Xamarin.Forms.StateSquid/blob/master/images/icon.png" width="150px" />

# Xamarin.Forms.StateSquid
A collection of attached properties that let you specify state views for any of your existing layouts.

[![Build Status](https://sthewissen.visualstudio.com/StateSquid/_apis/build/status/CI%20StateSquid%20YAML?branchName=master)](https://sthewissen.visualstudio.com/StateSquid/_build/latest?definitionId=41&branchName=master) [![](https://img.shields.io/nuget/vpre/Xamarin.Forms.StateSquid.svg)](https://nuget.org/packages/Xamarin.Forms.StateSquid)

## Why StateSquid?

Displaying items when your app is in a specific state is a common pattern throughout any mobile app. People create loading views to overlay on an entire screen or maybe just a subsection of your screen needs an individual loader. When there's no data to display we create empty state views or when something goes wrong we need to build an error state view. By implementing the `StateLayout`'s attached properties you can turn any layout element like a `Grid` or `StackLayout` into an individual state-aware element! StateSquid will take care of when to display which view.

<img src="https://github.com/sthewissen/Xamarin.Forms.StateSquid/blob/master/images/statesquid.gif?raw=true" width="400px" />

## Getting started

The project is up on NuGet at the following URL:

https://www.nuget.org/packages/Xamarin.Forms.StateSquid

Install this package into your shared project. There is no need to install it in your platform specific projects. After that you're good to go! Simply add the namespace declaration and the new `StateLayout` related attached properties should be available to you!

## Adding a simple loading state

Each layout that you make state-aware using the `StateLayout` attached properties contains a collection of `StateView` objects. These can be used as templates for the different states supported by `StateLayout`. Whenever the `CurrentState` is set to a value that matches the `State` property of one of the `StateViews` its contents will be displayed instead of the main content.

```
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms" 
   xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" 
   xmlns:state="clr-namespace:Xamarin.Forms.StateSquid;assembly=Xamarin.Forms.StateSquid" 
   x:Class="SampleApp.MainPage">

   <Grid state:StateLayout.CurrentState="{Binding CurrentState}">
      <state:StateLayout.StateViews>
         <state:StateView StateKey="Loading">
            <Grid BackgroundColor="White">
               <StackLayout VerticalOptions="Center" HorizontalOptions="Center">
                  <ActivityIndicator Color="#1abc9c" IsRunning="{Binding CurrentState, Converter={StaticResource StateToBooleanConverter}, ConverterParameter={x:Static state:State.Loading}}" />
                  <Label Text="Loading..." HorizontalOptions="Center" />
               </StackLayout>
            </Grid>
         </state:StateView>
      </state:StateLayout.StateViews>      
  
     ...
     
  </Grid>
  
</ContentPage>
```

The `State` property supports one of the following values:

- `Loading`
- `Saving`
- `Success`
- `Error`
- `Empty`
- `Custom`
- `None` (this will show the default view).

## Using custom states

Besides the built-in state StateSquid also supports a `Custom` state. By setting `State="Custom"` and `CustomStateKey="[yourvalue]"` you can create custom states beyond the built-in ones. You can use the `CurrentCustomStateKey` on your root `StateLayout` element to databind a variable that indicates when to show one of your custom states.

```
 <StackLayout Padding="10" state:StateLayout.CurrentState="{Binding CurrentState}" state:StateLayout.CurrentCustomStateKey="{Binding CustomState}" BackgroundColor="#f0f1f2">
   <state:StateLayout.StateViews>
      <state:StateView StateKey="Custom" CustomStateKey="ThisIsCustomHi">
         <Label Text="Hi, I'm a custom state!" VerticalOptions="Center" VerticalTextAlignment="Center" HorizontalOptions="Center" HorizontalTextAlignment="Center"  />
      </state:StateView>
      <state:StateView StateKey="Custom" CustomStateKey="ThisIsCustomToo">
         <Label Text="Hi, I'm a custom state too!" VerticalOptions="Center" VerticalTextAlignment="Center" HorizontalOptions="Center" HorizontalTextAlignment="Center"  />
      </state:StateView>
   </state:StateLayout.StateViews>
   <Label Text="This is the normal state." VerticalOptions="Center" VerticalTextAlignment="Center" HorizontalOptions="Center" HorizontalTextAlignment="Center" />
</StackLayout>
```

## Skeleton loading

Skeleton screens in different shapes and sizes are found everywhere across the web and apps — anywhere us humans are forced to wait. They are usually perceived as being shorter in duration when compared against a blank screen and a spinner.

>To mitigate focus on the loading process, versus the actual content that is loading, [Luke Wroblewski](https://www.lukew.com/ff/entry.asp?1797) introduced a new design pattern — the skeleton screen. In his own words, they are "essentially a blank version of a page into which information is gradually loaded." These visual placeholders were shown by Wroblewski to be light grey boxes that appeared instantly in areas where content had not yet completed loading.

StateSquid supports adding a skeleton loader like appearance using a `SkeletonView`. This view is automatically animated to show a slight blinking state animation. You can see it in action in the image below. Simply add it to your loading template and size it accordingly. You can even use `CornerRadius` to make rounded variations of it.

<img src="https://github.com/sthewissen/Xamarin.Forms.StateSquid/blob/master/images/skeleton.gif?raw=true" width="400px" />

## Repeating a template multiple times

When loading multiple items of the same type it could be benificial if we can simply repeat a piece of XAML without having to copy paste it multiple times. This is where `RepeatCount` comes into play. By defining a `RepeatTemplate` we can repeat the same bit of XAML but only define it once. A sample of this process can be seen here.

```
<StackLayout state:StateLayout.CurrentState="{Binding CurrentState}" Grid.Row="0">
     <state:StateLayout.StateViews>
        <state:StateView StateKey="Loading" RepeatCount="3">
            <state:StateView.RepeatTemplate>
                <DataTemplate>
                    <Grid Padding="20" HeightRequest="120">
                        <Grid.RowDefinitions>
                            <RowDefinition Height="24" />
                            <RowDefinition Height="20" />
                            <RowDefinition Height="Auto" />
                            <RowDefinition Height="14" />
                            <RowDefinition Height="Auto" />
                        </Grid.RowDefinitions>
                        <Grid.ColumnDefinitions>
                            <ColumnDefinition Width="*" />
                            <ColumnDefinition Width="*" />
                            <ColumnDefinition Width="*" />
                        </Grid.ColumnDefinitions>
                        <state:SkeletonView CornerRadius="6" Grid.Row="0" Grid.ColumnSpan="2" HeightRequest="20" BackgroundColor="#cccccc" WidthRequest="120" />
                        <state:SkeletonView CornerRadius="6" Grid.Row="1" Grid.ColumnSpan="3" HeightRequest="20" BackgroundColor="#cccccc" WidthRequest="200" />
                        <BoxView Grid.Row="2" Grid.ColumnSpan="3" HeightRequest="1" BackgroundColor="#cccccc" Margin="0,8" />
                        <Label Grid.Row="3" Grid.Column="0" HorizontalOptions="Center" Text="TOTAL ARTICLES" FontSize="10" TextColor="Gray" />
                        <Label Grid.Row="3" Grid.Column="1" HorizontalOptions="Center" Text="PRICE" FontSize="10" TextColor="Gray" />
                        <Label Grid.Row="3" Grid.Column="2" HorizontalOptions="Center" Text="BOXES" FontSize="10" TextColor="Gray" />
                        <state:SkeletonView CornerRadius="6" Grid.Row="4" Grid.Column="0" HeightRequest="20" BackgroundColor="#cccccc" HorizontalOptions="Fill" />
                        <state:SkeletonView CornerRadius="6" Grid.Row="4" Grid.Column="1" HeightRequest="20" BackgroundColor="#cccccc" HorizontalOptions="Fill" />
                        <state:SkeletonView CornerRadius="6" Grid.Row="4" Grid.Column="2" HeightRequest="20" BackgroundColor="#cccccc" HorizontalOptions="Fill" />
                    </Grid>
                </DataTemplate>
            </state:StateView.RepeatTemplate>
        </state:StateView>
    </state:StateLayout.StateViews>
<StackLayout>
```

This code results in the following UI, where the template is repeated three times:

<img src="https://github.com/sthewissen/Xamarin.Forms.StateSquid/blob/master/images/repeat.gif?raw=true" width="400px" />

## Property reference

Your imagination is the only limit to what you can do. You can use this control to recreate your layout and make a skeleton loader our of it. Or you can just use a simple `Label` to indicate that your application is in a specific state. The following parts are what make this thing tick:

| Property | Available on | What it does | Extra info |
| ------ | ------ | ------ | ------ |
| `CurrentState` | `StateLayout` | Defines the current state of the layout and which template to show. | `Loading`, `Saving`, `Success`, `Error`, `Empty`, `None`, `Custom`|
| `CurrentCustomStateKey` | `StateLayout` | Pair this with `State="Custom"` on a `StateView` to add custom states. | |
| `StateViews` | `StateLayout` | A list of `StateView` objects that contains a template per `State`. | |
| `CustomStateKey` | `StateView` | Used to identify a `StateView` when using `State="Custom"` | |
| `RepeatCount` | `StateView` | Repeats the specific `StateView` by a given amount. | Ideal to use to show a list of multiple items for e.g. a skeleton loader. |
| `RepeatTemplate` | `StateView` | Defines the `DataTemplate` that gets repeated when using `RepeatCount`. | |
| `State` | `StateView` | Used to identify a `StateView` to be shown for a specific `State`. | |
