## Avalonia localizer
  The sample project with the ready-to-use localization provider, allowing to make dynamic runtime localization using the [AvaloniaUI](https://github.com/AvaloniaUI/Avalonia) framework.
  The idea is to create JSON files assets. Each file contain key and translation according to the locale:
``` JSON
 {
  "EnumSample": "Primer pretvornika enum",
  "car": "Avto",
  "plane": "Letalo",
  "bicycle": "Kolo",
  "SampleTranslate": "To je primer besedila, uporabljenega za lokalizacijo."
}
```
Than you can use the localizer in XAML, code or with enums:
``` C#
\\ XAML
xmlns:localization="clr-namespace:Avalonia.Localizer.Core.Localization"
<TextBlock FontSize="18" Margin="0,10,0,0" Text="{localization:Localize SampleTranslate}"/>

\\ XAML for Enum
<Window.Resources>
   <converters:EnumDescriptionConverter x:Key="enumConverter" />
</Window.Resources>

<ComboBox.ItemTemplate>
  <DataTemplate>
     <TextBlock Text="{Binding Converter={StaticResource enumConverter}}"/>
  </DataTemplate>
</ComboBox.ItemTemplate>

\\ Code
var localized = ProgramCore.Localizer["plane"];

\\ In Enum, where in the Description attribute we use the localization key
 public enum SampleEnum
    {
        [Description("car")]
        Car,
    }
    
 EnumDescriptionConverter.GetEnumDescription(SampleEnum.Car);
```
Thanks to bindings we can change the locale in the runtime just with the one line of code:
``` C#
ProgramCore.Localizer.SwitchLanguage("en-US");
```  
  <p align="center"> <img src="Avalonia.Localizer\Assets\SampleEn.jpg" width="450" align="center" title="Screenshot with English locale"> </p> <p align="center"> <img src="Avalonia.Localizer\Assets\SampleSl.jpg" width="450" align="center" title="Screenshot with Slovenian locale"> </p>

Credits to [sakya](https://github.com/sakya/) for the idea.
