namespace FuncUIFileDialog

open System
open Avalonia
open Avalonia.Themes.Fluent
open Avalonia.FuncUI.Hosts
open Avalonia.Controls.ApplicationLifetimes

open Avalonia.Controls
open Avalonia.FuncUI
open Avalonia.FuncUI.DSL

module MainView =
    let create windowParams =
        Component (
            fun ctx ->
                Grid.create [
                    Grid.rowDefinitions "*"
                    Grid.children [
                        TextBlock.create [
                            Grid.row 0
                            TextBlock.text "Hello"
                        ]
                    ]
                ]
        )

type MainWindow() =
    inherit HostWindow()

    do
        base.Title   <- "WinApp"
        base.Content <- MainView.create ()

type App() =
    inherit Application()

    override this.Initialize() =
        this.Styles.Add (FluentTheme(baseUri = null, Mode = FluentThemeMode.Dark))

    override this.OnFrameworkInitializationCompleted() =
        this.Name <- "WinApp"
        match this.ApplicationLifetime with
        | :? IClassicDesktopStyleApplicationLifetime as desktopLifetime ->
            let mainWindow = MainWindow()
            mainWindow.MinWidth <- 850.0
            mainWindow.MinHeight <- 600.0

            desktopLifetime.MainWindow <- mainWindow
        | _ -> ()

module Program =

    [<EntryPoint; STAThread>]
    let main(args: string[]) =
        AppBuilder
            .Configure<App>()
            .UsePlatformDetect()
            //.With(Win32PlatformOptions(UseWgl = true)) // TODO - don't know why this is needed
            .UseSkia()
            .StartWithClassicDesktopLifetime(args)