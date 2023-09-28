

using Chopper.States.Splash;
using Chopper.Engine;
using Chopper.States.Dev;

const int WIDTH = 1280;
const int HEIGHT = 720;

using var game = new MainGame(WIDTH, HEIGHT, new SplashState());
// used to run in non-game mode so you can test out things like particles
// using var game = new MainGame(WIDTH, HEIGHT, new DevState());
game.Run();


