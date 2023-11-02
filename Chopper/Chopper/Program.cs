using Chopper.States.Splash;
using Chopper.Engine;
using Chopper.States.Dev;
using System;
using Chopper.Content;
using System.Globalization;

const int WIDTH = 1280;
const int HEIGHT = 720;

Strings.Culture = CultureInfo.CurrentCulture;
// Strings.Culture = CultureInfo.GetCultureInfo("ja-JP");
// Strings.Culture = CultureInfo.GetCultureInfo("fr-FR");

using (var game = new MainGame(WIDTH, HEIGHT, new SplashState(), true))
{
    game.IsFixedTimeStep = true;
    game.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / 60);
    game.Run();
}
// used to run in non-game mode so you can test out things like particles
//using (var game = new MainGame(WIDTH, HEIGHT, new TestCameraState(), true))
//{
//    game.IsFixedTimeStep = true;
//    game.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / 60);
//    game.Run();
//}


